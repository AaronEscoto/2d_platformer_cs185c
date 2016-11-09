using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class BossMonster : MonoBehaviour {

	private Animator anim;
	private Vector3 direction;
	private Vector3 startingPosition;
	private PlayerControl playerControl;

	public float walkingOffsetX; //how much it moves from the starting position (on both left and right side).
	public float speed;
	public float startingDirection = 1; //1 right, -1 left

	public Object fire;
	bool died;
	private Image healthBar;
	float health = 100f;

	void Awake() {
		anim = GetComponent<Animator>();
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
	}

	// Use this for initialization
	void Start () {
		startingPosition = transform.position;
		died = false;

		if(startingDirection == 1)
			direction = new Vector3(1.0f, 0.0f, 0.0f);
		else if(startingDirection == -1)
			direction = new Vector3(-1.0f, 0.0f, 0.0f);
		else 
			direction = new Vector3(1.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += direction*speed*Time.deltaTime;

		if(Mathf.Abs(transform.position.x - startingPosition.x) >= walkingOffsetX) {
			direction = -direction;
		}
	}

	void FixedUpdate() {
		if (tag == "Boss") {
			if (transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x > 50.0f)
				anim.SetFloat("Speed", speed*direction.x);
			else {
				Vector3 pos = transform.position;
				pos.x = GameObject.FindGameObjectWithTag("Finish").transform.position.x;
				transform.position = pos;
				anim.SetFloat("Speed", 0);
				InvokeRepeating ("fire_spawn", 1.0f, 0.5f);
			}
		} else {
			anim.SetFloat("Speed", speed*direction.x);
		}
	}

	void fire_spawn () {
		GameObject[] enemies;
		enemies = GameObject.FindGameObjectsWithTag ("fireball");
		if (enemies.Length < 2) {
			Instantiate (fire, transform.position, Quaternion.identity);
		}
	}

	/*void OnTriggerEnter (Collider other)
	{
		if(!died) {
			if (tag == "Boss" && other.tag == "Player") {
					health = health - 2.5f;
			
				if (health < 0.0f)
				{
					died = true;
					gameObject.GetComponent<SpriteRenderer>().enabled = false;
				}
				if (healthBar)
					healthBar.fillAmount = health / 100;

			}
		}
	}*/

}
