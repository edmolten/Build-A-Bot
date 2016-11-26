using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour {

	Text time1; 
	Text time2;
	float currentTime;
	public bool started = false;

	void Start () {
		
	}

	/*
	 * This is to control when the timer get the refences to the labels.
	*/
	public void setUpTimer () {
		time1 = GameObject.Find ("Time 1").GetComponent<Text> ();
		time2 = GameObject.Find ("Time 2").GetComponent<Text>();
		currentTime = 60f;
		started = true;
	} 

	void Update () {
		if (!started) {
			return;
		}

		int min = (int)(currentTime / 60);
		int sec = (int)(currentTime % 60);
		String minString, secString;
		if (min < 10) {
			minString = "0" + min.ToString ();
		} else {
			minString = min.ToString ();
		}		
		if (sec < 10) {
			secString = "0" + sec.ToString ();
		} else {
			secString = sec.ToString ();
		}
		time1.text = minString + " : " + secString;
		time2.text = minString + " : " + secString;
		currentTime -= Time.deltaTime;
		if (currentTime <= 0) {
			GameObject bot1 = GameObject.Find ("Bot 1");
			Score scoreScript1 = bot1.GetComponent<Score> ();
			GameObject bot2 = GameObject.Find ("Bot 2");
			Score scoreScript2 = bot2.GetComponent<Score> ();
			if (scoreScript1.count > scoreScript2.count) {
				Text t = (Text)GameObject.Find ("Match Result 1").GetComponent<Text> ();
				t.text = "You Win!";
				t = (Text)GameObject.Find ("Match Result 2").GetComponent<Text> ();
				t.text = "You Lose!";
			} else if (scoreScript1.count < scoreScript2.count) {
				Text t = (Text)GameObject.Find ("Match Result 2").GetComponent<Text> ();
				t.text = "You Win!";
				t = (Text)GameObject.Find ("Match Result 1").GetComponent<Text> ();
				t.text = "You Lose!";
			} else {
				Text t = (Text)GameObject.Find ("Match Result 2").GetComponent<Text> ();
				t.text = "Draw!";
				t = (Text)GameObject.Find ("Match Result 1").GetComponent<Text> ();
				t.text = "Draw!";
			}
			started = false;
		}
	}
}
