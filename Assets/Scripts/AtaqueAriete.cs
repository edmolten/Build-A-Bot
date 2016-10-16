using UnityEngine;
using System.Collections;

public class AtaqueAriete : MonoBehaviour {

	public float fuerza;
	public string pushButton;
	private float pushing;
	private bool isPushing;
	private Rigidbody collRigidBody;
	private Animation animation;

	// Use this for initialization
	void Start () {
		isPushing = false;
		pushing = 1F / 12F;
		animation = this.gameObject.GetComponent<Animation> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (pushButton)) {
			animation.Play("ramAttack");
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

		if (isPushing && pushing >= 0 && coll.gameObject.name != this.gameObject.name && coll.gameObject.tag == "Player") {
			collRigidBody.AddForce (-this.gameObject.transform.up * fuerza, ForceMode.Impulse);
		}
	}

}