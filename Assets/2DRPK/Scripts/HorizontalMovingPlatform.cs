using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class HorizontalMovingPlatform : MonoBehaviour {
	float direction;
	Vector3 startingPosition;
	DistanceJoint2D joint;
	GameObject playerOnPlatform;

	public float movingDistance; //how much it moves from the starting position (on both left and right side).
	public float speed;
	public float startingDirection = 1; //1 right, -1 left
	private Rigidbody2D rb2d;

	void Awake() {
		joint = GetComponent<DistanceJoint2D>();
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.isKinematic = true;
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

		if(Mathf.Abs(rb2d.position.x - startingPosition.x) >= movingDistance) {
			direction = -direction;
		}

		//Debug.Log(rb2d.position);
		Vector2 newPos = new Vector2(rb2d.position.x + direction*speed*Time.deltaTime, rb2d.position.y);
		rb2d.MovePosition(newPos);

	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player") {
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
