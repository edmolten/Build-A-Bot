using UnityEngine;
using System.Collections;

public class RestartPlayer : MonoBehaviour {

	private int countRestart = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void restartGame(){ 
		if (countRestart == 1) {
			countRestart = 0;
			Application.LoadLevel(0);
		} 

		countRestart++;

	}
}
