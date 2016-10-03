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
		//Debug.Log ("Hola");
		velocidadImpacto = thisRigidBody.velocity;
		collRigidBody = coll.gameObject.GetComponent<Rigidbody> ();
		//Debug.Log (this.gameObject.name);
		//Debug.Log (transform.parent.parent.name);
		if(coll.gameObject.tag == "Player" && coll.gameObject.name != transform.parent.parent.name){
			collRigidBody.AddForce (velocidadImpacto * fuerza, ForceMode.Impulse);
		}
	}
}
