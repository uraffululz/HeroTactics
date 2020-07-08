using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GangScriptable", menuName = "Scriptables/EventScripts/GangScript", order = 0)]
public class GangScriptable : ScriptableObject {

	public enum gangs { Eldritch, Jackals };
	public gangs myGang;
	public Color gangColor;

	//ScriptableObject gangLeader;

	public string[] gangClues;

	public EnemyScriptable[] level1Enemies;
	
	
}
