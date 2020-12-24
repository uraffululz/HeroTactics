using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity_EnemyMove : MonoBehaviour {

	[SerializeField] ActivityManager actMan;

	GameObject currentHex;




	void Awake () {
		currentHex = actMan.enemySpawnHex;
		currentHex.GetComponent<Arena_HexScript>().occupied = true;
		currentHex.GetComponent<Arena_HexScript>().occupant = this.gameObject;
	}


	void Start() {
        
    }


    void Update() {
        
    }
}
