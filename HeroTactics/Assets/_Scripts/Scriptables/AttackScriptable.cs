using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackScriptable", menuName = "Scriptables/EventScripts/AttackScript", order = 0)]
public class AttackScriptable : ScriptableObject {

	public enum attackTypes{Arson, BombThreat, ChemicalAttack, HostageSituation, Kidnapping, MassShooting, Robbery};
	public attackTypes myAttackType;

	public string[] attackClues;


	
	void Start() {
        
    }


    void Update() {
        
    }
}
