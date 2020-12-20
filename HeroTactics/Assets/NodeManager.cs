using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour {

	[SerializeField] GameObject[] locationNodes;
	public Color gangWarNodeColor;
	public Color highTierNodeColor;

	public GangScriptable gangWarAttackers;

    void Awake() {
		for (int i = 0; i < locationNodes.Length; i++) {
			locationNodes[i].GetComponent<NodeDetails>().myStatus = "";
		}
		GangWarStart();
	}

	void GangWarStart () {
		GameObject gangWarStartNode = locationNodes[Random.Range(0, locationNodes.Length)];
		NodeDetails startNodeDeets = gangWarStartNode.GetComponent<NodeDetails>();
		gangWarAttackers = startNodeDeets.controllingGangScript;

		List<GameObject> gangWarTurfNodes = new List<GameObject>();

		for (int i = 0; i < startNodeDeets.neighborNodes.Length; i++) {
			GameObject gangWarTurfOption = startNodeDeets.neighborNodes[i];

			if (gangWarAttackers != gangWarTurfOption.GetComponent<NodeDetails>().controllingGangScript) {
				gangWarTurfNodes.Add(gangWarTurfOption);
			}
		}

		if (gangWarTurfNodes.Count > 0) {
			GameObject gangWarLocationNode = gangWarTurfNodes[Random.Range(0, gangWarTurfNodes.Count)];

			//Update the map to impose a "Gang War" activity at the gangWarLocationNode
			gangWarLocationNode.GetComponent<NodeDetails>().myStatus = "Gang War";
		}
		else {
			print("No gang war tonight. All nodes near " + gangWarStartNode.GetComponent<NodeDetails>().myLocationScript.locationName +
				" are owned by the " + gangWarAttackers.myGang);
		}
	}

	void Update() {
        
    }
}

