using UnityEngine;
using System.Collections;

public class MakeCañonAttack : MakeAttack
{
	public override void attack(){
		AtaqueCañon ataqueCañonScript = gameObject.GetComponent<AtaqueCañon> ();
		if (!ataqueCañonScript.isOnCooldown) {
			ataqueCañonScript.attack = true;
		}
			
	}
}

