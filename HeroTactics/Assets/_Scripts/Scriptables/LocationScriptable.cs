using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LocationScriptable", menuName = "Scriptables/EventScripts/LocationScript", order = 0)]
public class LocationScriptable : ScriptableObject {

	public int locationSceneIndex;
	public string locationName;

	public Image locationImage;

	public string[] locationClues;
	
}
