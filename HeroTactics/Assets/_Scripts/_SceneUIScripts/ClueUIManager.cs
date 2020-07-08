using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueUIManager : MonoBehaviour {

	[SerializeField] GameObject locationOptionUI;
	[SerializeField] GameObject gangOptionUI;
	[SerializeField] GameObject attackOptionUI;

	[Space]

	[SerializeField] Text locationClueText;
	[SerializeField] Text gangClueText;
	[SerializeField] Text attackClueText;

	[Space]

	[SerializeField] HideoutSceneUIManager HSUIM;

	


	void OnEnable () {
		//Clear any previously-turned-on toggles in the option menus
		locationOptionUI.GetComponent<ToggleGroup>().SetAllTogglesOff(sendCallback: true);
		gangOptionUI.GetComponent<ToggleGroup>().SetAllTogglesOff(sendCallback: true);
		attackOptionUI.GetComponent<ToggleGroup>().SetAllTogglesOff(sendCallback: true);

		UpdateClueTexts();
	}


	void Start() {

    }


    void Update() {
        
    }


	void UpdateClueTexts() {
		locationClueText.text = "";
		for (int lcf = 0; lcf < ClueMaster.currentLocationCluesFound; lcf++) {
			locationClueText.text += "-" + ClueMaster.locationCluesKnown[lcf] + "\n";
		}

		gangClueText.text = "";
		for (int gcf = 0; gcf < ClueMaster.currentGangCluesFound; gcf++) {
			gangClueText.text +=  "-" + ClueMaster.gangCluesKnown[gcf] + "\n";
		}

		attackClueText.text = "";
		for (int acf = 0; acf < ClueMaster.currentAttackCluesFound; acf++) {
			attackClueText.text += "-" + ClueMaster.attackCluesKnown[acf] + "\n";
		}

	}


	public void SelectLocation(LocationScriptable location) {
		ClueMaster.selectedLocation = location;
	}


	public void SelectGang(GangScriptable gang) {
		ClueMaster.selectedGang = gang;
	}


	public void SelectAttack(AttackScriptable attack) {
		ClueMaster.selectedAttack = attack;
	}


	public void Solve() {
		ClueMaster.SolveEvent();

		if (ClueMaster.eventSolved) {
			HSUIM.CloseUI(this.gameObject);
		}
		else {
//TODO If the player gets multiple chances to solve the event, then clear (and maybe temporarily disable) the selected toggles that were incorrect.
//If the player DOESN'T get multiple chances, the event is failed and the player is unable to try again.
		}
	}
	
}
