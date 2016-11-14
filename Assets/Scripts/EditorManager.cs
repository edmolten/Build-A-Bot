using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EditorManager : MonoBehaviour {
	public GameObject cameraBot;
	public float distanceCamera;
	public Canvas canvas; //reference to the canvas associated to the camera of the bot.
	public Vector3 positionEditor; //position to set at the start of the game.
	public GameObject[] weapons;
	public int layerWeapon1 = 12;
	public int layerWeapon2 = 13;

	private Vector3 initPosCar; //save the initial position spawn.
	private GameObject bot; //reference to the bot owner.
	private Rigidbody rbBot; // reference to the rBody of the bot.
	private FixedJoint joinPointFixed;
	private ConfigurableJoint joinPointConfigurable;
	private int currentWeapon = 0;

	//Selection of joint point
	private int indexCurrentPoint = 0;
	private GameObject pointsJoin;
	private GameObject currentPointJoin = null;

	static bool player1Ready = false;
	static bool player2Ready = false;

	// Use this for initialization
	void Start () {
		bot = this.gameObject;
		rbBot = bot.GetComponent<Rigidbody> ();
		//joinPointFixed = bot.GetComponent<FixedJoint> ();
		//joinPointConfigurable = bot.GetComponent<ConfigurableJoint> ();
		pointsJoin = bot.transform.Find ("PositionWeapon").gameObject;
	
		initPosCar = bot.transform.position;
		bot.transform.position = positionEditor;

		//modify started properties to avoid the bot fall
		rbBot.useGravity = false;
		rbBot.isKinematic = true;

		cameraBot.GetComponent<SmoothFollow> ().enabled = false;
		cameraBot.transform.position = new Vector3(positionEditor.x, positionEditor.y, distanceCamera);

		nextPointJoin ();

		//attachWeapon ();

		//score y time desaparece
		GameObject.Find ("Score 1").transform.localScale = Vector3.zero;
		GameObject.Find ("Score 2").transform.localScale = Vector3.zero;
		GameObject.Find ("Time 1").transform.localScale = Vector3.zero;
		GameObject.Find ("Time 2").transform.localScale = Vector3.zero;

	}

	void Update(){
	/*	if (this.bot.name == "Bot 1") {
			if (Input.GetButtonDown ("R1")) {
				nextPointJoin ();
			} else if (Input.GetButtonDown ("R2")) {
				attachWeapon ();
			} else if (Input.GetButtonDown ("start")) {
				sendToArena ();
			}
		}*/

	}
	//Select the next point to join
	public void nextPointJoin() {
		if (currentPointJoin != null) {
			currentPointJoin.GetComponent<MeshRenderer> ().enabled = false;
		}

		indexCurrentPoint = (indexCurrentPoint + 1) % pointsJoin.transform.childCount;
		currentPointJoin = pointsJoin.transform.GetChild (indexCurrentPoint).gameObject;
		currentPointJoin.GetComponent<MeshRenderer> ().enabled = true;
	}

	public void attachWeapon(){
		currentWeapon = (currentWeapon + 1) % weapons.Length;

		GameObject weapon = Instantiate (weapons [currentWeapon]);

		if (currentPointJoin.transform.childCount == 1) {
			Destroy (currentPointJoin.transform.GetChild(0).gameObject);
		}
			
		weapon.transform.parent = currentPointJoin.transform;
		weapon.transform.localRotation = weapon.transform.rotation;
		joinPointConfigurable = currentPointJoin.GetComponent<ConfigurableJoint> ();
		joinPointFixed = currentPointJoin.GetComponent<FixedJoint> ();

		if (weapon.name.StartsWith ("Sierra")) {
			weapon.transform.localPosition = new Vector3 (0f, 0f, 0.3f);
			joinPointConfigurable.connectedBody = weapon.GetComponent<Rigidbody> ();
			joinPointConfigurable.anchor = currentPointJoin.transform.localPosition + weapon.transform.localPosition;

			joinPointConfigurable.xMotion = ConfigurableJointMotion.Locked;
			joinPointConfigurable.yMotion = ConfigurableJointMotion.Locked;
			joinPointConfigurable.zMotion = ConfigurableJointMotion.Locked;
			joinPointConfigurable.angularXMotion = ConfigurableJointMotion.Locked;
			joinPointConfigurable.angularYMotion = ConfigurableJointMotion.Free;
			joinPointConfigurable.angularZMotion = ConfigurableJointMotion.Locked;
			String tag;
			int layer;
			if (this.bot.name == "Bot 1") {
				tag = "Player1";
				layer = layerWeapon1;
			} else {
				tag = "Player2";
				layer = layerWeapon2;
			}
			weapon.tag = tag;
			weapon.layer = layer;
		} else if (weapon.name.StartsWith ("Ariete")) {
			weapon.transform.position = currentPointJoin.transform.position + new Vector3 (0f, 0f, -0.3f);
			weapon.transform.Rotate(0, currentPointJoin.transform.rotation.y, 0);
			joinPointFixed.connectedBody = weapon.GetComponent<Rigidbody> ();
			String tag;
			int layer;
			if (this.bot.name == "Bot 1") {
				tag = "Player1";
				layer = layerWeapon1;
			} else {
				tag = "Player2";
				layer = layerWeapon2;
			}
			weapon.tag = tag;
			weapon.layer = layer;
		} else if (weapon.name.StartsWith ("Bola Demoledora")) {
			weapon.transform.position = currentPointJoin.transform.position;
			weapon.transform.Rotate(0, currentPointJoin.transform.rotation.y + 180, 0);
			joinPointConfigurable.connectedBody = weapon.transform.Find ("Conexion1").gameObject.GetComponent<Rigidbody> ();
			joinPointConfigurable.xMotion = ConfigurableJointMotion.Locked;
			joinPointConfigurable.yMotion = ConfigurableJointMotion.Locked;
			joinPointConfigurable.zMotion = ConfigurableJointMotion.Locked;
			joinPointConfigurable.angularXMotion = ConfigurableJointMotion.Free;
			joinPointConfigurable.angularYMotion = ConfigurableJointMotion.Free;
			joinPointConfigurable.angularZMotion = ConfigurableJointMotion.Free;
			String tag;
			int layer;
			if (this.bot.name == "Bot 1") {
				tag = "Player1";
				layer = layerWeapon1;
			} else {
				tag = "Player2";
				layer = layerWeapon2;
			}
			weapon.tag = tag;
			weapon.layer = layer;
			GameObject go = weapon.transform.Find ("Conexion1").gameObject;
			go.tag = tag;
			go.layer = layer;
			go = weapon.transform.Find ("Conexion2").gameObject;
			go.tag = tag;
			go.layer = layer;
			go = weapon.transform.Find ("Conexion3").gameObject;
			go.tag = tag;
			go.layer = layer;
			go = weapon.transform.Find ("Conexion4").gameObject;
			go.tag = tag;
			go.layer = layer;
			go = weapon.transform.Find ("Conexion5").gameObject;
			go.tag = tag;
			go.layer = layer;
			go = weapon.transform.Find ("Bola").gameObject;
			go.tag = tag;
			go.layer = layer;
		} else if (weapon.name.StartsWith ("Cañon")) {
			weapon.transform.position = currentPointJoin.transform.position + new Vector3 (0f, 0f, -0.3f);
			weapon.transform.Rotate(0, currentPointJoin.transform.rotation.y, 0);
			joinPointFixed.connectedBody = weapon.GetComponent<Rigidbody> ();
			String tag;
			int layer;
			if (this.bot.name == "Bot 1") {
				tag = "Player1";

				foreach (Transform child in weapon.transform){
					child.gameObject.tag = tag;
				}
				layer = layerWeapon1;
			} else {
				tag = "Player2";
				layer = layerWeapon2;
				foreach (Transform child in weapon.transform){
					child.gameObject.tag = tag;
				}
			}
			weapon.tag = tag;
			weapon.layer = layer;

		} else if (weapon.name.StartsWith ("Pala")) {
			weapon.transform.position = currentPointJoin.transform.position;
			//weaon.transform.localPosition += new Vector3(,,-0,)
			weapon.transform.Rotate(0, currentPointJoin.transform.rotation.y, 0);
			joinPointFixed.connectedBody = weapon.GetComponent<Rigidbody> ();
			String tag;
			int layer;
			if (this.bot.name == "Bot 1") {
				tag = "Player1";
				layer = layerWeapon1;
			} else {
				tag = "Player2";
				layer = layerWeapon2;
			}
			weapon.tag = tag;
			weapon.layer = layer;
		}
	}

	public void sendToArena() {
		//score aparece
		if (this.bot.name == "Bot 1") {
			player1Ready = true;
		} else {
			player2Ready = true;
		}

		if (player1Ready && player2Ready) {
			//startTimer
			Timer timer = GameObject.Find ("TimerObject").GetComponent<Timer> ();
			GameObject.Find ("Score 2").transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
			GameObject.Find ("Time 2").transform.localScale =  new Vector3(1.5f,1.5f,1.5f);
			GameObject.Find ("Score 1").transform.localScale =  new Vector3(1.5f,1.5f,1.5f);
			GameObject.Find ("Time 1").transform.localScale =  new Vector3(1.5f,1.5f,1.5f);
			timer.started = true;
		}

		//set back the normal properties.
		bot.transform.position = initPosCar;
		rbBot.useGravity = true;
		rbBot.isKinematic = false;

		//Enable/disable scripts
		cameraBot.GetComponent<SmoothFollow> ().enabled = true;
		bot.GetComponent<EditorManager> ().enabled = false;
		bot.GetComponent<FollowCamera> ().enabled = false;
		bot.GetComponent<MovimientoBot> ().initialPosition = initPosCar;

		//Disable UI components
		Transform backGround = canvas.gameObject.transform.Find("BgEditor");
		Transform containerMenu = canvas.gameObject.transform.Find("Container");
		for (int i = 0; i < 4; i++) {
			GameObject currentPointJoin = pointsJoin.transform.GetChild (i).gameObject;
			currentPointJoin.GetComponent<MeshRenderer> ().enabled = false;

		}

		Destroy (containerMenu.gameObject);
		Destroy (backGround.gameObject);
		Destroy (this);

	}
}