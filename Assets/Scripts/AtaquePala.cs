using UnityEngine;
using System.Collections;

public class AtaquePala : MonoBehaviour {

	public float fuerza;
	public string pushButton;
	//private float cooldown;
	public float attackTime;
	public bool isAttacking;
	private Animation attackAnimation;
	private Rigidbody collRigidBody;
	private int layerWeapon1 = 12;
	private int layerWeapon2 = 13;

	// Use this for initialization
	void Start () {
		isAttacking = false;
		attackTime = 1F / 4F;
		attackAnimation = this.transform.parent.GetComponent<Animation> ();

		GameObject soporte = this.transform.parent.parent.GetChild (0).gameObject;

		soporte.name = "Soporte_" + this.transform.parent.parent.parent.name;
		this.transform.parent.parent.name = "Pala_" + this.transform.parent.parent.parent.name;
		if (this.transform.parent.parent.parent.name == "Izquierda" || this.transform.parent.parent.parent.name == "Derecha") {
			this.transform.parent.parent.localPosition = new Vector3 (-0.5f, -1.9f, 1.8f);
		} else {
			this.transform.parent.parent.localPosition = new Vector3 (-0.5f, -1.9f, 0.8f);
		}

		//weaon.transform.localPosition += new Vector3(,,-0,)
		this.transform.parent.parent.localEulerAngles = new Vector3 (0,90,0);

		//Debug.Log (this.transform.parent.parent.GetChild(0).name + this.transform.parent.parent.GetChild(1).name);

		FixedJoint joinPoint = this.transform.parent.parent.parent.gameObject.AddComponent<FixedJoint> ();

		joinPoint.connectedBody = soporte.GetComponent<Rigidbody> ();

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
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (pushButton)) {
			attackAnimation.Play("shovelAttack");
			isAttacking = true;
		}
		if (isAttacking) {
			attackTime -= Time.deltaTime;
			if (attackTime < 0) {
				attackTime = 1F / 4F;
				isAttacking = false;
			}
		}
	}

	void OnCollisionEnter(Collision coll){
		collRigidBody = coll.gameObject.GetComponent<Rigidbody> ();
		string thisTag = this.tag;
		string otherTag = coll.gameObject.tag;
		if(isAttacking && attackTime >= 0 && ((thisTag == "Player1" && otherTag == "Player2") || (thisTag == "Player2" && otherTag == "Player1"))){
			//AudioSource audio = GetComponent<AudioSource> ();
			//audio.Play ();
			collRigidBody.AddForce (- this.gameObject.transform.up * fuerza, ForceMode.VelocityChange);
			attackTime = 1F / 4F;
			isAttacking = false;
		}
	}

	void OnDestroy(){
		GameObject parent = this.transform.parent.parent.parent.gameObject;
		Destroy (parent.GetComponent<FixedJoint> ());
	}
}
