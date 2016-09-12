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
	public KeyCode respawnKey;
	public float yCenter;

	private Vector3 initialPosition;
	private Quaternion initialRotation;

	public void Start(){
		//para evitar volcaminetos weones, se baja el centro de gravedad
		GetComponent<Rigidbody>().centerOfMass = new Vector3(0,yCenter,0);

		Vector3 position = GetComponent<Transform> ().position;
		Quaternion rotation = GetComponent<Transform> ().localRotation;
		initialPosition = new Vector3(position.x,position.y,position.z);
		initialRotation = new Quaternion (rotation.x, rotation.y, rotation.z, rotation.w);
	}

	public void Update(){
		if (Input.GetKeyDown (respawnKey)) {
			//transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
			transform.localRotation = new Quaternion(initialRotation.x,initialRotation.y,initialRotation.z,initialRotation.w);
		}
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

		visualWheel.transform.Rotate (0,0,90); //sven
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
	}
}