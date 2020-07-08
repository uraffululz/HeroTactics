using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum combatStates {PlayerTurn, EnemyTurn, Win, Lose, NotInCombat};


public class CombatManager : MonoBehaviour {

	public combatStates currentCombatState;

	[SerializeField] public List<GameObject> heroes;
	[SerializeField] public List<GameObject> enemies;

	public GameObject activeHero;
	public GameObject targetedHero;
	public int activeEnemyIndex = 0;
	public GameObject activeEnemy;
	public GameObject targetedEnemy;

	[Space]

	ArenaSceneUIManager arenaUIManager;
	ArenaSceneEventManager arenaEventManager;


	void Awake () {
		arenaUIManager = GetComponent<ArenaSceneUIManager>();
		arenaEventManager = GetComponent<ArenaSceneEventManager>();

		heroes = new List<GameObject>();
		enemies = new List<GameObject>();

		heroes.AddRange(GameObject.FindGameObjectsWithTag("Player"));
		enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

		activeHero = heroes[0];
		activeEnemy = enemies[0];
	}


	void Start() {
        ChooseFirstTurn();
    }


    void Update() {
        
    }


	void ChooseFirstTurn() {
		//StartPlayerTurn();
		StartEnemyTurn();
	}


	public void StartPlayerTurn() {
		currentCombatState = combatStates.PlayerTurn;
		arenaUIManager.UpdateTurnText(currentCombatState);

		foreach (GameObject hero in heroes) {
			hero.GetComponent<NavMeshObstacle>().enabled = false;
			hero.GetComponent<NavMeshAgent>().enabled = true;
			hero.GetComponent<Combat_PlayerMove>().myCanvasManager.EnableActionButtons();
			hero.GetComponent<Combat_PlayerMove>().StartPlayerTurn();
		}
	}


	public void StartEnemyTurn() {
		currentCombatState = combatStates.EnemyTurn;
		arenaUIManager.UpdateTurnText(currentCombatState);

		foreach (GameObject enemy in enemies) {
			enemy.GetComponent<Combat_EnemyMove>().myCanvasManager.DisableActionButtons();
		}

		activeEnemyIndex = 0;
		activeEnemy = enemies[activeEnemyIndex];
		activeEnemy.GetComponent<Combat_EnemyMove>().StartTurn();
	}


	public void PlayerAttackEnemy() {

	}


	public void WinCombat() {
		print ("You won the combat");
		arenaEventManager.Win();
		arenaUIManager.UpdateWinUI();
		arenaUIManager.winUI.SetActive(true);
	}


	public void LoseCombat() {
		print ("You lost the combat");
		arenaEventManager.Lose();
		arenaUIManager.UpdateLoseUI();
		arenaUIManager.loseUI.SetActive(true);
	}
}
