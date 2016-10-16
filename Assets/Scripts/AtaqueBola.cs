using UnityEngine;
using System.Collections;

public class AtaqueBola : MonoBehaviour {

	public float forceAmplify = 2;
	private Vector3 velocidadImpacto;
	private Rigidbody thisRigidBody;
	private Rigidbody collRigidBody;
	private Transform bolaDemoledoraInitialTransform;
	private Stabilizer stabilizer;

	private float minVol = 0.1f;
	private float estimatedMaxVelMag = 17f;
	private float estimatedVelMagToFail = 50f;

	Vector3 parentPosition;
	Quaternion parentRotation;

	void Start () {
		thisRigidBody = this.gameObject.GetComponent<Rigidbody> ();
		stabilizer = GetComponentInParent<Stabilizer> ();
	}

	void Update(){
		if (thisRigidBody.velocity.magnitude >= estimatedVelMagToFail) {
			Debug.Log ("Estabilizado");
			stabilizer.stabilize ();
		}
	}

	void OnCollisionEnter(Collision coll){
		string thisTag = this.tag;
		string otherTag = coll.gameObject.tag;
		if((thisTag == "Player1" && otherTag == "Player2") || (thisTag == "Player2" && otherTag == "Player1")){
			Vector3 dir = (coll.contacts[0].point - transform.position).normalized;
			float velMag = thisRigidBody.velocity.magnitude;
			collRigidBody = coll.gameObject.GetComponent<Rigidbody> ();
			AudioSource audio = GetComponent<AudioSource> ();
			setHitVolumen (audio);
			audio.Play ();
			collRigidBody.AddForce (dir * velMag * forceAmplify, ForceMode.VelocityChange);
			thisRigidBody.AddForce (-dir * velMag, ForceMode.VelocityChange);
		}
	}
	void setHitVolumen(AudioSource audio){
		float velMag = thisRigidBody.velocity.magnitude;
		if (velMag >= estimatedMaxVelMag) {
			audio.volume = 1;
		} else {
			audio.volume = ((1 - minVol) / estimatedMaxVelMag) * velMag + minVol;
		}
	}
}
