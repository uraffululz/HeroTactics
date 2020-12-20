using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity_EnemyTurnManager : MonoBehaviour {

	ActivityManager actMan;

	int actionPoints;

	int moveRange = 3;

	int attackRange = 1;
	[SerializeField] GameObject aggroTarget;


	
	void Start() {
        
    }


    void Update() {
        
    }


	public void DetermineAction () {
		if (aggroTarget != null) {

		}
		else {

		}
	}


	List<GameObject> DetermineMoveChoices() {
		List<GameObject> choices = new List<GameObject>();



		return choices;
	}


	public void InitiateMove() {

	}


	public void InitiateAttack() {
		Attack(aggroTarget);
	}


	public void Attack(GameObject target) {

	}
}
