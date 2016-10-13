using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	public int moveFactor = 150;
	public int controlType = 0; //0 for letters and 1 for Arrows

	private KeyCode keyA = KeyCode.A;
  	private KeyCode keyD = KeyCode.D;
	private KeyCode keyLeft = KeyCode.LeftArrow;
	private KeyCode keyRight = KeyCode.RightArrow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (controlType == 0) {	
			if (Input.GetKey (keyLeft)) {
				transform.RotateAround (transform.position, Vector3.up, Time.deltaTime * moveFactor);
			} else if (Input.GetKey (keyRight)) {
				transform.RotateAround (transform.position, Vector3.down, Time.deltaTime * moveFactor);
			}
		} else if (controlType == 1) {
			if (Input.GetKey (keyA)) {
				transform.RotateAround (transform.position, Vector3.up, Time.deltaTime * moveFactor);
			} else if (Input.GetKey (keyD)) {
				transform.RotateAround (transform.position, Vector3.down, Time.deltaTime * moveFactor);
			}
		}
	}
}
