using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BasicPickup : MonoBehaviour {

	bool picked;
	private Score s;
	public Text win;

	// Use this for initialization
	void Start () {
		s = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
		s.score =0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(!picked) {
			if(tag!="Finish") {
				if (other.tag == "Player") {
					picked = true;

					AudioSource audio = this.GetComponent<AudioSource>();
					audio.Play();
					s.score++;
					this.GetComponent<Renderer>().enabled = false; //hide this object
					Destroy (this.gameObject, audio.clip.length); //destroy it after "audio clip length" seconds has elapsed (cannot destroy it now otherwise the audio clip is stopped)
				}
			}
			else {
				picked = true;
				AudioSource audio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
				audio.Play();
				this.GetComponent<Renderer>().enabled = false;
				win.text = "You Win!!!";
				StartCoroutine("load");
			}
			
		}
	}
	IEnumerator load()
		{
			Destroy(GameObject.FindGameObjectWithTag("fireball"));
			Destroy(GameObject.FindGameObjectWithTag("Boss"));
			Destroy(GameObject.FindGameObjectWithTag("HealthBar"));
			yield return new WaitForSeconds(3.0f);
			Destroy (this.gameObject);
			Application.LoadLevel(0);
		}
}
