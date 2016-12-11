using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	public int moveFactor = 150;
	string playerNumber;

	void Start () {
		this.playerNumber = this.gameObject.name.Split ()[1];
	}

	void Update () {
		transform.RotateAround (transform.position, Vector3.up, Time.deltaTime * Input.GetAxis("AD"+playerNumber)*moveFactor);
	}
}
