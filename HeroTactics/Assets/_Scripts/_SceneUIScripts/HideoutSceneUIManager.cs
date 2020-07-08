using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideoutSceneUIManager : MonoBehaviour {

	[SerializeField] GameObject clueUI;
	
	void Start() {
        
    }


    void Update() {
        
    }


	public void OpenClueUI() {
		clueUI.SetActive(true);
	}


	public void CloseUI(GameObject UIToClose) {
		UIToClose.SetActive(false);
	}
}
