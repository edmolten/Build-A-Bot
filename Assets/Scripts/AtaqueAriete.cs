using UnityEngine;
using System.Collections;

public class AtaqueAriete : MonoBehaviour {

	public float fuerza;
	public string pushButton;
	private float pushing;
	private bool isPushing;
	private Rigidbody collRigidBody;
	private Animation pushAnimation;
	private int layerWeapon1 = 12;
	private int layerWeapon2 = 13;
	public bool attack = false;
	// Use this for initialization
	void Start () {
		
		isPushing = false;
		pushing = 1F / 12F;
		pushAnimation = this.gameObject.GetComponent<Animation> ();

		this.transform.name = "Ariete_" + this.transform.parent.name;
		this.transform.localPosition = new Vector3 (0f, 0f, 0f);

		Debug.Log (this.transform.parent.name + " esta rotado en " + this.transform.parent.localEulerAngles.y);
		if (this.transform.parent.name == "Atras") {
			this.transform.localRotation = Quaternion.AngleAxis (this.transform.parent.localEulerAngles.y/2, Vector3.right);
		} else if (this.transform.parent.name == "Frente" ) {
			this.transform.localRotation = Quaternion.AngleAxis (-90, Vector3.right);
		} else {
			this.transform.localRotation = Quaternion.AngleAxis (this.transform.parent.localEulerAngles.y, Vector3.right);
		}

		FixedJoint jointPoint = this.transform.parent.gameObject.AddComponent<FixedJoint> ();
		jointPoint.connectedBody = this.gameObject.GetComponent<Rigidbody> ();

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
		if (attack && !isPushing) {
			pushAnimation.Play("ramAttack");
			attack = false;
			isPushing = true;
		}
		if (isPushing) {
			pushing -= Time.deltaTime;
			if (pushing < 0) {
				pushing = 1F / 12F;
				isPushing = false;
			}
		}
	}

	void OnCollisionEnter(Collision coll){
		collRigidBody = coll.gameObject.GetComponent<Rigidbody> ();
		string thisTag = this.tag;
		string otherTag = coll.gameObject.tag;
		if(isPushing && pushing >= 0 && ((thisTag == "Player1" && otherTag == "Player2") || (thisTag == "Player2" && otherTag == "Player1"))){
			AudioSource audio = GetComponent<AudioSource> ();
			audio.Play ();
			collRigidBody.AddForce (- this.gameObject.transform.up * fuerza, ForceMode.VelocityChange);
			pushing = 1F / 12F;
			isPushing = false;
		}
	}

	void OnDestroy(){
		GameObject parent = this.transform.parent.gameObject;
		Destroy (parent.GetComponent<FixedJoint> ());
	}

}