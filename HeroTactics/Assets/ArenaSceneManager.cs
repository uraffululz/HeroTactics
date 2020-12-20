using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaSceneManager : MonoBehaviour {

	[SerializeField] Vector3 arenaScale;


	void Awake () {
		//arenaBounds = GameObject.CreatePrimitive(PrimitiveType.Cube);
		//arenaBounds.transform.localScale = new Vector3(arenaScale.x, 1, arenaScale.z);

		GameObject[] hexes = GameObject.FindGameObjectsWithTag("Hex");

		foreach (GameObject hex in hexes) {
			if (hex.transform.position.x > arenaScale.x || hex.transform.position.z > arenaScale.z) { //!hex.GetComponent<BoxCollider>().bounds.Intersects(arenaBounds.GetComponent<BoxCollider>().bounds)) {
				hex.SetActive(false);
			}
		}

		//Destroy(arenaBounds);
	}


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
