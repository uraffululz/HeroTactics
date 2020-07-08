using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Scriptables/Enemies", order = 0)]
public class EnemyScriptable : ScriptableObject {

	public string enemyName;
	public GangScriptable.gangs myGang;

	public int health;
	public int strength;
	public int agility;
	public int defense;

	public int moveRange;
	
	
	void Start() {
        
    }


    void Update() {
        
    }
}
