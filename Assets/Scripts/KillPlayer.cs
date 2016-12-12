using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine.UI;
using System;

public class KillPlayer : MonoBehaviour {

	private int winCount = 10;
	Timer timer;
	void Start(){
		timer = GameObject.Find ("TimerObject").GetComponent<Timer> ();
	}

	void OnCollisionEnter (Collision col){
		GameObject bot;
		Score scoreScript;
		if (timer.started) {
			if (col.gameObject.name == "Bot 1") {
				bot = GameObject.Find ("Bot 2");
				scoreScript = bot.GetComponent<Score> ();
				scoreScript.count++;
				if (scoreScript.count == winCount) {
					Text t = (Text)GameObject.Find ("Match Result 2").GetComponent<Text> ();
					t.text = "You Win!";
					t = (Text)GameObject.Find ("Match Result 1").GetComponent<Text> ();
					t.text = "You Lose!";
				}
				respawn (col.gameObject);
			} else if (col.gameObject.name == "Bot 2") {
				bot = GameObject.Find ("Bot 1");
				scoreScript = bot.GetComponent<Score> ();
				scoreScript.count++;
				if (scoreScript.count == winCount) {
					Text t = (Text)GameObject.Find ("Match Result 2").GetComponent<Text> ();
					t.text = "You Lose!";
					t = (Text)GameObject.Find ("Match Result 1").GetComponent<Text> ();
					t.text = "You Win!";
				}
				respawn (col.gameObject);
			}
		}
	}

	void respawn(GameObject obj){
		AudioSource audio = obj.GetComponent<AudioSource> ();
		audio.Play ();
		Rigidbody rb = obj.GetComponent<Rigidbody>() ;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		foreach (Transform child in obj.transform){
			GameObject go = child.gameObject; 
			if (go.CompareTag ("Tool")) {
				foreach (Transform toolChild in go.transform) {
					Rigidbody rig = toolChild.gameObject.GetComponent<Rigidbody> ();
					if (rig != null) {
						rig.velocity = Vector3.zero;
						rig.angularVelocity = Vector3.zero;
					}
				}
			}
		}
		MovimientoBot script = obj.GetComponent<MovimientoBot> ();
		obj.transform.position = new Vector3 (script.initialPosition.x, script.initialPosition.y, script.initialPosition.z);
		if (obj.name == "Bot 1") {
			obj.transform.rotation = new Quaternion (0, 0, 0, 0);
		} else {
			obj.transform.rotation = new Quaternion (0, 180, 0, 0);
		}
	}
}
