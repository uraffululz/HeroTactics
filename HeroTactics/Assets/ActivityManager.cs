using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActivityManager : MonoBehaviour {

	public Camera mainCam;

	[SerializeField] ArenaSceneUIManager ASUIM;

	public enum whoseTurn {neither, playerTurn, enemyTurn};
	public whoseTurn myTurn;

	public GameObject[] heroes;
	public GameObject[] enemies;

	[SerializeField] bool heroesSelectable;

	public GameObject spawnHex;

	public GameObject enemySpawnHex;


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

//PLAYER TURN FUNCTIONS

	public void StartPlayerTurn() {
		myTurn = whoseTurn.playerTurn;
		ASUIM.UpdateTurnText(myTurn);

		foreach (GameObject hero in heroes) {
			hero.GetComponent<Activity_HeroTurnManager>().actionPoints = hero.GetComponent<Activity_HeroTurnManager>().maxActionPoints;
//TOMAYBEDO Reduce the hero's action points if they are "Fatigued", or have some other negative status effect
		}

		EnableHeroSelection();

		print("Starting Player Turn");
	}


	public void EnableHeroSelection () {
		heroesSelectable = true;
	}


	public void ContinuePlayerTurn() {
		EnableHeroSelection();
	}


	public void EndPlayerTurn () {
		StartEnemyTurn();
	}

//ENEMY TURN FUNCTIONS

	public void StartEnemyTurn() {
		myTurn = whoseTurn.enemyTurn;
		ASUIM.UpdateTurnText(myTurn);

		print("Starting Enemy Turn");

		EndEnemyTurn();
	}


	void EndEnemyTurn() {
		StartPlayerTurn();
	}

	
	public List<GameObject> DetermineMoveSpaces(GameObject originHex, int range) {
		List<GameObject> moveHexes = new List<GameObject>();
		List<GameObject> spaces = new List<GameObject>();

		moveHexes.Add(originHex);
		//spaces.Add(originHex);

		for (int h = 0; h < range; h++) {
			foreach (GameObject hex in moveHexes) {
				foreach (GameObject neighbor in hex.GetComponent<Arena_HexScript>().neighbors) {
					if (!spaces.Contains(neighbor) && !neighbor.GetComponent<Arena_HexScript>().occupied && neighbor.GetComponent<Arena_HexScript>().traversable) {
						spaces.Add(neighbor);
						moveHexes = new List<GameObject>(spaces);
						//moveHexes.Add(neighbor);
						//moveHexes.Clear();
						//for (int i = 0; i < spaces.Count; i++) {
						//	moveHexes.Add(spaces[i]);
						//}
					}
				}
			}
		}

		foreach (GameObject space in spaces) {
			space.GetComponent<MeshRenderer>().material = space.GetComponent<Arena_HexScript>().moveMat;
		}

		return spaces;
	}


	public List<GameObject> DetermineEnemiesInRange(GameObject originHex, int minRange, int maxRange) {
		List<GameObject> attackHexes = new List<GameObject>();
		List<GameObject> targets = new List<GameObject>();

		attackHexes.Add(originHex);

		for (int r = minRange; r <= maxRange; r++) {
			foreach (GameObject hex in attackHexes) {
				foreach (GameObject neighbor in hex.GetComponent<Arena_HexScript>().neighbors) {
					if (neighbor.GetComponent<Arena_HexScript>().occupant != null &&
						neighbor.GetComponent<Arena_HexScript>().occupant.GetComponent<Activity_EnemyTurnManager>() != null &&
						neighbor.GetComponent<Arena_HexScript>().occupant.GetComponent<Activity_EnemyTurnManager>().isTargetable) {
						targets.Add(neighbor.GetComponent<Arena_HexScript>().occupant);
					}
				}
			}
		}

		foreach (GameObject target in targets) {
//TODO Use a shader to highlight relevant enemies
			target.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.magenta;
		}

		return targets;
	}
}
