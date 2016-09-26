using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstancePiece : MonoBehaviour {
	public AssemblerPiece assembler = null;

	private GameObject piece = null;
    private bool partSelected = false;
    private Dictionary<string, PrimitiveType> typeObjects; 

	// Use this for initialization
	void Start () {
        typeObjects = new Dictionary<string, PrimitiveType>();
        typeObjects["cube"] = PrimitiveType.Cube;
	    typeObjects["cylinder"] = PrimitiveType.Cylinder;
	}

    public void selectPart (string type) {
		if (assembler.piece != null) {
			return;
		}

		if (assembler.currentType != null) {
			if (type == assembler.currentType) {
				return;
			}
		}

		piece = GameObject.CreatePrimitive(typeObjects[type]);
		piece.AddComponent<Rigidbody>();
		piece.GetComponent<Rigidbody> ().isKinematic = true;
		assembler.setPiece(piece);
    }
}
