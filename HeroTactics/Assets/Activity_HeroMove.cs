using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Activity_HeroMove : MonoBehaviour {

	Camera mainCam;

	[SerializeField] GameObject sceneManager;
	ActivityManager actMan;

	Activity_HeroTurnManager turnMan;
	[SerializeField] HeroCanvasManager canvasMan;

	public enum Actions {none, idle, moving, attacking};
	public Actions currentAction;

	public bool hasMoved = false;
	GameObject currentHex;

	NavMeshAgent navAgent;
	Vector3 navTargetPos;

	Animator anim;

	int moveCost = 1;

	
	void Start() {
        mainCam = Camera.main;

		actMan = sceneManager.GetComponent<ActivityManager>();

		turnMan = GetComponent<Activity_HeroTurnManager>();

		currentAction = Actions.idle;

		navAgent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
    }

    void Update() {
		if (actMan.myTurn == ActivityManager.whoseTurn.playerTurn) {
			if (currentAction == Actions.moving) {
				Ray camRay = mainCam.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(camRay, out hit, 50f)) {
					//print("Cam ray hitting something");

					if (hit.collider.CompareTag("Hex") && hit.collider.GetComponent<Arena_HexScript>().occupied == false) {
						//print("Cam ray hitting a hex");

						if (Input.GetMouseButtonDown(0)) {
							navTargetPos = hit.collider.gameObject.transform.position;
							currentHex = hit.collider.gameObject;

							transform.LookAt(transform.forward, navAgent.steeringTarget);
							navAgent.destination = navTargetPos;
							anim.SetBool("Moving", true);
							hasMoved = true;
						}
					}
				}
			}
		}

		if (hasMoved && Vector3.Distance(transform.position, navAgent.destination) <= navAgent.stoppingDistance) {
			//currentHex.GetComponent<Arena_HexScript>().occupied = true;
			turnMan.actionPoints -= moveCost;
			navAgent.enabled = false;
			anim.SetBool("Moving", false);


			currentAction = Actions.idle;

			if (turnMan.actionPoints <= 0) {
				turnMan.EndHeroTurn();
			}
			else {
				canvasMan.EnableActionButtons();
			}
		}
		else if (navAgent.isOnOffMeshLink) {
			//navAgent.velocity.Set(2, 0, 2);
			//print(navAgent.velocity);
			navAgent.speed = 1.5f;
		}
		else {
			navAgent.speed = 2f;
		}
    }


	void OnCollisionEnter (Collision enterColl) {
		if (enterColl.collider.gameObject.CompareTag("Hex")) {
			enterColl.collider.gameObject.GetComponent<Arena_HexScript>().occupied = true;
			//print("Occupying " + enterColl.transform.parent.name + " : " + enterColl.gameObject.name);
		}
	}


	void OnCollisionExit (Collision exitColl) {
		if (exitColl.collider.gameObject.CompareTag("Hex")) {
			exitColl.collider.gameObject.GetComponent<Arena_HexScript>().occupied = false;
			//print("Leaving " + exitColl.transform.parent.name + " : " + exitColl.gameObject.name);
		}
	}


	public void InitiateMove () {
		if (currentHex != null) {
			currentHex.GetComponent<Arena_HexScript>().occupied = false;
		}

		hasMoved = false;
		navAgent.enabled = true;
		currentAction = Actions.moving;

		canvasMan.DisableActionButtons();
	}
}
