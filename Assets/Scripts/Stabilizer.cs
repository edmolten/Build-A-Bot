using UnityEngine;
using System.Collections;

public class Stabilizer : MonoBehaviour
{
	public void stabilize(){
		Rigidbody[] partsRigidbodies = GetComponentsInChildren<Rigidbody> ();
		Transform[] partsTransforms = GetComponentsInChildren<Transform> ();
		foreach(Rigidbody r in partsRigidbodies){
			r.velocity = Vector3.zero;
			r.angularVelocity = Vector3.zero;
		}	
		foreach (Transform t in partsTransforms) {
			InitialTransform it = t.GetComponent<InitialTransform>();
			t.localPosition = it.initialPosition;
			t.localRotation = it.initialRotation;
		}
		Transform thisT = GetComponent<Transform> ();
		InitialTransform thisIT = GetComponent<InitialTransform> ();
		thisT.localPosition = thisIT.initialPosition;
		thisT.localRotation = thisIT.initialRotation;
	}
}

