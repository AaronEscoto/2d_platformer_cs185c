using UnityEngine;
using System.Collections;

public class flame_arrow_of_death : MonoBehaviour {
	public float speed;
	private Transform player;
	private player player_script;
	float lockY;
	float dir = 0.0f;
	// Use this for initialization
	void Start () {
		speed = 35f;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
		lockY = player.position.y; 
		dir = player_script.GetDir();
	}

	// Update is called once per frame
	void Update () {	
		if (dir < 0)
		{
			GetComponent<SpriteRenderer>().flipX = true;
			GetComponent<SpriteRenderer>().flipY = true;
			speed = -35f;
		} else 
		{
			GetComponent<SpriteRenderer>().flipX = false;
			GetComponent<SpriteRenderer>().flipY = false;
			speed = 35f;
		}
		float dist = Vector3.Distance(GameObject.Find ("Player").transform.position,transform.position);
		Vector3 position;
		position = transform.position;
		position = new Vector3 ((position.x + speed * Time.deltaTime), lockY);
		transform.position = position;

		if (dist > 50) {
			Destroy (this.gameObject);
		}
	}
}

