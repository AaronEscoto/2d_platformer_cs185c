using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class BossMonster : MonoBehaviour {

	private Animator anim;
	private Vector3 direction;
	private Vector3 startingPosition;
	private PlayerControl playerControl;


	public Transform SpawnPoint;

	public float walkingOffsetX; //how much it moves from the starting position (on both left and right side).
	public float speed;
	public float startingDirection = 1; //1 right, -1 left
	public float distance;
	public Rigidbody arrow;
	public int protectionRadius, bulletSpeed;
	public bool isAttacking;
	public float nextFire;
	public float fireRate = 0.5f;
	public GameObject playerTarget;


	void Awake() {
		anim = GetComponent<Animator>();
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}

	// Use this for initialization
	void Start () {
		startingPosition = transform.position;
		isAttacking = false;
		protectionRadius = 70;
		bulletSpeed = 50;
		nextFire = 5;

		if(startingDirection == 1)
			direction = new Vector3(1.0f, 0.0f, 0.0f);
		else if(startingDirection == -1)
			direction = new Vector3(-1.0f, 0.0f, 0.0f);
		else 
			direction = new Vector3(1.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {

		playerTarget = GameObject.FindGameObjectWithTag ("Player");
		transform.position += direction * speed * Time.deltaTime;


			if (Mathf.Abs (transform.position.x - startingPosition.x) >= walkingOffsetX) {
				direction = -direction;
			}
		
		distance = Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, 
			GameObject.FindGameObjectWithTag ("Boss").transform.position);


		if(distance <= protectionRadius){
			isAttacking = true;
			Attacking ();
		} else {
			isAttacking = false;
		}

	}

	void Attacking ()
	{
		nextFire -= Time.deltaTime;
		//transform.LookAt (GameObject.FindGameObjectWithTag ("Player").transform.position);
		if (nextFire <= 0) {

			Rigidbody arrowClone = (Rigidbody)Instantiate (arrow, transform.position, transform.rotation);
			arrowClone.velocity = transform.forward * bulletSpeed;
			nextFire += 5;
			}




	}
	void FixedUpdate() {
			anim.SetFloat("Speed", speed*direction.x);
	}

}
