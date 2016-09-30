using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SideMove : MonoBehaviour {

	public float velocity;
	float direction = 1;
	Vector3 p;

	void Update () {
		p = transform.position;
		transform.position = new Vector3(p.x,p.y,p.z + direction * velocity);
		if (transform.position.z <= -32 || transform.position.z >= 2) {
			direction *= -1;
		}
	}
}
