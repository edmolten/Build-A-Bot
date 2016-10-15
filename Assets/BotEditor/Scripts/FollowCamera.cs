using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

  public int moveFactor = 150;

  private KeyCode keyLeft = KeyCode.LeftArrow;
  private KeyCode keyRight = KeyCode.RightArrow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
  
	   if (Input.GetKey(keyLeft)) {
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * moveFactor);
      } else if (Input.GetKey(keyRight)) {
        transform.RotateAround(transform.position, Vector3.down, Time.deltaTime * moveFactor);
      }
	}
}
