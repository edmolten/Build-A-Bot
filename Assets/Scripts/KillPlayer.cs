using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	void OnCollisionEnter (Collision col){
		if(col.gameObject.name.StartsWith ("Bot")){
			respawn (col.gameObject);
		}
	}

	void respawn(GameObject obj){
		Rigidbody rb = obj.GetComponent<Rigidbody>() ;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

		MovimientoBot script = obj.GetComponent<MovimientoBot> ();
		obj.transform.position = new Vector3 (script.initialPosition.x, script.initialPosition.y, script.initialPosition.z);
		obj.transform.rotation = new Quaternion (0, 0, 0, 0);
	}
}
