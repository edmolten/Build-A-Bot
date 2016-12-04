using UnityEngine;
using System.Collections;

public class AllwaysSpin : MonoBehaviour {

	public float speed = 3;

	void Update () {
		transform.Rotate (0,speed,0);
	}
}
