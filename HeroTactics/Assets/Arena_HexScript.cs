using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena_HexScript : MonoBehaviour {

	public Material baseMat;
	public Material moveMat;

	public List<GameObject> neighbors;

	public bool traversable;
	public bool occupied;
	public GameObject occupant;
 
	
	void Awake() {
		GetComponent<MeshRenderer>().material = baseMat;

		SetupNeighbors();
	}


	void Update() {
        
    }


	void SetupNeighbors () {
		Collider[] neighborColliders = Physics.OverlapBox(transform.position, transform.localScale / 100, Quaternion.identity);

		for (int i = 0; i < neighborColliders.Length; i++) {
			if (neighborColliders[i].CompareTag("Hex") && neighborColliders[i] != this.gameObject.GetComponent<Collider>()) {
				neighbors.Add(neighborColliders[i].gameObject);
			}
		}
	}
}
