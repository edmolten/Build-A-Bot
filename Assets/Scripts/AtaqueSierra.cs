using UnityEngine;
using System.Collections;

public class AtaqueSierra : MonoBehaviour {

	public int frecuenciaRotacion;
	public int fuerza;
	private Rigidbody collRigidBody;

	void Update () {
		transform.Rotate (0, frecuenciaRotacion * Time.deltaTime, 0);
	}

	void OnCollisionEnter (Collision coll){
		collRigidBody = coll.gameObject.GetComponent<Rigidbody> ();
		string thisTag = this.tag;
		string otherTag = coll.gameObject.tag;
		if((thisTag == "Player1" && otherTag == "Player2") || (thisTag == "Player2" && otherTag == "Player1")){
			collRigidBody.AddForce (this.gameObject.transform.right * fuerza, ForceMode.VelocityChange);
		}
	}
}
