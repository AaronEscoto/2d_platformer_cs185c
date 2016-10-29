using UnityEngine;
using System.Collections;

public class PlayerSimpleHealth : MonoBehaviour {

	bool died;

	void Awake() {
		died = false;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () { 
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(!died) {
			if(other.tag == "Enemy" || other.tag == "DeathTrigger") {
				died = true;
				Application.LoadLevel (Application.loadedLevel); //reload the scene
			}
		}
	}

}
