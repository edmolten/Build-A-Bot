using UnityEngine;
using System.Collections;
using System;

public class Attack : MonoBehaviour {

	String playerNumber;
	// Use this for initialization
	void Start () {
		this.playerNumber = this.gameObject.name.Split ()[1];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Y" + playerNumber)) {
			attackInSide ("Frente");
		}
		else if (Input.GetButtonDown ("B" + playerNumber)) {
			attackInSide ("Derecha");
		}
		else if (Input.GetButtonDown ("A" + playerNumber)) {
			attackInSide ("Atras");
		}
		else if (Input.GetButtonDown ("X" + playerNumber)) {
			attackInSide ("Izquierda");
		}
	}

	void attackInSide(String side){
		Transform frente = this.transform.Find ("PositionWeapon").Find (side);
		if (frente.childCount > 0) {
			Transform weapon = frente.GetChild (0);
			MakeAttack makeAttackScript = weapon.gameObject.GetComponent<MakeAttack> ();
			if (makeAttackScript != null) {
				Debug.Log ("asd");
				makeAttackScript.attack();
			}
		}
	}
}
