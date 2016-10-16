using UnityEngine;
using System.Collections;

public class InitialTransform : MonoBehaviour
{
	public Vector3 initialPosition;
	public Quaternion initialRotation;

	void Start ()
	{
		Transform t = GetComponent<Transform> ();
		this.initialPosition = t.localPosition;
		this.initialRotation = t.localRotation;
	}

}

