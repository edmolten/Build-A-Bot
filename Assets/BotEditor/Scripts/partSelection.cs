using UnityEngine;
using System.Collections;

public class partSelection : MonoBehaviour {
    public GameObject target = null; //should be the bot
    public GameObject piece = null;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void addReferenceToPiece(GameObject pieceRef) { //Ealuar mejor forma de pasar el objeto por referencia
        piece = pieceRef;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)){
            if (target != null && piece != null) {
                piece.transform.parent = target.transform;
                piece = null;
            }
        }
    }
}
