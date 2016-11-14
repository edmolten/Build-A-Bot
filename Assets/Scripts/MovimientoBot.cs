using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class AxleInfo {
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public bool motor;
	public bool steering;
}

public class MovimientoBot : MonoBehaviour {
	
	public List<AxleInfo> axleInfos; 
	public float maxMotorTorque;
	public float maxSteeringAngle;
	public String ejeMotor;
	public String ejeRotacion;
	public String jumpAxis;
	public KeyCode respawnKey;
	public float yCenter;
	public Vector3 initialPosition;
	public int jumpPower;
	public String YButton;
	public String BButton;
	public String AButton;
	public String XButton;

	public void Start(){
		//para evitar volcaminetos weones, se baja el centro de gravedad
		GetComponent<Rigidbody>().centerOfMass = new Vector3(0,yCenter,0);
		initialPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
	}

	// finds the corresponding visual wheel
	// correctly applies the transform
	public void ApplyLocalPositionToVisuals(WheelCollider collider)
	{
		if (collider.transform.childCount == 0) {
			return;
		}

		Transform visualWheel = collider.transform.GetChild(0);

		Vector3 position;
		Quaternion rotation;
		collider.GetWorldPose(out position, out rotation);


		visualWheel.transform.position = position;
		visualWheel.transform.rotation = rotation;

		visualWheel.transform.Rotate (0,0,90);
	}

	public void FixedUpdate()
	{
		
		float motor = maxMotorTorque * Input.GetAxis(ejeMotor);
		float steering = maxSteeringAngle * Input.GetAxis(ejeRotacion);

		foreach (AxleInfo axleInfo in axleInfos) {
			if (axleInfo.steering) {
				axleInfo.leftWheel.steerAngle = steering;
				axleInfo.rightWheel.steerAngle = steering;
			}
			if (axleInfo.motor) {
				axleInfo.leftWheel.motorTorque = motor;
				axleInfo.rightWheel.motorTorque = motor;
			}
			ApplyLocalPositionToVisuals(axleInfo.leftWheel);
			ApplyLocalPositionToVisuals(axleInfo.rightWheel);
		}
		if (Input.GetButtonDown (jumpAxis)) {
			WheelCollider[]  wheels = GetComponentsInChildren<WheelCollider> ();
			foreach (WheelCollider wc in wheels) {
				WheelHit wh = new WheelHit();
				if (wc.GetGroundHit(out wh)){
					GetComponent<Rigidbody> ().AddForce (new Vector3 (0, jumpPower, 0),ForceMode.Impulse);
					break;
				}
			}
		}

	}
}