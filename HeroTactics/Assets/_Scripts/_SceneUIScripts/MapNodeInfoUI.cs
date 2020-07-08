using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNodeInfoUI : MonoBehaviour {

	public string currentLocation;
	public GangScriptable.gangs currentGang;
	public string currentStatus;
	public int currentDifficulty;

	[SerializeField] Text locationText;
	[SerializeField] Text gangText;
	[SerializeField] Text statusText;
	[SerializeField] Text difficultyText;


	void OnEnable () {
		locationText.text = "Location: " + currentLocation;
		gangText.text = "Gang: The " + currentGang;
		statusText.text = "Status: " + currentStatus;

		string difficultyStars = "<size=18><b><color=#ff0000ff>";
		for (int i = 1; i <= currentDifficulty; i++) {
			difficultyStars += "X";
		}
		difficultyStars += "</color></b></size>";
		difficultyText.text = "Difficulty: " + difficultyStars + "";
	}
}
