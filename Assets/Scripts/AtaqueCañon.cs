using UnityEngine;
using System.Collections;

public class AtaqueCañon : MonoBehaviour {

	public string pushButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (pushButton)) {
			GameObject bala = Instantiate (Resources.Load("Bola de cañon") as GameObject, new Vector3(0,0,0), Quaternion.identity, this.gameObject.transform)  as GameObject;
			//Debug.Log (bala.transform.position);
			//Debug.Log (bala.transform.position);
			//Debug.Log (bala.transform.localPosition);
			bala.transform.localPosition = new Vector3 (0.08F, 0.025F, -0.05F);
			if (this.transform.parent.tag == "Player1") {
				bala.layer = 12;
			} else if (this.transform.parent.tag == "Player2") {
				bala.layer = 13;
			}
			//Debug.Log (bala.transform.localPosition);

		}
	}
}
