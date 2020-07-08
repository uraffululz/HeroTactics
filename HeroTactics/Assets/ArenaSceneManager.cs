using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaSceneManager : MonoBehaviour {

	

 
	void Start() {
        
    }


    void Update() {
		
    }


	public void ReturnToHideout() {
		SceneManager.LoadScene(1);
	}


	public void ReturnToMap() {
		SceneManager.LoadScene(2);
	}
}
