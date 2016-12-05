using UnityEngine;
using System.Collections;

public class MakeArieteAttack : MakeAttack
{
	public override void attack(){
		gameObject.GetComponent<AtaqueAriete> ().attack = true;
	}
}

