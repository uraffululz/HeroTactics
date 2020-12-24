using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity_HeroAttack : MonoBehaviour {

	[SerializeField] ActivityManager actMan;
	Activity_HeroTurnManager turnMan;

	[SerializeField] List<GameObject> enemiesInRange;

	//[SerializeField] GameObject target;

	[SerializeField] int attackRangeMin;
	[SerializeField] int attackRangeMax;



    void Start() {
        turnMan = GetComponent<Activity_HeroTurnManager>();

		enemiesInRange = new List<GameObject>();
    }


    void Update() {
		if (turnMan.currentAction == Activity_HeroTurnManager.Actions.attacking) {
			Ray camRay = actMan.mainCam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(camRay, out hit, 50f)) {
				if (Input.GetMouseButtonUp(0) && hit.collider.CompareTag("Enemy")
					&& enemiesInRange.Contains(hit.collider.gameObject) && hit.collider.GetComponent<Activity_EnemyTurnManager>().isTargetable) {

					ConfirmAttack(hit.collider.gameObject);
				}
			}
		}
    }


	public void InitiateAttack() {
		if (turnMan.currentHex != null) {
			enemiesInRange = actMan.DetermineEnemiesInRange(turnMan.currentHex, attackRangeMin, attackRangeMax);
		}

		turnMan.currentAction = Activity_HeroTurnManager.Actions.attacking;
		turnMan.myCanvas.DisableActionButtons();
//TODO Pop up a button on the Unit UI to cancel the current action (Same when initiating move action)
//Alternatively, put it on the SceneUI and have it detect the "currently-active hero"

	}


	public void ConfirmAttack(GameObject target) {
		transform.LookAt(target.transform.position);

		//Attack
		//turnMan.anim.SetBool("Attacking", true);

		print("Attacking: " + target.name);

		EndAttack();
	}


	public void EndAttack() {
		foreach (GameObject enemy in enemiesInRange) {
			enemy.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
		}

		turnMan.actionPoints -= turnMan.attackCost;
		//turnMan.anim.SetBool("Attacking", false);

		turnMan.currentAction = Activity_HeroTurnManager.Actions.idle;
		turnMan.EndHeroAction();
	}
}
