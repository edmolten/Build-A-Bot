using UnityEngine;
using System.Collections;

public class AtaqueAriete : MonoBehaviour {

	public float fuerza;
	public float pushing;
	public bool isPushing;
	private Rigidbody collRigidBody;
	private GameObject terrain;
	private Animation animation;

	// Use this for initialization
	void Start () {
		isPushing = false;
		pushing = 1F / 12F;
		terrain = GameObject.Find ("Terrain");
		animation = this.gameObject.GetComponent<Animation> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.L)) {
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
		//Debug.Log (collRigidBody.GetType());
		//Debug.Log (coll.gameObject.name);
		if (isPushing && pushing >= 0 && coll.gameObject.name != terrain.name) {
			collRigidBody.AddForce (-this.gameObject.transform.up * fuerza, ForceMode.Impulse);
		}
	}

}