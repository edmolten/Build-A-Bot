using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {
	
	GameObject camara2Object;
	Camera camara1;
	Camera camara2;
	GameObject bot1;
	GameObject bot2;

	void Start(){
		camara1 = GameObject.Find ("Camara 1").GetComponent<Camera> ();
		camara2 = GameObject.Find ("Camara 2").GetComponent<Camera> ();
		bot1 = GameObject.Find ("Bot 1");
		bot2 = GameObject.Find ("Bot 2");
		camara1.GetComponent<SmoothFollow> ().enabled = false;
		camara2.GetComponent<SmoothFollow> ().enabled = false;
		camara2Object = GameObject.Find ("Camara 2");
		camara2Object.SetActive (false);
	}

	void Update () {
		if (Input.anyKey) {
			camara2Object.SetActive (true);
			camara1.rect = new Rect (0, 0.5f, 1, 0.5f);
			GameObject canvas1 = GameObject.Find ("Canvas 1");
			canvas1.transform.Find ("Wallpaper").gameObject.SetActive (false);
			bot1.GetComponent<EditorManager> ().start();
			bot2.GetComponent<EditorManager> ().start ();
			this.enabled = false;
		}
	}
}
