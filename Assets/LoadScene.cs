using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadByIndex(int i) {
		SceneManager.LoadScene (i);
	}

	public void quitApplication() {
		Application.Quit();
	}
}
