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
	private int currentWeapon = 1;

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

		if (currentPointJoin.transform.childCount == 1) {
			Destroy (currentPointJoin.transform.GetChild(0).gameObject);
		}

		GameObject weapon = Instantiate (weapons [currentWeapon], currentPointJoin.transform) as GameObject;
		
		currentWeapon = (currentWeapon + 1) % weapons.Length;
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