using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSceneManager : MonoBehaviour {

	public int locationSceneToLoad;

	MapSceneUIManager UIManager;
	
	
	void Start() {
        UIManager = GetComponent<MapSceneUIManager>();
    }


    void Update() {
		//Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		//RaycastHit camHit;
		//if (Physics.Raycast(camRay, out camHit, 30)) {

		//	if (camHit.collider.CompareTag("MapNode")/* && Input.GetMouseButtonDown(0)*/) {
		//		print("Yup, that's a node");
		//		NodeDetails nodeDeets = camHit.collider.GetComponent<NodeDetails>();
		//		locationSceneToLoad = nodeDeets.myLocationScript.locationSceneIndex;

		//		UIManager.UpdateNodeInfoUI(nodeDeets);
		//	}
	}


	public void LoadNextScene() {
		SceneManager.LoadScene(locationSceneToLoad);
	}


	public void GoBackToHideout() {
		SceneManager.LoadScene(1);
	}

	
}
