using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDetails : MonoBehaviour {

	public LocationScriptable myLocationScript;
	public GangScriptable activeGangScript;
	public GangScriptable controllingGangScript;
	//GangScriptable currentEventGangScript;
	//public Color myColor;

	public string myStatus;
	public int difficulty;

	[SerializeField] MapSceneManager mapSceneManager;
	MapSceneUIManager MSUIM;

	
	void Awake() {
		MSUIM = mapSceneManager.GetComponent<MapSceneUIManager>();
    }


	void Start () {
		if (ClueMaster.eventOngoing && ClueMaster.eventSolved && myLocationScript == ClueMaster.currentEventLocation) {
			activeGangScript = ClueMaster.currentEventGang;
			GetComponent<MeshRenderer>().material.color = Color.yellow;

			myStatus = ClueMaster.currentEventAttack.myAttackType.ToString();
			difficulty = Random.Range(4, 6);
		}
		else {
			activeGangScript = controllingGangScript;
			GetComponent<MeshRenderer>().material.color = controllingGangScript.gangColor;

//TODO Set the status of non-event location nodes to smaller crimes, like a mugging or vandalism?
//There can't possibly be multiple arsons/bomb threats/hostage situations/etc happening EVERY night, right?
			myStatus = "Small crimes";

			difficulty = Random.Range(1, 4);
		}
	}


	void Update() {
        
    }

	void OnMouseDown () {
		mapSceneManager.locationSceneToLoad = myLocationScript.locationSceneIndex;
		MSUIM.UpdateNodeInfoUI(this);
	}
}
