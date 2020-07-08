using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum playerCombatStates { PlayerStart, PlayerIdle, PlayerMoving, PlayerAttacking };


public class Combat_PlayerMove : MonoBehaviour {
	Animator heroAnim;
	NavMeshAgent heroNav;
	public GameObject target;
	Vector3 targetPos;

	[SerializeField] playerCombatStates currentHeroState;

	int totalActionPoints = 6;
	public int remainingActionPoints;
	bool actionChosen = false;
	public int costToMove {get; private set;} = 2;
	public int costToAttack {get; private set;} = 3;
	//bool canIKickIt = true;

	CombatManager combatManager;
	public UnitCanvasManager myCanvasManager;

	[SerializeField] GameObject moveRangeIndicator;


	void Awake () {
		heroAnim = GetComponent<Animator>();
		heroNav = GetComponent<NavMeshAgent>();

		combatManager = GameObject.FindGameObjectWithTag("ArenaSceneManager").GetComponent<CombatManager>();

		remainingActionPoints = totalActionPoints;
		//costToMove = 1;
		//costToAttack = 3;

		target = GameObject.FindGameObjectWithTag("Enemy");

	}


	void Start() {
		

	}


	void Update() {
		if (combatManager.currentCombatState == combatStates.PlayerTurn) {
			if (Input.GetMouseButtonDown(0)) {
				Ray moveRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hitLoc;
				bool hitLocValid = false;

				if (Physics.Raycast(moveRay, out hitLoc, 30f) && currentHeroState == playerCombatStates.PlayerIdle) {
					if (moveRangeIndicator.activeSelf == true && moveRangeIndicator.GetComponent<Collider>().bounds.Contains(hitLoc.point) && actionChosen) {
						if (target != null && target.name == "TargetEmpty_Player") {
							Destroy(target);
						}

						if (hitLoc.collider.CompareTag("Enemy")) {
							target = hitLoc.collider.gameObject;
						}
						else {
							GameObject targetEmpty = new GameObject("TargetEmpty_Player");
							targetEmpty.transform.position = new Vector3(hitLoc.point.x, gameObject.transform.position.y, hitLoc.point.z);
							target = targetEmpty;
						}
						
						hitLocValid = true;
					}

					if (hitLocValid) {
						StartCoroutine(ConfirmMove());
					}
					else {
						//print("Movement location invalid");
					}
				}
			}

			transform.LookAt(targetPos, Vector3.up);

			//if (remainingActionPoints >= costToAttack && target.layer == 8 && currentHeroState != playerCombatStates.PlayerAttacking && canIKickIt) {
			//	if (target.GetComponent<Combat_EnemyMove>() != null) {
			//		/*target.GetComponent<Combat_EnemyMove>().myCanvasManager.*/myCanvasManager.EnableActionButtons();
			//	}
			//}
			//else {
			//	currentHeroState = playerCombatStates.PlayerIdle;
			//}
		}
	}


	public void StartPlayerTurn() {
		remainingActionPoints = totalActionPoints;
	}


	public void Move() {
		actionChosen = true;
		myCanvasManager.DisableActionButtons();
		moveRangeIndicator.SetActive(true);
		moveRangeIndicator.transform.localScale = new Vector3(PlayerStats.heroMoveRange, 2, PlayerStats.heroMoveRange);//Vector3.one * PlayerStats.heroMoveRange;
	}


	IEnumerator ConfirmMove() {
		remainingActionPoints -= costToMove;

		moveRangeIndicator.SetActive(false);

		targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
		heroNav.destination = targetPos;

		currentHeroState = playerCombatStates.PlayerMoving;

		if (target != null) {
			yield return new WaitUntil(() => Vector3.Distance(transform.position, target.transform.position) <= heroNav.stoppingDistance + 1);
		}
		else {
			yield return new WaitForEndOfFrame();
		}

		print("Arrived at my destination");

		//yield return new WaitForSeconds(.5f);
		
		EndAction();
	}


	public IEnumerator Attack() {
		//canIKickIt = false;
		remainingActionPoints -= costToAttack;
		target = combatManager.targetedEnemy;
		currentHeroState = playerCombatStates.PlayerAttacking;
		heroAnim.SetBool("Attacking", true);
		
		yield return new WaitForSeconds(.8f);
		heroAnim.SetBool("Attacking", false);
		DamageTarget(10);

		EndAction();
	}


	void DamageTarget(int damage) {
		if (target != null) {
			if (target.GetComponent<HealthScript>() != null) {
				target.GetComponent<HealthScript>().TakeDamage(damage);
			}
			else {
				print ("Target cannot take damage");
			}
		}
		else {
			print("I don't have a target");
		}
	}


	void EndAction () {
		currentHeroState = playerCombatStates.PlayerIdle;

		if (remainingActionPoints <= 0) {
			remainingActionPoints = 0;
			EndHeroTurn();
		}
		else {
			print("You still have action points remaining");
			myCanvasManager.EnableActionButtons();
		}

		//canIKickIt = true;
		actionChosen = false;

		StopAllCoroutines();
	}


	void EndHeroTurn() {
		print("Ending hero turn");
//TODO This will end the active hero's turn. If all heroes' turns are finished, then end the player's turn wholly
		heroNav.enabled = false;
		GetComponent<NavMeshObstacle>().enabled = true;

		EndPlayerTurn();
	}


	void EndPlayerTurn() {
		myCanvasManager.DisableActionButtons();
		combatManager.StartEnemyTurn();
	}
}
