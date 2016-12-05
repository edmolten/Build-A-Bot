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

	private int layerWeapon1 = 12;
	private int layerWeapon2 = 13;

	void Start () {
		thisRigidBody = this.gameObject.GetComponent<Rigidbody> ();
		stabilizer = GetComponentInParent<Stabilizer> ();

		this.transform.parent.localPosition = new Vector3(0f,0.75f,-2.5f);
		this.transform.parent.GetChild(1).name += "_" + this.transform.parent.parent.name;
		this.transform.parent.name = "Bola Demoledora_" + this.transform.parent.parent.name;

		this.transform.parent.Rotate(0, this.transform.parent.parent.eulerAngles.y + 180, 0);

		FixedJoint joinPoint = this.transform.parent.parent.gameObject.AddComponent<FixedJoint> ();

		joinPoint.connectedBody = this.transform.parent.GetChild(1).gameObject.GetComponent<Rigidbody> ();

		string tag;
		int layer;

		if (this.transform.parent.parent.parent.parent.name == "Bot 1") {
			tag = "Player1";
			layer = layerWeapon1;
		} else {
			tag = "Player2";
			layer = layerWeapon2;
		}
		this.tag = tag;
		this.gameObject.layer = layer;
		foreach (Transform child in this.transform.parent){
			child.tag = tag;
			child.gameObject.layer = layer;
		}

		this.transform.parent.tag = tag;
		this.transform.parent.gameObject.layer = layer;
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

	void OnDestroy(){
		GameObject parent = this.transform.parent.parent.gameObject;
		Destroy (parent.GetComponent<FixedJoint> ());
	}
}
