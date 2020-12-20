using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

	private int totalHealth {get; set;}
	[SerializeField] int currentHealth;

	//CombatManager combatManager;
	//[SerializeField] UnitCanvasManager myCanvasManager;


    void Start() {
		//combatManager = GameObject.FindGameObjectWithTag("ArenaSceneManager").GetComponent<CombatManager>();
		totalHealth = 30;
        currentHealth = totalHealth;
    }


    void Update() {
        
    }


	public void TakeDamage(int damageDealt) {
		currentHealth -= damageDealt;

		if (currentHealth <= 0) {
			currentHealth = 0;
			//print("I'm dead");
			//StartCoroutine(Die(gameObject.tag));
		}
		else {

		}

		//if (gameObject.CompareTag("Enemy")) {
		//	gameObject.GetComponent<Combat_EnemyMove>().myCanvasManager.UpdateHealthBar(totalHealth, currentHealth);
		//}
		//else if (gameObject.CompareTag("Player")) {
		//	gameObject.GetComponent<Combat_PlayerMove>().myCanvasManager.UpdateHealthBar(totalHealth, currentHealth);
		//}
	}


	//IEnumerator Die(string unitTag) {
	//	yield return new WaitForSeconds(.2f);

	//	if (unitTag == "Enemy") {
	//		combatManager.enemies.Remove(gameObject);
	//	}
	//	else if (unitTag == "Player") {
	//		combatManager.heroes.Remove(gameObject);
	//	}

	//	if (combatManager.enemies.Count <= 0) {
	//		combatManager.WinCombat();
	//	}
	//	else if (combatManager.heroes.Count <= 0) {
	//		combatManager.LoseCombat();
	//	}

	//	Destroy(gameObject);
	//}
}
