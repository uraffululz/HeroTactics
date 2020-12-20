using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class HeroCanvasManager : MonoBehaviour {

	Camera mainCam;

	[SerializeField] GameObject myUnit;

	[SerializeField] Image hpBar;

	[SerializeField] Button moveButton;
	[SerializeField] Button attackButton;
	

	//CombatManager combatManager;

	//[SerializeField] HealthScript hpScript;

 
	void Awake() {
        myUnit = transform.parent.gameObject;
		mainCam = Camera.main;
		//combatManager = GameObject.FindGameObjectWithTag("ArenaSceneManager").GetComponent<CombatManager>();
    }


    void Update() {
        transform.LookAt(transform.position - mainCam.transform.position, Vector3.up);

		
    }


	//public void InitiatePlayerAttack (GameObject enemyAttacked) { //Called by the enemy canvas's Attack Button
	//	combatManager.targetedEnemy = enemyAttacked;
	//	//combatManager.activeHero.GetComponent<Combat_PlayerMove>().target = combatManager.targetedEnemy;
	//	combatManager.activeHero.GetComponent<Combat_PlayerMove>().StartCoroutine(combatManager.activeHero.GetComponent<Combat_PlayerMove>().Attack());
	//}


	//public void InitiateEnemyAttack (GameObject playerAttacked) {
	//	combatManager.targetedHero = playerAttacked;
	//	//target = playerAttacked;
	//	combatManager.activeEnemy.GetComponent<Combat_EnemyMove>().StartCoroutine(combatManager.activeEnemy.GetComponent<Combat_EnemyMove>().Attack());
	//}


	public void UpdateHealthBar(int maxHP, int currentHP) {
		hpBar.fillAmount = (float) currentHP / maxHP;
	}


	public void EnableActionButtons() {
		moveButton.gameObject.SetActive(true);

		////if (combatManager.currentCombatState == combatStates.PlayerTurn) {
		//	//foreach (var hero in combatManager.heroes) {
		//		foreach (var enemy in combatManager.enemies) {
		//			//print(Vector3.Distance(hero.transform.position, enemy.transform.position));
		//			if (combatManager.activeHero.GetComponent<Combat_PlayerMove>().remainingActionPoints >= combatManager.activeHero.GetComponent<Combat_PlayerMove>().costToAttack
		//			&& Vector3.Distance(combatManager.activeHero.transform.position, enemy.transform.position) <= combatManager.activeHero.GetComponent<NavMeshAgent>().stoppingDistance + 1) {
		//				print("Player close enough to attack");
		//				enemy.GetComponent<Combat_EnemyMove>().myCanvasManager.attackButton.gameObject.SetActive(true);
		//			}
		//			else {
		//				//print("Player not close enough to attack");
		//				enemy.GetComponent<Combat_EnemyMove>().myCanvasManager.attackButton.gameObject.SetActive(false);
		//			}
		//		}
		//	//}
		////}
		////else if (combatManager.currentCombatState == combatStates.EnemyTurn) {

		////}

		////if (myUnit.GetComponent<NavMeshAgent>() != null && myUnit.GetComponent<NavMeshAgent>().remainingDistance <= myUnit.GetComponent<NavMeshAgent>().stoppingDistance + 1) {
		////	print(myUnit.GetComponent<NavMeshAgent>().remainingDistance);
		////	attackButton.gameObject.SetActive(true);
		////}
		////else {
		////	attackButton.gameObject.SetActive(false);
		////}
	}


	public void DisableActionButtons() {
		moveButton.gameObject.SetActive(false);
		attackButton.gameObject.SetActive(false);
	}
}
