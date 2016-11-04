using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
{
	float speed = 30;
	float gravity = -100;
	private Vector3 dir;
	int pickupsCollected;
	CharacterController cc;
	SpriteRenderer ss;
	int jumpForce =80;
	void Start ()
	{
		cc = GetComponent<CharacterController> ();
		ss = GetComponent<SpriteRenderer> ();

	}

	void Update ()
	{
		ChangeDirection ();
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
		if (Input.GetAxis("Vertical") > 0 ) {
			if (cc.isGrounded) {
				dir.y = dir.y + jumpForce;
			}
		}

	
		dir.x = Input.GetAxis ("Horizontal") * speed;
		dir.y += gravity * Time.deltaTime;
		cc.Move (dir * Time.deltaTime);
	
	}




	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "pickup") {
			Destroy (other.gameObject);
			pickupsCollected += 1;
		}
		if (other.gameObject.tag == "enemy") {
			Destroy (other.gameObject);
			Application.LoadLevel (0);
		}
		if (other.gameObject.tag == "Start") {
			Destroy (other.gameObject);
			Application.LoadLevel (1);
		}
		if (other.gameObject.name == "heart") {
			Destroy (other.gameObject);
			Application.LoadLevel (0);
		}
		if (other.gameObject.name == "directions") {
			Destroy (other.gameObject);
			Application.LoadLevel (2);
		}
		if (other.gameObject.name == "back") {
			Destroy (other.gameObject);
			Application.LoadLevel (0);
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
