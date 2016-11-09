using UnityEngine;
using System.Collections;

public class flame_arrow_of_death : MonoBehaviour {
	public float speed;
	private Transform player;
	// Use this for initialization
	void Start () {
		speed = 35f;
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		Vector3 position;

		position = transform.position;
		position = new Vector3 ((position.x + speed * Time.deltaTime), player.position.y);
		transform.position = position;
	}
}

