using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSceneUIManager : MonoBehaviour {

	[SerializeField] MapNodeInfoUI nodeInfoUIScript;


	void Start() {
        
    }


    void Update() {
        
    }


	


	public void CloseUI (GameObject UIToClose) {
		UIToClose.SetActive(false);
	}


	public void UpdateNodeInfoUI (NodeDetails deets) {
		nodeInfoUIScript.currentLocation = deets.myLocationScript.locationName;
		nodeInfoUIScript.currentGang = deets.activeGangScript.myGang;
		nodeInfoUIScript.currentStatus = deets.myStatus;
		nodeInfoUIScript.currentDifficulty = deets.difficulty;

		nodeInfoUIScript.gameObject.SetActive(true);
	}
}
