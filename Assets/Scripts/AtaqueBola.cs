using UnityEngine;
using System.Collections;

public class AtaqueBola : MonoBehaviour {

	public float fuerza;
	private Vector3 velocidadImpacto;
	private Rigidbody thisRigidBody;
	private Rigidbody collRigidBody;

	void Start () {
		thisRigidBody = this.gameObject.GetComponent<Rigidbody> ();
	}

	void OnCollisionEnter(Collision coll){
		velocidadImpacto = thisRigidBody.velocity;
		collRigidBody = coll.gameObject.GetComponent<Rigidbody> ();
		if(coll.gameObject.tag == "Player" || coll.gameObject.tag == "Tool"){
			collRigidBody.AddForce (velocidadImpacto * fuerza, ForceMode.Impulse);
		}
	}
}
