using UnityEngine;
using System.Collections;

public class AtaqueBola : MonoBehaviour {

	public float fuerza;
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
		velocidadImpacto = thisRigidBody.velocity;
		collRigidBody = coll.gameObject.GetComponent<Rigidbody> ();
		if(coll.gameObject.tag == "Player"){
			AudioSource audio = this.GetComponent<AudioSource> ();
			setHitVolumen (audio);
			audio.Play ();
			collRigidBody.AddForce (velocidadImpacto * fuerza, ForceMode.Impulse);
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
