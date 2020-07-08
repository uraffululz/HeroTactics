using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClueMaster {

	public static bool eventOngoing;
	public static bool eventSolved;

	//public static NodeScriptable[] eventLocations;
	public static LocationScriptable currentEventLocation;
	public static LocationScriptable selectedLocation;
	public static GangScriptable currentEventGang;
	public static GangScriptable selectedGang;
	public static AttackScriptable currentEventAttack;
	public static AttackScriptable selectedAttack;

	static int maxClues = 9;
	static int currentCluesFound = 0;
	static int maxLocationClues = 3;
	public static int currentLocationCluesFound = 0;
	static int maxGangClues = 3;
	public static int currentGangCluesFound = 0;
	static int maxAttackClues = 3;
	public static int currentAttackCluesFound = 0;

	static List<string> possibleLocationClues = new List<string>();
	static List<string> possibleGangClues = new List<string>();
	static List<string> possibleAttackClues = new List<string>();

	public static string[] locationCluesKnown = new string[3];
	public static string[] gangCluesKnown = new string[3];
	public static string[] attackCluesKnown = new string[3];

	
	static void Start() {
        
    }


    static void Update() {
        
    }


	public static void InitializeEvent(LocationScriptable[] locations, GangScriptable[] gangs, AttackScriptable[] attacks) {
		currentEventLocation = locations[Random.Range(0, locations.Length)];
		currentEventGang = gangs[Random.Range(0, gangs.Length)];
		currentEventAttack = attacks[Random.Range(0, attacks.Length)];

		possibleLocationClues.Clear();
		possibleGangClues.Clear();
		possibleAttackClues.Clear();

		for (int lc = 0; lc < currentEventLocation.locationClues.Length; lc++) {
			possibleLocationClues.Add(currentEventLocation.locationClues[lc]);
		}

		for (int gc = 0; gc < currentEventGang.gangClues.Length; gc++) {
			possibleGangClues.Add(currentEventGang.gangClues[gc]);
		}

		for (int ac = 0; ac < currentEventAttack.attackClues.Length; ac++) {
			possibleAttackClues.Add(currentEventAttack.attackClues[ac]);
		}

		eventOngoing = true;

		Debug.Log("Event Initialized: " + currentEventLocation.locationName + " | " + currentEventGang.myGang + " | " + currentEventAttack.myAttackType);

		//GetAClue();
	}


	public static string GetAClue() {
		string clueFound = "";

		if (currentCluesFound < maxClues) {
			List<int> cluesLeftToFind = new List<int>();
			if (currentLocationCluesFound < maxLocationClues) {
				cluesLeftToFind.Add(0);
			}

			if (currentGangCluesFound < maxGangClues) {
				cluesLeftToFind.Add(1);
			}

			if (currentAttackCluesFound < maxAttackClues) {
				cluesLeftToFind.Add(2);
			}

			int clueType = cluesLeftToFind[Random.Range(0, cluesLeftToFind.Count)];

			switch (clueType) {
				case 0: //Location clue
					clueFound = GetALocationClue();
					break;
				case 1: //Gang clue
					clueFound = GetAGangClue();
					break;
				case 2: //Attack clue
					clueFound = GetAnAttackClue();
					break;
				default:

					break;
			}

			currentCluesFound++;
			//Debug.Log("You got a clue"); 
			return clueFound;
		}
		else {
			Debug.Log("You have already found the max number of clues");
			return "Nope";
		}

	}


	static string GetALocationClue() {
		string locClueFound = "";
		int clueFound = Random.Range(0, possibleLocationClues.Count);

		locationCluesKnown[currentLocationCluesFound] = possibleLocationClues[clueFound];
		locClueFound = locationCluesKnown[currentLocationCluesFound];
		currentLocationCluesFound++;

		possibleLocationClues.RemoveAt(clueFound);

		//Debug.Log("You got a location clue");
		return locClueFound;
	}


	static string GetAGangClue() {
		string gangClueFound = "";
		int clueFound = Random.Range(0, possibleGangClues.Count);

		gangCluesKnown[currentGangCluesFound] = possibleGangClues[clueFound];
		gangClueFound = gangCluesKnown[currentGangCluesFound];
		currentGangCluesFound++;

		possibleGangClues.RemoveAt(clueFound);

		//Debug.Log("You got a gang clue");
		return gangClueFound;
	}


	static string GetAnAttackClue() {
		string attackClueFound = "";
		int clueFound = Random.Range(0, possibleAttackClues.Count);

		attackCluesKnown[currentAttackCluesFound] = possibleAttackClues[clueFound];
		attackClueFound = attackCluesKnown[currentAttackCluesFound];
		currentAttackCluesFound++;

		possibleAttackClues.RemoveAt(clueFound);

		//Debug.Log("You got an attack clue");
		return attackClueFound;
	}


	public static void SolveEvent () {
		bool locationCorrect = false;
		bool gangCorrect = false;
		bool attackCorrect = false;

		if (selectedLocation == currentEventLocation) {
			Debug.Log("You selected the correct location");
			locationCorrect = true;
		}
		else {
			Debug.Log("You selected the wrong location");
		}

		if (selectedGang == currentEventGang) {
			Debug.Log("You selected the correct gang");
			gangCorrect = true;
		}
		else {
			Debug.Log("You selected the wrong gang");
		}

		if (selectedAttack == currentEventAttack) {
			Debug.Log("You selected the correct attack");
			attackCorrect = true;
		}
		else {
			Debug.Log("You selected the wrong attack");
		}

		//Tally the player's correct/incorrect choices
		if (locationCorrect && gangCorrect && attackCorrect) {
			eventSolved = true;

			Debug.Log("You solved the event! Now go get 'em!");
		}
		else {
			Debug.Log("You failed the event! Better luck next time!");
		}
	}


	public static void ClearAllClues() {
		currentEventLocation = null;
		currentEventGang = null;
		currentEventAttack = null;

		currentCluesFound = 0;
		currentLocationCluesFound = 0;
		currentGangCluesFound = 0;
		currentAttackCluesFound = 0;

		for (int i = 0; i < 3; i++) {
			locationCluesKnown[i] = null;
			gangCluesKnown[i] = null;
			attackCluesKnown[i] = null;
		}

		eventOngoing = false;
		eventSolved = false;

	}
}
