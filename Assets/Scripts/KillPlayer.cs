using UnityEngine;
using System.Collections;
using UnityEditor.VersionControl;
using System.Security.Cryptography;
using UnityEditor;

public class KillPlayer : MonoBehaviour {

	void OnCollisionEnter (Collision col){
		GameObject bot;
		Score scoreScript;
		if (col.gameObject.name == "Bot 1") {
			bot = GameObject.Find ("Bot 2");
			scoreScript = bot.GetComponent<Score> ();
			scoreScript.count++;
			respawn (col.gameObject);
		}
		else if (col.gameObject.name == "Bot 2") {
			bot = GameObject.Find ("Bot 1");
			scoreScript = bot.GetComponent<Score> ();
			scoreScript.count++;
			respawn (col.gameObject);
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
		obj.transform.rotation = new Quaternion (0, 0, 0, 0);
	}
}
