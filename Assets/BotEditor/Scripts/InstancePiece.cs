using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstancePiece : MonoBehaviour {
	public AssemblerPiece assembler = null;
	public GameObject[] piecesPrefab = new GameObject[2];

	private GameObject piece = null;

	// Use this for initialization
	void Start () {
       
	}

	public void selectPart (int index) {
		Debug.Log ("QWeqweqwewqe");
		if (assembler.piece != null) {
			return;
		}

		if (assembler.currentType != -1) {
			if (index == assembler.currentType) {
				return;
			}
		}

		assembler.currentType = index;
		piece = Instantiate(piecesPrefab[index]);
		piece.GetComponent<Rigidbody> ().isKinematic = true;
		piece.GetComponent<Rigidbody> ().useGravity = false;
		assembler.setPiece(piece);
    }
}
