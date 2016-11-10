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
	int jumpForce =65;
	int dJumpForce = 40;
	public AudioClip[] ouchClips;
	//public Text healthBar;
	bool died;
	public float health = 100f;
	public float damageAmount = 10f;
	Animator animator;
	public Image healthBarUI;
	float lowHealth = .30f;
	float medHealth = .50f;
	bool canDoubleJump = true;
	public GameObject arrow;
	public float direction;

	//SpriteRenderer hBar;

	public float GetDir()
	{
		return direction;
	}

	void Start ()
	{
		animator = GetComponent<Animator> ();
		cc = GetComponent<CharacterController> ();
		ss = GetComponent<SpriteRenderer> ();
		died = false;
		direction = 0.0f;

		//if (GameObject.Find("HealthBar")!=null)
		//healthBar = GameObject.Find("HealthBar").GetComponent<Text>();
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

		if (Input.GetButtonDown("Play")) {
			GameObject projectile = (GameObject)Instantiate (arrow);
			Vector3 loc = transform.position;

			if (dir.x >= 0) {
				loc.x = loc.x + 3;
				projectile.transform.position = loc;
			} else {
				loc.x = loc.x - 5;
				projectile.transform.position = loc;
			}

		}

		if (cc.isGrounded) {
			if (Input.GetAxis ("Vertical") > 0) {
				dir.y = dir.y + jumpForce;
			}
			canDoubleJump = true;
		}

		if (!cc.isGrounded && canDoubleJump) {

			if (Input.GetAxis ("Jump") > 0) {
				dir.y = 0;
				dir.y = dir.y + dJumpForce;
				canDoubleJump = false;
			}
		}

		dir.x = horizontalInput * speed;
		dir.y += gravity * Time.deltaTime;
		cc.Move (dir * Time.deltaTime);
		animator.SetFloat ("direction", dir.x);
		if (dir.x != 0)
			direction = dir.x;

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "LevelUp") {
			Application.LoadLevel(2);
		}
		if(!died) {
			if (other.CompareTag("DeathTrigger")) {
				int i = Random.Range (0, ouchClips.Length);
				AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
				died = true;
				Application.LoadLevel (Application.loadedLevel); //reload the scene
			}
			if (other.tag == "Boss" || other.tag == "Enemy" || other.tag == "fireball") {
				int i = Random.Range (0, ouchClips.Length);
				AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
				if (other.tag == "Boss") {
					health = health - 2.5f;
				} else if (other.tag == "Enemy") {
					health = health - 0.5f;
				} else if (other.tag == "fireball") {
					Destroy (other.gameObject);
					health = health - 1.25f;
				}

				if (health < 0.0f)
				{
					//healthBar.text = "0%";
					died = true;
					Application.LoadLevel (Application.loadedLevel); //reload the scene
				}

				//healthBar.text = health + "%";
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
