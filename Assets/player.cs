using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour
{
	float speed = 30;
	float gravity = -100;
	private Vector3 dir;
	int pickupsCollected;
	CharacterController cc;
	SpriteRenderer ss;
	int jumpForce =60;
	public AudioClip[] ouchClips;
	public Text healthBar;
	public GameObject win;
	bool died;
	public float health = 100f;
	public float damageAmount = 10f;
	Animator animator;
	public Image healthBarUI;
	float lowHealth = .99f;
	float medHealth = .30f;
	//SpriteRenderer hBar;

	void Start ()
	{
		animator = GetComponent<Animator> ();
		cc = GetComponent<CharacterController> ();
		ss = GetComponent<SpriteRenderer> ();
		died = false;

		if (GameObject.Find("HealthBar")!=null)
			healthBar = GameObject.Find("HealthBar").GetComponent<Text>();
		win = GameObject.Find("Finish");
		healthBarUI = GameObject.Find ("Health_backup").GetComponent<Image> ();
		//hBar = GameObject.Find("Health_backup").GetComponent<SpriteRenderer>();
	}

	void Update ()
	{
	//	ChangeDirection ();
		MoveAround ();
		isGameOver ();
		playerQuit ();

	}

	//changedirection
	void ChangeDirection ()
	{
		if (dir.x < 0 && ss.flipX == false) {
			ss.flipX = true;
		} 

		if (dir.x > 0) {
			ss.flipX = false;
		} 
	}

	//move character around
	void MoveAround ()
	{
		float horizontalInput = Input.GetAxis ("Horizontal");
		if (Input.GetAxis("Vertical") > 0 ) {
			if (cc.isGrounded) {
				dir.y = dir.y + jumpForce;
			}
		}
	
	
		dir.x = horizontalInput * speed;
		dir.y += gravity * Time.deltaTime;
		cc.Move (dir * Time.deltaTime);
		animator.SetFloat ("direction", dir.x);
	}




	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Finish") {
				win.SetActive(true);
		} else if (other.tag == "LevelUp") {
			Application.LoadLevel(2);
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
				healthBarUI.fillAmount = health / 100;

				// this works for changing the color inspector but does not reflect in game 
				/*
				if (healthBarUI.fillAmount < medHealth) {
					//hBar.color = Color.yellow;
				}else if(healthBarUI.fillAmount < lowHealth){
					hBar.material.color = Color.red;
				}*/

			}
		}
	}

	void isGameOver ()
	{
		if (pickupsCollected >= 5) {
			pickupsCollected = 0;
			Application.LoadLevel (0);
		}

	}

	void playerQuit ()
	{
		if (Input.GetKey ("escape")) {
			Application.LoadLevel (0);
		}
	}


}
