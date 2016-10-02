using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Text scoreText;
	public int count;

	void Start () {
		count = 0;
	}

	void Update () {
		scoreText.text = "Score : " + count.ToString ();
	}
}
