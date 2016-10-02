using UnityEngine;
using System.Collections;
using UnityEditor.VersionControl;

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
		MovimientoBot script = obj.GetComponent<MovimientoBot> ();
		obj.transform.position = new Vector3 (script.initialPosition.x, script.initialPosition.y, script.initialPosition.z);
		obj.transform.rotation = new Quaternion (0, 0, 0, 0);
	}
}
