using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour {

	Vector3 reachRayOffset;
	float moveSpeed = 2.5f;
	float runSpeed = 4;
	Vector3 towardPos;


    void Start() {
        reachRayOffset = new Vector3(0, .5f, 0);
    }


	void Update() {
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
			GetComponent<Animator>().SetBool("PlayerWalk", true);
			Move();
		}
		else {
			GetComponent<Animator>().SetBool("PlayerWalk", false);
		}

		ReachOut();
	}


	void Move () {
		bool running = Input.GetKey(KeyCode.LeftShift);

		//transform.Translate(towardPos - transform.position * Time.deltaTime, Space.World);
		transform.position +=  Vector3.right * Input.GetAxis("Horizontal") * ((running) ? runSpeed : moveSpeed) * Time.deltaTime;
		transform.position += Vector3.forward * Input.GetAxis("Vertical") * ((running) ? runSpeed : moveSpeed) * Time.deltaTime;
		//transform.position += towardPos;

		towardPos = new Vector3(transform.position.x + ((Input.GetAxis("Horizontal") * moveSpeed)), 0f, transform.position.z + ((Input.GetAxis("Vertical") * moveSpeed)));

//TODO Rotate faster
		transform.LookAt(towardPos);
	}


	void ReachOut() {
		RaycastHit reached;
		Debug.DrawLine(transform.localPosition + reachRayOffset, transform.position + reachRayOffset + transform.forward * .7f/*TransformDirection(Vector3.forward * .7f)*/, Color.red);
		if (Physics.Raycast(transform.localPosition + reachRayOffset, transform.position + reachRayOffset + transform.forward/*TransformDirection(Vector3.forward)*/, out reached, .7f)) {
			print(reached.collider.gameObject.name);
			if (Input.GetKeyDown(KeyCode.E)) {
				if (reached.collider.CompareTag("HideoutExit")) {
					SceneManager.LoadScene(2);
					//print("Loading map scene");
				}
				else if (reached.collider.CompareTag("ClueSolver")) {
					if (ClueMaster.eventOngoing) {
						GameObject.Find("SceneManager").GetComponent<HideoutSceneUIManager>().OpenClueUI();
					}
					else {
//TODO If there is no event ongoing, what happens if the player tries to open the clue-solving UI?
					}
				}
			}
		}
	}
}
