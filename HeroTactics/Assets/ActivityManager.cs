using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActivityManager : MonoBehaviour {

	Camera mainCam;

	[SerializeField] ArenaSceneUIManager ASUIM;

	public enum whoseTurn {neither, playerTurn, enemyTurn};
	public whoseTurn myTurn;

	public GameObject[] heroes;
	public GameObject[] enemies;

	[SerializeField] bool heroesSelectable;


	void Awake () {
		mainCam = Camera.main;

		heroes = GameObject.FindGameObjectsWithTag("Player");
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}


	void Start() {
        myTurn = whoseTurn.neither;
		
    }

 
	void Update() {
		if (heroesSelectable) {
			Ray camRay = mainCam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(camRay, out hit, 50f)) {
				if (hit.collider.CompareTag("Player") && Input.GetMouseButtonDown(0)) {
					heroesSelectable = false;
					hit.collider.GetComponent<Activity_HeroTurnManager>().StartHeroTurn();
				}
			}
		}
    }


	public void StartActivity() {
		ASUIM.introUI.SetActive(false);
		StartPlayerTurn();
	}


	public void EnableHeroSelection() {
		heroesSelectable = true;
	}


	public void StartPlayerTurn() {
		myTurn = whoseTurn.playerTurn;

		foreach (GameObject hero in heroes) {
			hero.GetComponent<Activity_HeroTurnManager>().actionPoints = hero.GetComponent<Activity_HeroTurnManager>().maxActionPoints;
		}

		EnableHeroSelection();

		print("Starting Player Turn");
	}


	public void EndPlayerTurn () {
		StartEnemyTurn();
	}


	public void StartEnemyTurn() {
		myTurn = whoseTurn.enemyTurn;

		print("Starting Enemy Turn");

		EndEnemyTurn();
	}


	void EndEnemyTurn() {
		StartPlayerTurn();
	}
}
