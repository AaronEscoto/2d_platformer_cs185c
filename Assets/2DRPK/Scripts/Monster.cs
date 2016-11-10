﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Monster : MonoBehaviour {

	private Animator anim;
	private Vector3 direction;
	private Vector3 startingPosition;
	private PlayerControl playerControl;

	public float walkingOffsetX; //how much it moves from the starting position (on both left and right side).
	public float speed;
	public float startingDirection = 1; //1 right, -1 left
	bool died;

	void Awake() {
		anim = GetComponent<Animator>();
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
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
			anim.SetFloat("Speed", speed*direction.x);
	}
	void OnTriggerEnter (Collider other)
	{
		if(!died) {
			if (other.gameObject.tag == "Bullet") {
				Destroy(this.gameObject);
			}
		}
	}
}
