using UnityEngine;
using System.Collections;

public class SwitchBackgroundColor : MonoBehaviour {

	public enum ColorSwitchMode {
		Immediate,
		Smooth
	};

	public Color newBackgroundColor;
	public ColorSwitchMode colorSwitchMode;
	public float smoothDuration; //Only for Smooth Switch Mode

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(isOrContainPlayer(other.transform)) {
			Camera curCamera = Camera.main;
			if(curCamera) {
				if(colorSwitchMode == ColorSwitchMode.Immediate)
					curCamera.backgroundColor = newBackgroundColor;
				else if(colorSwitchMode == ColorSwitchMode.Smooth) {
					StartCoroutine(smoothChangeColor());
				}
			}
		}
	}

	IEnumerator smoothChangeColor() {
		Camera curCamera = Camera.main;
		if(curCamera) {
			float t = 0.0f;
			Color startingColor = Camera.main.backgroundColor;

			while(t < smoothDuration) {
				Color interpolatedColor = Color.Lerp(startingColor, newBackgroundColor, t/smoothDuration);
				curCamera.backgroundColor = interpolatedColor;

				t+=Time.deltaTime;
				yield return null;
			}
		}
	}

	//Check if the collider is the Player's collider or if it contains the Player in some nested objects
	bool isOrContainPlayer(Transform transform) {
	
		Transform curTransform = transform;
		if(curTransform.tag == "Player") {
			return true;
		}

		for(int i=0;i<curTransform.childCount;i++) {
			Transform childTransform = curTransform.GetChild(i);
			if(isOrContainPlayer(childTransform))
				return true;
		}

		return false;
	}
}
