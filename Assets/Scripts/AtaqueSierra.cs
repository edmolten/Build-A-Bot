using UnityEngine;
using System.Collections;

public class AtaqueSierra : MonoBehaviour {

	public int frecuenciaRotacion;
	public int fuerza;
	private Rigidbody collRigidBody;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, frecuenciaRotacion * Time.deltaTime, 0);
	}

	void OnCollisionEnter (Collision coll){
		collRigidBody = coll.gameObject.GetComponent<Rigidbody> ();
		if (coll.gameObject.name != this.gameObject.name && coll.gameObject.tag == "Player") {
			collRigidBody.AddForce (this.gameObject.transform.right * fuerza, ForceMode.Impulse);
		}
	}
}
