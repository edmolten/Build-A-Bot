using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Bot 1" || col.gameObject.name == "Bot 2")
		{
			Destroy(col.gameObject);

		}
	}
}
