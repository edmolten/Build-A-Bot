using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour {

	Text time1; 
	Text time2;
	float currentTime;

	public bool started = false;
	public Canvas canvasUI1 = null;
	public Canvas canvasUI2 = null;
    public GameObject cameraBot1 = null;
    public GameObject cameraBot2 = null;

	/*
	 * This is to control when the timer get the refences to the labels.
	*/
	public void setUpTimer () {
		time1 = GameObject.Find ("Time 1").GetComponent<Text> ();
		time2 = GameObject.Find ("Time 2").GetComponent<Text>();
		currentTime = 5*60f;
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
			end ();
		}
	}

	public void end(){
		started = false;

		int distanceCamera = 1;

		//Postion where move the cameras
		Vector3 positionMenuFinal1 = new Vector3(-1000, 0, 0);
		Vector3 positionMenuFinal2 = new Vector3(1000, 0, 0);

		GameObject bot1 = GameObject.Find("Bot 1");
		GameObject bot2 = GameObject.Find("Bot 2");

		//Disable physics to bots
		Rigidbody rbBot1 = bot1.GetComponent<Rigidbody>();
		Rigidbody rbBot2 = bot1.GetComponent<Rigidbody>();

		rbBot1.useGravity = false;
		rbBot1.isKinematic = true;

		rbBot2.useGravity = false;
		rbBot2.isKinematic = true;

		//bot1.transform.position = positionMenuFinal1;
		//bot2.transform.position = positionMenuFinal2;

		cameraBot1.GetComponent<SmoothFollow>().enabled = false;
		cameraBot1.transform.position = new Vector3(positionMenuFinal1.x, positionMenuFinal1.y, distanceCamera);

		cameraBot2.GetComponent<SmoothFollow>().enabled = false;
		cameraBot2.transform.position = new Vector3(positionMenuFinal1.x, positionMenuFinal1.y, distanceCamera);

		bot1.gameObject.SetActive(false);
		bot2.gameObject.SetActive(false);

		Transform menuGameBot1 = canvasUI1.gameObject.transform.Find("MenuGame");
		Transform menuFinalBot1 = canvasUI1.gameObject.transform.Find("MenuFinal");

		Transform menuGameBot2 = canvasUI2.gameObject.transform.Find("MenuGame");
		Transform menuFinalBot2 = canvasUI2.gameObject.transform.Find("MenuFinal");

		//Change visibilty of UI 
		menuGameBot1.gameObject.SetActive(false);
		menuFinalBot1.gameObject.SetActive(true);
		menuGameBot2.gameObject.SetActive(false);
		menuFinalBot2.gameObject.SetActive(true);

		menuFinalBot1.gameObject.transform.Find("Continue").gameObject.SetActive(false);
		menuFinalBot2.gameObject.transform.Find("Continue").gameObject.SetActive(false);

		//Set status result
		Score scoreScript1 = bot1.GetComponent<Score> ();
		Score scoreScript2 = bot2.GetComponent<Score> ();

		//Text t1 = (Text) GameObject.Find ("Match Result 1").GetComponent<Text> ();
		Text t1 = menuFinalBot1.Find("Container").Find("Status").gameObject.GetComponent<Text>();
		Text t2 = menuFinalBot2.Find("Container").Find("Status").gameObject.GetComponent<Text>();

		Text p1 = menuFinalBot1.Find("Container").Find("Puntaje1").gameObject.GetComponent<Text>();
		Text p2 = menuFinalBot2.Find("Container").Find("Puntaje1").gameObject.GetComponent<Text>();

		p1.text = scoreScript1.count.ToString();
		p2.text = scoreScript2.count.ToString();

		if (scoreScript1.count > scoreScript2.count) {
			t1.text = "You Win!";
			t2.text = "You Lose!";
		} else if (scoreScript1.count < scoreScript2.count) {
			t2.text = "You Win!";
			t1.text = "You Lose!";
		} else {
			/*secString = sec.ToString ();
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
					t.text = "Y o u  W i n !";
					t = (Text)GameObject.Find ("Match Result 2").GetComponent<Text> ();
					t.text = "Y o u  L o s e !";
				} else if (scoreScript1.count < scoreScript2.count) {
					Text t = (Text)GameObject.Find ("Match Result 2").GetComponent<Text> ();
					t.text = "Y o u   W i n !";
					t = (Text)GameObject.Find ("Match Result 1").GetComponent<Text> ();
					t.text = "Y o u  L o s e !";
				} else {
					Text t = (Text)GameObject.Find ("Match Result 2").GetComponent<Text> ();
					t.text = "D r a w !";
					t = (Text)GameObject.Find ("Match Result 1").GetComponent<Text> ();
					t.text = "D r a w !";
				}
				started = false;*/
			t1.text = "Draw!";
			t2.text = "Draw!";
		}
	}
}
