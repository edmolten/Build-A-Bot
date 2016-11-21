using UnityEngine;
using System.Collections;

public class AtaquePala : MonoBehaviour {

	public float fuerza;
	public string pushButton;
	//private float cooldown;
	public float attackTime;
	public bool isAttacking;
	private Animation attackAnimation;
	private Rigidbody collRigidBody;

	// Use this for initialization
	void Start () {
		isAttacking = false;
		attackTime = 1F / 4F;
		attackAnimation = this.transform.parent.GetComponent<Animation> ();
		//cooldown = 3F / 4F;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (pushButton)) {
			attackAnimation.Play("shovelAttack");
			isAttacking = true;
		}
		if (isAttacking) {
			attackTime -= Time.deltaTime;
			if (attackTime < 0) {
				attackTime = 1F / 4F;
				isAttacking = false;
			}
		}
	}

	void OnCollisionEnter(Collision coll){
		collRigidBody = coll.gameObject.GetComponent<Rigidbody> ();
		string thisTag = this.tag;
		string otherTag = coll.gameObject.tag;
		if(isAttacking && attackTime >= 0 && ((thisTag == "Player1" && otherTag == "Player2") || (thisTag == "Player2" && otherTag == "Player1"))){
			//AudioSource audio = GetComponent<AudioSource> ();
			//audio.Play ();
			collRigidBody.AddForce (- this.gameObject.transform.up * fuerza, ForceMode.VelocityChange);
			attackTime = 1F / 4F;
			isAttacking = false;
		}
	}
}
