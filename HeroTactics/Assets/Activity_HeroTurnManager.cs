using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Activity_HeroTurnManager : MonoBehaviour {

	[SerializeField] ActivityManager actMan;

	Activity_HeroMove moveScript;

	public HeroCanvasManager myCanvas;
	public Animator anim;

	public enum Actions { none, idle, moving, attacking };
	public Actions currentAction;

	//public bool playerTurnComplete;
	public bool heroTurnComplete;

	public GameObject currentHex;


	public int maxActionPoints;
	public int actionPoints;

	public int moveCost = 1;
	
	public int attackCost = 2;


	void Awake () {
		moveScript = GetComponent<Activity_HeroMove>();
	}


	void Start() {
        
    }


    void Update() {
        
    }


	public void StartHeroTurn () {
		//GetComponent<NavMeshAgent>().enabled = true;
		currentAction = Actions.idle;
		moveScript.hasMoved = false;

		//playerTurnComplete = false;
		heroTurnComplete = false;

		SetupActionUI();

		//if (actionPoints > 0) {
		//	myCanvas.EnableActionButtons();
		//}

		print("Starting Hero Turn");
	}


	public void EndHeroAction() {
		if (actionPoints > 0) {
			SetupActionUI();
		}
		else {
			EndHeroTurn();
		}
	}


	public void SetupActionUI() {
		bool hasMovePoints = false;
		bool hasAttackPoints = false;

		if (actionPoints >= moveCost) {
			hasMovePoints = true;
		}

		if (actionPoints >= attackCost) {
			hasAttackPoints = true;
		}

		myCanvas.EnableActionButtons(hasMovePoints, hasAttackPoints);
	}


	public void EndHeroTurn () {
		currentAction = Actions.idle;

		heroTurnComplete = true;
		bool playerTurnComplete = true;

		foreach (GameObject hero in actMan.heroes) {
			if (!hero.GetComponent<Activity_HeroTurnManager>().heroTurnComplete) {
				playerTurnComplete = false;
			}
		}

		if (!playerTurnComplete) {
			actMan.ContinuePlayerTurn();
		}
		//TODO Allow the player to select any hero with remaining action points
		//ALTERNATIVELY Allow selection of ANY hero, in order to at least see their stats, even if all action points are spent
		else {
			actMan.EndPlayerTurn();
		}
	}

	
}
