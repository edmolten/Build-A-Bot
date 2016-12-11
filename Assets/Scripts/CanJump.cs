using UnityEngine;
using System.Collections;
using System;

public class CanJump : MonoBehaviour {

	String playerNumber;
	bool colliding = false;

	void Start () {
		this.playerNumber = gameObject.transform.parent.gameObject.name.Split ()[1];
	}

	void FixedUpdate(){
		if (colliding && Input.GetButtonDown ("L1" + playerNumber)) {
			Rigidbody rb = gameObject.transform.parent.GetComponent<Rigidbody> ();
			rb.AddForce (new Vector3 (0, 6000, 0),ForceMode.Impulse);
			rb.AddTorque (new Vector3(0,300,0));
		}
	}

	public void OnCollisionEnter(){
		colliding = true;
		Debug.Log ("asd");
	}

	public void OnCollisionExit(){
		colliding = false;
		Debug.Log ("salio");
	}
}
