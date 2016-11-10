using UnityEngine;
using System.Collections;

public class flame_arrow_of_death : MonoBehaviour {
	public float speed;
	private Transform player;
	float lockY;
	// Use this for initialization
	void Start () {
		speed = 35f;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		lockY = player.position.y; 
	}

	// Update is called once per frame
	void Update () {
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

