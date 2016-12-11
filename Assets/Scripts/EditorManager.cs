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

	}

	void Update(){
		//Y -> 0
		//B -> 1
		//A -> 2
		//X -> 3
		//L1 -> 4
		//R1 -> 5
		//L2 -> 6
		//R2 -> 7
		//selec ->8
		//star -> 9
		//L3 -> 10
		//R3 -> 11
		String playerNumber = this.bot.name.Split ()[1];
		if (Input.GetButtonDown ("R1"+playerNumber)) {
			nextPointJoin (); 

		} else if (Input.GetButtonDown ("R2"+playerNumber)) {
			attachWeapon (); 

		} else if (Input.GetButtonDown ("start"+playerNumber)) {
			sendToArena (); 
		}

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

		Instantiate (weapons [currentWeapon], currentPointJoin.transform);
		
		currentWeapon = (currentWeapon + 1) % weapons.Length;
	}

	public void sendToArena() {
		//score aparece
		if (this.bot.name == "Bot 1") {
			player1Ready = true;
		} else {
			player2Ready = true;
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
		Transform menuPricipal = canvas.gameObject.transform.Find("MenuPrincipal");
		Transform menuGame = canvas.gameObject.transform.Find("MenuGame");

		menuPricipal.gameObject.SetActive (false);
		menuGame.gameObject.SetActive (true);
		canvas.planeDistance = 0.31f;

		if (player1Ready && player2Ready) {
			//startTimer
			Timer timer = GameObject.Find ("TimerObject").GetComponent<Timer> ();
			timer.setUpTimer ();
		}

		currentPointJoin.GetComponent<MeshRenderer> ().enabled = false;

	}
}