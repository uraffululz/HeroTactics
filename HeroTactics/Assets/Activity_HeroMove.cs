using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Activity_HeroMove : MonoBehaviour {

	//Camera mainCam;

	[SerializeField] GameObject sceneManager;
	ActivityManager actMan;

	Activity_HeroTurnManager turnMan;
	//[SerializeField] HeroCanvasManager canvasMan;

	[SerializeField] List<GameObject> moveSpaces;
	int moveRange = 2;
	public bool hasMoved = false;

	NavMeshAgent navAgent;
	Vector3 navTargetPos;

	

	

	
	void Start() {
        //mainCam = Camera.main;

		actMan = sceneManager.GetComponent<ActivityManager>();

		turnMan = GetComponent<Activity_HeroTurnManager>();

		turnMan.currentAction = Activity_HeroTurnManager.Actions.idle;

		moveSpaces = new List<GameObject>();
		turnMan.currentHex = actMan.spawnHex;

		navAgent = GetComponent<NavMeshAgent>();
		turnMan.anim = GetComponent<Animator>();
    }

    void Update() {
		//if (actMan.myTurn == ActivityManager.whoseTurn.playerTurn) {
			if (turnMan.currentAction == Activity_HeroTurnManager.Actions.moving) {
				Ray camRay = actMan.mainCam.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(camRay, out hit, 50f)) {
					//print("Cam ray hitting something");

					if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Hex") &&
						moveSpaces.Contains(hit.collider.gameObject) && hit.collider.GetComponent<Arena_HexScript>().occupied == false) {
						ConfirmMove(hit);
					}
				}
			}
		//}

		if (hasMoved && Vector3.Distance(transform.position, navAgent.destination) <= navAgent.stoppingDistance) {
			EndMove();
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
			turnMan.currentHex = enterColl.collider.gameObject;
			turnMan.currentHex.GetComponent<Arena_HexScript>().occupied = true;
			turnMan.currentHex.GetComponent<Arena_HexScript>().occupant = this.gameObject;

			//print("Occupying " + enterColl.transform.parent.name + " : " + enterColl.gameObject.name);
		}
	}


	void OnCollisionExit (Collision exitColl) {
		if (exitColl.collider.gameObject.CompareTag("Hex")) {
			exitColl.collider.gameObject.GetComponent<Arena_HexScript>().occupied = false;
			exitColl.collider.gameObject.GetComponent<Arena_HexScript>().occupant = null;

			//print("Leaving " + exitColl.transform.parent.name + " : " + exitColl.gameObject.name);
		}
	}


	public void InitiateMove () {

		if (turnMan.currentHex != null) {
			moveSpaces = new List<GameObject>(actMan.DetermineMoveSpaces(turnMan.currentHex, moveRange));
			//currentHex.GetComponent<Arena_HexScript>().occupied = false;
		}

		hasMoved = false;
		navAgent.enabled = true;
		turnMan.currentAction = Activity_HeroTurnManager.Actions.moving;

		turnMan.myCanvas.DisableActionButtons();
//TODO Pop up a button on the Unit UI to cancel the current action (Same when initiating attack action)
//Alternatively, put it on the SceneUI and have it detect the "currently-active hero"

	}


	void ConfirmMove (RaycastHit hit) {
		navTargetPos = hit.collider.gameObject.transform.position;
		//currentHex = hit.collider.gameObject;

		transform.LookAt(transform.forward, navAgent.steeringTarget);
		navAgent.destination = navTargetPos;
		turnMan.anim.SetBool("Moving", true);
		hasMoved = true;
	}


	void EndMove () {
		foreach (GameObject space in moveSpaces) {
			space.GetComponent<MeshRenderer>().material = space.GetComponent<Arena_HexScript>().baseMat;
		}

		turnMan.actionPoints -= turnMan.moveCost;
		navAgent.enabled = false;
		turnMan.anim.SetBool("Moving", false);

		turnMan.currentAction = Activity_HeroTurnManager.Actions.idle;

		turnMan.EndHeroAction();

		//if (turnMan.actionPoints <= 0) {
		//	turnMan.EndHeroTurn();
		//}
		//else {
		//	//canvasMan.EnableActionButtons(true, true);
		//}
	}
}
