using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDetails : MonoBehaviour {

	[SerializeField] NodeManager nodeMan;

	public LocationScriptable myLocationScript;
	public GameObject[] neighborNodes; //To be used during gang wars, to determine nodes within proximity to be usurped
	public GangScriptable activeGangScript;
	public bool controllingGangIsStatic; //To be used during gang wars, to make sure that each gang's "home node" can't be usurped
	public GangScriptable controllingGangScript;
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
		else if (myStatus == "Gang War") {
			activeGangScript = nodeMan.gangWarAttackers;

			GetComponent<MeshRenderer>().material.color = nodeMan.gangWarNodeColor;

			print("Gang war at the " + myLocationScript.locationName + ": " + activeGangScript.myGang + " vs " + controllingGangScript.myGang);

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


	void GangWar() {

	}
}
