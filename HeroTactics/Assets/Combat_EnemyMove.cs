using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum enemyCombatStates { EnemyStart, EnemyIdle, EnemyMoving, EnemyAttacking };


public class Combat_EnemyMove : MonoBehaviour {
	[SerializeField] EnemyScriptable myEnemyScript;
	Animator myAnim;
	NavMeshAgent myNavAgent;
	public GameObject target;
	[SerializeField] Vector3 targetPos;

	[SerializeField] enemyCombatStates myCurrentState;

	int totalActionPoints = 3;
	[SerializeField] int remainingActionPoints;
	bool actionChosen = false;
	public int costToMove {get; private set;} = 1;
	public int moveRange { get; private set; }
	public int costToAttack {get; private set;} = 1;
	bool canIKickIt = false;
	bool canMove = true;
	[SerializeField] LineRenderer pathRenderer;
	bool bopIt = false;

	CombatManager combatManager;
	public UnitCanvasManager myCanvasManager;

 
	void Awake() {
		myAnim = GetComponent<Animator>();
		myNavAgent = GetComponent<NavMeshAgent>();
		target = GameObject.FindGameObjectWithTag("Player");
		targetPos = target.transform.position;

		moveRange = myEnemyScript.moveRange;

		combatManager = GameObject.FindGameObjectWithTag("ArenaSceneManager").GetComponent<CombatManager>();
		
		
		//StartTurn();
    }


    void Update() {
		//if (combatManager.currentCombatState == combatStates.EnemyTurn) {
			//	if (Input.GetMouseButtonUp(0)) {
			//		RaycastHit hitLoc;
			//		bool hitLocValid = false;

			//		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitLoc, 30f) && myCurrentState == enemyCombatStates.EnemyIdle) {
			//			if (!hitLoc.collider.CompareTag("Enemy")) {
			//				if (target.name == "TargetEmpty_Enemy") {
			//					Destroy(target);
			//				}

			//				hitLocValid = true;
			//			}

			//			if (hitLocValid) {
			//				if (hitLoc.collider.CompareTag("Player")) {
			//					print("Targeting the player");
			//					target = hitLoc.collider.gameObject;
			//				}
			//				else if (hitLoc.collider.CompareTag("Ground")) {
			//					GameObject targetEmpty = new GameObject("TargetEmpty_Enemy");
			//					targetEmpty.transform.position = new Vector3(hitLoc.point.x, gameObject.transform.position.y, hitLoc.point.z);
			//					target = targetEmpty;
			//				}

			//				if (myNavAgent.enabled) {
			//					//targetPos = targetEmpty.transform.position;
			//					//myNavAgent.destination = targetPos;

			//					StartTurn();
			//					//Destroy(targetEmpty);
			//				}

			//				actionChosen = true;
			//			}
			//			else {
			//				print("Target location invalid");
			//			}

			//		}
			//	}

			//	transform.LookAt(myNavAgent.nextPosition/*targetPos*/, Vector3.up);

			//}

			//if (myAnim.GetBool("isMyturn") == true) {
		//	if (remainingActionPoints > 0 && actionChosen) {
		//		if (Vector3.Distance(transform.position, target.transform.position) <= myNavAgent.stoppingDistance + 1f) {
		//			//print("Reached the target");
		//			myAnim.SetBool("Moving", false);

		//			if (remainingActionPoints >= costToAttack && target.layer == 8 && myCurrentState != enemyCombatStates.EnemyAttacking && canIKickIt) {
		//				//transform.LookAt(targetPos, Vector3.up);
		//				//StartCoroutine("Attack");
		//			}
		//			else {
		//				myCurrentState = enemyCombatStates.EnemyIdle;
		//				//myAnim.SetBool("Moving", false);
		//				//myAnim.SetBool("Attacking", false);
		//			}
		//		}
		//		else {
		//			myCurrentState = enemyCombatStates.EnemyMoving;
		//			myAnim.SetBool("Moving", true);
		//		}
		//	}
		//	else {
		//		myCurrentState = enemyCombatStates.EnemyIdle;
		//	}

		//}
	}


	public void StartTurn () {
		GetComponent<NavMeshObstacle>().enabled = false;
		myNavAgent.enabled = true;
		//actionChosen = false;

		remainingActionPoints = totalActionPoints;
		//myAnim.SetBool("isMyTurn", true);

		//myNavAgent.isStopped = false;
		
		//targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
		//myNavAgent.destination = targetPos;

		ChooseAction();
	}


	void ChooseAction () {
		//canMove = true;
		canIKickIt = true;
		actionChosen = false;

		print("Choosing an action for my turn");
//TODO Choose the enemy's action for the turn
		if (target != null) {
			if (Vector3.Distance(transform.position, target.transform.position) <= myNavAgent.stoppingDistance + 1) {
//TODO Make sure this doesn't work if the enemy and target have an obstruction between them
//For example, they could be within range, but be on opposite sides of a wall/obstacle, or on different floors of a building
				if (target.GetComponent<HealthScript>() != null && remainingActionPoints >= costToAttack) {
					//myCurrentState = enemyCombatStates.EnemyAttacking;
					StartCoroutine(Attack());
				}
				else {
					print("Cannot attack target");
				}

			}
			else {
				//if (canMove) {
					print("Out of range of target. Moving closer");

					StartCoroutine(SetMoveTarget());
				//}
				
			}
		}
		else {
			print("Acquiring target");

			myNavAgent.isStopped = true;
			//NavMeshPath pathToHero = new NavMeshPath();
			//myNavAgent.CalculatePath(combatManager.heroes[0].transform.position, pathToHero);
			//myNavAgent.destination = combatManager.heroes[0].transform.position;
			combatManager.targetedHero = combatManager.heroes[0];
			float distToHero = 100;

			//if (combatManager.heroes.Count > 1) {
			foreach (var hero in combatManager.heroes) {
				//myNavAgent.CalculatePath(hero.transform.position, pathToHero);
				myNavAgent.destination = hero.transform.position;

				if (myNavAgent.remainingDistance < distToHero) {
					distToHero = myNavAgent.remainingDistance;
					combatManager.targetedHero = hero;
				}
			}

			target = combatManager.targetedHero;
			targetPos = target.transform.position;
			myNavAgent.destination = targetPos;
			myNavAgent.isStopped = false;
			//Move();
			//}

			ChooseAction();
		}

		actionChosen = true;

	}


	IEnumerator SetMoveTarget () {
		yield return new WaitForEndOfFrame();

		canMove = false;
		//remainingActionPoints -= costToMove;
		myCurrentState = enemyCombatStates.EnemyMoving;
		//myNavAgent.isStopped = true;

		//NavMeshPath currentPathToTarget = new NavMeshPath();
		//myNavAgent.CalculatePath(target.transform.position, currentPathToTarget);
		//myNavAgent.SetPath(currentPathToTarget);
		myNavAgent.isStopped = true;
		targetPos = target.transform.position;
		myNavAgent.destination = targetPos;
		
		yield return new WaitForEndOfFrame();

		pathRenderer.positionCount = myNavAgent.path.corners.Length;
		pathRenderer.SetPositions(myNavAgent.path.corners);

		float distToTravel = myEnemyScript.moveRange;
		float accumulatedDistance = 0;
		///print("Distance to travel: " + distToTravel);
		//Determine how far to move, using corners of the current path to the destination
		print("Path corners: " + myNavAgent.path.corners.Length);

		for (int i = 1; i < myNavAgent.path.corners.Length; i++) {

			//Just for a visual representation of the enemy's path
			
			accumulatedDistance += Vector3.Distance(myNavAgent.path.corners[i - 1], myNavAgent.path.corners[i]);

			//print("Determining path point");
			//myNavAgent.destination = wayPointSphere.transform.position/*currentPathToTarget.corners[i]*/;


			if (accumulatedDistance/*Vector3.Distance(transform.position, myNavAgent.path.corners[i])/*myNavAgent.remainingDistance*/ <= /*myEnemyScript.moveRange*/distToTravel * remainingActionPoints) {
				//distToTravel = Vector3.Distance(transform.position, myNavAgent.path.corners[i]);
				///myNavAgent.CalculatePath(currentPathToTarget.corners[i], currentPathToTarget);
				//myNavAgent.SetPath(currentPathToTarget);
				//targetPos = myNavAgent.pathEndPosition;
				targetPos = myNavAgent.path.corners[i];
				//myNavAgent.destination = targetPos;
				///print("NavMeshAgent remaining distance: " + myNavAgent.remainingDistance + " || " + "Distance left to travel: " + distToTravel);
				//break;
			}
			else {
				print("Path corner too far away");
				accumulatedDistance -= Vector3.Distance(myNavAgent.path.corners[i - 1], myNavAgent.path.corners[i]);

				targetPos = myNavAgent.path.corners[i - 1];

				break;
			}
		}

		//myNavAgent.isStopped = false;

		yield return new WaitForEndOfFrame();

		print("Distance accumulated: " + accumulatedDistance + " | " + (distToTravel * remainingActionPoints));

		//float distToTargetPos = Vector3.Distance(transform.position, targetPos);
		//print("Distance to targetPos: " + distToTargetPos + " : " + distToTravel);
		myNavAgent.destination = targetPos;

		GameObject wayPointSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		wayPointSphere.transform.position = targetPos;

		print("Moving to location");

		myNavAgent.isStopped = false;

		yield return new WaitUntil(() => Vector3.Distance(transform.position, targetPos) <= myNavAgent.stoppingDistance);
		print("Location reached");
		//remainingActionPoints -= costToMove;

		yield return new WaitForEndOfFrame();

		EndAction(1 + (int)(accumulatedDistance / distToTravel));
		//StartCoroutine(Move(targetPos));
	}


	//IEnumerator Move(Vector3 moveToPos) {
	//	print("Moving to location");

	//	yield return new WaitUntil(() => Vector3.Distance(transform.position, moveToPos) <= myNavAgent.stoppingDistance + .5f);
	//	print("Location reached");
	//	remainingActionPoints -= costToMove;

	//	EndAction();
	//}


	public IEnumerator Attack() {
		canIKickIt = false;
		//remainingActionPoints -= costToAttack;
		combatManager.targetedHero = target;
		myCurrentState = enemyCombatStates.EnemyAttacking;
		myAnim.SetBool("Attacking", true);

		yield return new WaitForSeconds(.8f);
		myAnim.SetBool("Attacking", false);

		DamageTarget(10);


		//yield return new WaitForSeconds(.2f);
	}


	void DamageTarget(int damage) {
		if (target != null) {
			if (target.GetComponent<HealthScript>() != null) {
				target.GetComponent<HealthScript>().TakeDamage(damage);
			}
			else {
				print("Target cannot take damage");
			}
		}
		else {
			print("I don't have a target");
		}

		EndAction(costToAttack);
	}


	void EndAction(int actionCost) {
		StopAllCoroutines();

		remainingActionPoints -= actionCost;
		
		myCurrentState = enemyCombatStates.EnemyIdle;

		print("Action points used: " + actionCost + " | Remaining action points: " + remainingActionPoints);
		if (remainingActionPoints > 0) {
			ChooseAction();
		}
		else {
			remainingActionPoints = 0;
			EndIndividualEnemyTurn();
		}
		
		
	}


	void EndIndividualEnemyTurn() {
		myCanvasManager.DisableActionButtons();
		myNavAgent.enabled = false;
		GetComponent<NavMeshObstacle>().enabled = true;

		combatManager.activeEnemyIndex++;

		if (/*combatManager.enemies.Count > 1 && */combatManager.activeEnemyIndex < combatManager.enemies.Count) {
			combatManager.activeEnemy = combatManager.enemies[combatManager.activeEnemyIndex];
			combatManager.activeEnemy.GetComponent<Combat_EnemyMove>().StartTurn();
		}
		else {
			EndTurn();
		}
	}


	void EndTurn() {
		//myAnim.SetBool("isMyTurn", false);
		combatManager.StartPlayerTurn();

		//myNavAgent.isStopped = false;
		//myNavAgent.SetDestination(transform.position);
		//target = gameObject;
		//targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
 
		
	}
}
