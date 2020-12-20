using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Activity_HeroTurnManager : MonoBehaviour {

	[SerializeField] ActivityManager actMan;

	Activity_HeroMove moveScript;

	[SerializeField] HeroCanvasManager myCanvas;

	public bool playerTurnComplete;
	public bool heroTurnComplete;

	public int maxActionPoints;
	public int actionPoints;


	void Awake () {
		moveScript = GetComponent<Activity_HeroMove>();
	}


	void Start() {
        
    }


    void Update() {
        
    }


	public void StartHeroTurn () {
		//GetComponent<NavMeshAgent>().enabled = true;
		moveScript.currentAction = Activity_HeroMove.Actions.idle;
		moveScript.hasMoved = false;

		playerTurnComplete = false;
		heroTurnComplete = false;

		if (actionPoints > 0) {
			myCanvas.EnableActionButtons();
		}

		print("Starting Hero Turn");
	}


	public void EndHeroTurn () {
		heroTurnComplete = true;

		foreach (GameObject hero in actMan.heroes) {
			if (!hero.GetComponent<Activity_HeroTurnManager>().heroTurnComplete) {
//TODO Allow the player to select any hero with remaining action points
//ALTERNATIVELY Allow selection of ANY hero, in order to at least see their stats, even if all action points are spent
				actMan.StartPlayerTurn();
				
			}
			else {
				actMan.EndPlayerTurn();
			}
		}
	}

	
}
