using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BasicPickup : MonoBehaviour {

	bool picked;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(!picked) {
			if(other.tag == "Player") {
				picked = true;

				AudioSource audio = this.GetComponent<AudioSource>();
				audio.Play();

				this.GetComponent<Renderer>().enabled = false; //hide this object
				Destroy (this.gameObject, audio.clip.length); //destroy it after "audio clip length" seconds has elapsed (cannot destroy it now otherwise the audio clip is stopped)
			}
		}
	}
}
