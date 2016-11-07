using UnityEngine;
using System.Collections;

public class arrow : MonoBehaviour {


	void Start () 
	{

		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 5);
	}


	void OnTriggerEnter (Collider col) 
	{
		// If it hits an enemy...
		if(col.tag == "Player")
		{
			// ... find the Enemy script and call the Hurt function.
			//col.gameObject.GetComponent<Enemy>().Hurt();




			// Destroy the rocket.
			Destroy (gameObject);
		}

	}
}
