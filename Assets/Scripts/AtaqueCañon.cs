using UnityEngine;
using System.Collections;

public class AtaqueCañon : MonoBehaviour {

	public string pushButton;
	public float cooldown;

	public bool isOnCooldown;
	private float cooldownTimer;
	private int layerWeapon1 = 12;
	private int layerWeapon2 = 13;

	// Use this for initialization
	void Start () {
		cooldownTimer = 0f;
		cooldown = 1f;
		isOnCooldown = false;

		this.transform.localPosition = new Vector3 (-0.25f, -0.1f, -0.3f);
		this.transform.Rotate(0, this.transform.parent.eulerAngles.y, 0);

		this.transform.name = "Cañon_" + this.transform.parent.name;
		this.transform.GetChild (0).name += "_" + this.transform.parent.name;

		FixedJoint joinPoint = this.transform.parent.gameObject.AddComponent<FixedJoint> ();
		joinPoint.connectedBody = this.transform.GetChild(0).GetComponent<Rigidbody> ();

		string tag;
		int layer;
		if (this.transform.parent.parent.parent.name == "Bot 1") {
			tag = "Player1";
			layer = layerWeapon1;
			foreach (Transform child in this.transform){
				child.gameObject.tag = tag;
				child.gameObject.layer = layer;
			}
		} else {
			tag = "Player2";
			layer = layerWeapon2;
			foreach (Transform child in this.transform){
				child.gameObject.tag = tag;
				child.gameObject.layer = layer;
			}
		}
		this.tag = tag;
		this.gameObject.layer = layer;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (pushButton) && isOnCooldown == false) {

			isOnCooldown = true;
			GameObject bala = Instantiate (Resources.Load("Bola de cañon") as GameObject, new Vector3(0,0,0), Quaternion.identity, this.gameObject.transform)  as GameObject;
			bala.transform.localPosition = new Vector3 (0.08F, 0.025F, -0.05F);
			if (this.transform.parent.tag == "Player1") {
				bala.layer = 12;
			} else if (this.transform.parent.tag == "Player2") {
				bala.layer = 13;
			}
		}

		if (isOnCooldown == true) {
			cooldownTimer += Time.deltaTime;
			if (cooldownTimer >= cooldown) {
				isOnCooldown = false;
				cooldownTimer = 0;
			}
		}
	}

	void OnDestroy(){
		GameObject parent = this.transform.parent.gameObject;
		Destroy (parent.GetComponent<FixedJoint> ());
	}
}
