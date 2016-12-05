using UnityEngine;
using System.Collections;

public class AtaqueSierra : MonoBehaviour {

	public int frecuenciaRotacion;
	public int fuerza;
	private Rigidbody collRigidBody;
	private int layerWeapon1 = 12;
	private int layerWeapon2 = 13;

	void Start () {

		this.transform.name = "Sierra_" + this.transform.parent.name;
		this.transform.localPosition = new Vector3 (0f, 0f, 0.5f);

		ConfigurableJoint joinPoint = this.transform.parent.gameObject.AddComponent<ConfigurableJoint> ();
		joinPoint.connectedBody = this.GetComponent<Rigidbody> ();
		//Debug.Log (this.transform.localPosition);
		joinPoint.anchor = this.transform.localPosition;

		joinPoint.xMotion = ConfigurableJointMotion.Locked;
		joinPoint.yMotion = ConfigurableJointMotion.Locked;
		joinPoint.zMotion = ConfigurableJointMotion.Locked;
		joinPoint.angularXMotion = ConfigurableJointMotion.Locked;
		joinPoint.angularYMotion = ConfigurableJointMotion.Free;
		joinPoint.angularZMotion = ConfigurableJointMotion.Locked;

		string tag;
		int layer;
		if (this.transform.parent.parent.parent.name == "Bot 1") {
			tag = "Player1";
			layer = layerWeapon1;
		} else {
			tag = "Player2";
			layer = layerWeapon2;
		}
		this.tag = tag;
		this.gameObject.layer = layer;
	}

	void Update () {
		transform.Rotate (0, frecuenciaRotacion * Time.deltaTime, 0);
	}

	void OnCollisionEnter (Collision coll){
		collRigidBody = coll.gameObject.GetComponent<Rigidbody> ();
		string thisTag = this.tag;
		string otherTag = coll.gameObject.tag;
		if((thisTag == "Player1" && otherTag == "Player2") || (thisTag == "Player2" && otherTag == "Player1")){
			collRigidBody.AddForce (this.gameObject.GetComponentInParent<Transform> ().right * fuerza, ForceMode.VelocityChange);
			AudioSource audio = GetComponent<AudioSource> ();
			audio.Play ();
		}
	}

	void OnDestroy(){
		GameObject parent = this.transform.parent.gameObject;
		Destroy (parent.GetComponent<ConfigurableJoint> ());
	}
}
