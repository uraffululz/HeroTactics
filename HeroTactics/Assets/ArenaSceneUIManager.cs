﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaSceneUIManager : MonoBehaviour {

	public GameObject introUI;

	[SerializeField] Text turnText;

	public GameObject winUI;
	[SerializeField] Text recentClueText;

	public GameObject loseUI;

	ArenaSceneEventManager arenaEventManager;
	
	void Start() {
        arenaEventManager = GetComponent<ArenaSceneEventManager>();
    }


    void Update() {
        
    }

	public void UpdateTurnText (ActivityManager.whoseTurn turn) {
		switch (turn) {
			case ActivityManager.whoseTurn.playerTurn:
				turnText.text = "<Color=#0000ffff>PLAYER TURN</color>";
				break;
			case ActivityManager.whoseTurn.enemyTurn:
				turnText.text = "<Color=#ff0000ff>ENEMY TURN</color>";
				break;
			default:
				break;
		}
	}


	public void UpdateWinUI() {
		recentClueText.text = "Clue Found: " + arenaEventManager.mostRecentClue;
	}


	public void UpdateLoseUI() {

	}
}
