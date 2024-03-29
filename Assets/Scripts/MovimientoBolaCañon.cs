﻿using UnityEngine;
using System.Collections;

public class MovimientoBolaCañon : MonoBehaviour {

	private Rigidbody collRigidBody;
	public float fuerza;
	public float fuerzaDisparo;
	private AudioSource audioSource;

	void Start () {
		GameObject cuerpo = this.transform.parent.transform.gameObject;
		this.GetComponent<Rigidbody> ().AddForce (cuerpo.transform.right * fuerzaDisparo, ForceMode.Impulse);
		this.tag = this.transform.parent.tag;
		audioSource = GameObject.Find ("SoundMaker").GetComponent<AudioSource> ();
	}

	void OnCollisionEnter(Collision coll){
		collRigidBody = coll.gameObject.GetComponent<Rigidbody> ();
		string thisTag = this.tag;
		string otherTag = coll.gameObject.tag;
		if ((thisTag == "Player1" && otherTag == "Player2") || (thisTag == "Player2" && otherTag == "Player1")) {
			GameObject cuerpo = this.transform.parent.transform.gameObject;
			audioSource.Play ();
			collRigidBody.AddForce (cuerpo.transform.right * fuerza, ForceMode.VelocityChange);

		}
		if (thisTag != otherTag) {
			Destroy (this.gameObject);
		}
	}
}
