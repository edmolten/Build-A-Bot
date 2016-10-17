using UnityEngine;
using System.Collections;

public class AtaqueAriete : MonoBehaviour {

	public float fuerza;
	public string pushButton;
	private float pushing;
	private bool isPushing;
	private Rigidbody collRigidBody;
	private Animation pushAnimation;

	// Use this for initialization
	void Start () {
		isPushing = false;
		pushing = 1F / 12F;
		pushAnimation = this.gameObject.GetComponent<Animation> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (pushButton)) {
			pushAnimation.Play("ramAttack");
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
		if(isPushing && pushing >= 0 && (thisTag == "Player1" && otherTag == "Player2") || (thisTag == "Player2" && otherTag == "Player1")){
			collRigidBody.AddForce (-this.gameObject.transform.up * fuerza, ForceMode.VelocityChange);
		}
	}

}