using UnityEngine;
using System.Collections;

public class VerticalMovingPlatform : MonoBehaviour {
	float direction;
	Vector3 startingPosition;
	DistanceJoint2D joint;
	GameObject playerOnPlatform;

	public float movingDistance; //how much it moves from the starting position (on both left and right side).
	public float speed;
	public float startingDirection = 1; //1 up, -1 down

	private Rigidbody2D rb2d;

	
	void Awake() {
		joint = GetComponent<DistanceJoint2D>();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Use this for initialization
	void Start () {
		startingPosition = rb2d.position;
		direction = startingDirection;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate() {

		if(Mathf.Abs(rb2d.position.y - startingPosition.y) >= movingDistance) {
			direction = -direction;
		}

		Vector2 newPos = new Vector2(rb2d.position.x, rb2d.position.y + direction*speed*Time.deltaTime);
		rb2d.MovePosition(newPos);
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {

		if(other.gameObject.tag == "Player") {
			Debug.Log("Player ENTERED moving platform --");
			other.gameObject.transform.parent = transform;
			playerOnPlatform = other.gameObject;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if(playerOnPlatform)
			playerOnPlatform.transform.parent = transform;
	}
	
	void OnTriggerExit2D(Collider2D other) {

		if(other.gameObject.tag == "Player") {
			other.gameObject.transform.parent = null;
			playerOnPlatform = null;
		}
	}
}
