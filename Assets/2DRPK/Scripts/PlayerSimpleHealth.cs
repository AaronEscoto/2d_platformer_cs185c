using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSimpleHealth : MonoBehaviour {

	bool died;
	public float health = 100f;
	public float damageAmount = 10f;
	public Text healthBar;
	public GameObject win;
	public AudioClip[] ouchClips;

	void Awake() {
		died = false;
	}
	// Use this for initialization
	void Start () {
		healthBar = GameObject.Find("HealthBar").GetComponent<Text>();
		win = GameObject.Find("Finish");
	}
	
	// Update is called once per frame
	void Update () { 
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Finish") {
				win.SetActive(true);
		}
		if(!died) {
			if (other.tag == "DeathTrigger") {
				int i = Random.Range (0, ouchClips.Length);
				AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
				died = true;
				Application.LoadLevel (Application.loadedLevel); //reload the scene
			}
			if (other.tag == "Boss" || other.tag == "Enemy") {
				int i = Random.Range (0, ouchClips.Length);
				AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
				health = health - 0.5f;
				if (other.tag == "Boss") {
					health = health - 2.5f;
				}
			
				if (health < 0.0f)
				{
					healthBar.text = "0%";
					died = true;
					Application.LoadLevel (Application.loadedLevel); //reload the scene
				}
				healthBar.text = health + "%";
			}
		}
	}

}
