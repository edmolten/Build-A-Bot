﻿using UnityEngine;
using System.Collections;

public class AssemblerPiece : MonoBehaviour {
	public GameObject car = null;
    public GameObject pointToJoin = null;
	[HideInInspector] 
	public int currentType = -1;
	[HideInInspector]
	public GameObject piece = null;

    // Use this for initialization
    void Start () {
	
	}

	//Add piece to he point of joint
	public void jointPartToPoint() {
		FixedJoint fjPoint;
		GameObject pieceToAdd;

		if (piece != null && pointToJoin != null) {
			pieceToAdd = piece; 
			fjPoint = pointToJoin.GetComponent<FixedJoint> ();

			unSelectPart (false);
			
			if (fjPoint.connectedBody != null) {
				Destroy (fjPoint.connectedBody.gameObject);
			}
				
			pieceToAdd.transform.parent = pointToJoin.transform.parent;
			pointToJoin.GetComponent<FixedJoint>().connectedBody = pieceToAdd.GetComponent<Rigidbody>();
			pieceToAdd.transform.localPosition = new Vector3 (
				pointToJoin.transform.localPosition.x, 
				pointToJoin.transform.localPosition.y,
				pointToJoin.transform.localPosition.z
			);
			pieceToAdd.transform.localRotation = Quaternion.Euler(90, 0, 0); //Aplicar rotación locals
		}
	}
		
	//Set the reference to the piece to joint
	public void setPiece(GameObject newPiece) {
		piece = newPiece;
	}

	//Delete the reference to piece
	void unSelectPart (bool destroy) {
		if (piece == null) {
			return;
		}

		if (destroy) {
			Destroy(piece);
		}
		piece = null;
		currentType = -1;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(1)) {
			unSelectPart(true); 
		}

		if (piece != null) {
			Vector3 temp = Input.mousePosition;
			temp.z = 10f;        
			piece.transform.position = Camera.main.ScreenToWorldPoint(temp); 
		}
	}

}