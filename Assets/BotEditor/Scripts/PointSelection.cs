using UnityEngine;
using System.Collections;

public class PointSelection : MonoBehaviour {
	
    public AssemblerPiece assembler = null;

    // Use this for initialization
    void Start() {

    }

	void OnMouseOver() {
		if (Input.GetMouseButtonDown(0)){
			assembler.pointToJoin = transform.gameObject;
			assembler.jointPartToPoint ();
		}
	}
		
}
