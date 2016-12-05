using UnityEngine;
using System.Collections;

public class MakePalaAttack : MakeAttack
{
	public override void attack(){
		gameObject.GetComponent<AtaquePala> ().attack = true;
	}
}

