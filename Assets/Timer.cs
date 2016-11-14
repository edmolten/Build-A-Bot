using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour {

	Text time1, time2;
	float currentTime;


	void Start () {
		time1 = (Text)GameObject.Find ("Time 1");
		time2 = (Text)GameObject.Find ("Time 2");
		totalTcurrentTimeime = 60f * 10f;
	}

	void Update () {
		int min = currentTime / 60;
		int sec = currentTime % 60;
		String minString, secString;
		if(min < 10){
			minString = "0" +min);

		currentTime
	}
}
