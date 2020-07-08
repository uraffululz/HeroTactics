using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaSceneEventManager : MonoBehaviour {

	[SerializeField] LocationScriptable[] locations;
	[SerializeField] GangScriptable[] gangs;
	[SerializeField] AttackScriptable[] attacks;

	public string mostRecentClue;



    void Start() {
        
    }


    void Update() {
		if (Input.GetKeyDown(KeyCode.W)) {
			Win();
		}
	}


	public void Win () {
		if (ClueMaster.eventOngoing) {
			if (ClueMaster.eventSolved) {
				print("You completed the event!");
				ClueMaster.ClearAllClues();
			}
			else {
				mostRecentClue = ClueMaster.GetAClue();
			}
		}
		else {
			ClueMaster.InitializeEvent(locations, gangs, attacks);
			mostRecentClue = ClueMaster.GetAClue();
		}
	}


	public void Lose () {

	}
}
