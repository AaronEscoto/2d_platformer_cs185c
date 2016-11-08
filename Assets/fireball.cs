using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour {
	float lockPos = 0;
	float dist = 0;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		float dist = Vector3.Distance(GameObject.Find ("Player").transform.position,transform.position);

		if (dist < 40) {
			transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, lockPos, lockPos);
			transform.position = Vector3.MoveTowards (transform.position, GameObject.Find ("Player").transform.position, 0.2f);
		}
	}
}
