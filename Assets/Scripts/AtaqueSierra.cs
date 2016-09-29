using UnityEngine;
using System.Collections;

public class AtaqueSierra : MonoBehaviour {

	public int frecuenciaRotacion;
	public int fuerza;
	private Rigidbody collRigidBody;
	private GameObject terrain;

	void Start () {
		terrain = GameObject.Find ("Terrain");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, frecuenciaRotacion * Time.deltaTime, 0);
	}

	void OnCollisionEnter (Collision coll){
		collRigidBody = coll.gameObject.GetComponent<Rigidbody> ();
		if (coll.gameObject.name != terrain.name) {
			collRigidBody.AddForce (-this.gameObject.transform.forward * fuerza, ForceMode.Impulse);
		}
	}
}
