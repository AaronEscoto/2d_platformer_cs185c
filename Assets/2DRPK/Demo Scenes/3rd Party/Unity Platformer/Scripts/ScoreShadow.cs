using UnityEngine;
using System.Collections;

public class ScoreShadow : MonoBehaviour
{
	public GameObject guiCopy;		// A copy of the score.
	private GUIText _guiText;

	void Awake()
	{
		// Set the position to be slightly down and behind the other gui.
		Vector3 behindPos = transform.position;
		behindPos = new Vector3(guiCopy.transform.position.x, guiCopy.transform.position.y-0.005f, guiCopy.transform.position.z-1);
		transform.position = behindPos;
		_guiText = GetComponent<GUIText> ();
	}


	void Update ()
	{
		// Set the text to equal the copy's text.
		_guiText.text = guiCopy.GetComponent<GUIText>().text;
	}
}
