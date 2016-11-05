using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EditorManager : MonoBehaviour {
	public GameObject cameraBot;
	public float distanceCamera;
	public Canvas canvas; //reference to the canvas associated to the camera of the bot.
	public Vector3 positionEditor; //position to set at the start of the game.
	public GameObject[] weapons = new GameObject[2];
	public int layerWeapon = 12;

	private Vector3 initPosCar; //save the initial position spawn.
	private GameObject bot; //reference to the bot owner.
	private Rigidbody rbBot; // reference to the rBody of the bot.
	private FixedJoint joinPointFixed;
	private ConfigurableJoint joinPointConfigurable;
	private int currentWeapon = 0;
	private GameObject refWeapon = null;

	//Selection of joint point
	private int indexCurrentPoint = 0;
	private GameObject pointsJoin;
	private GameObject currentPointJoin = null;

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

	//Select the next point to join
	public void nextPointJoin() {
		if (currentPointJoin != null) {
			currentPointJoin.GetComponent<MeshRenderer> ().enabled = false;
		}

		indexCurrentPoint = (indexCurrentPoint + 1) % pointsJoin.transform.childCount;
		currentPointJoin = pointsJoin.transform.GetChild (indexCurrentPoint).gameObject;
		currentPointJoin.GetComponent<MeshRenderer> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void attachWeapon(){
		currentWeapon = (currentWeapon + 1) % weapons.Length;

		GameObject weapon = Instantiate (weapons [currentWeapon]);

		if (currentPointJoin.transform.childCount == 1) {
			Destroy (currentPointJoin.transform.GetChild(0).gameObject);
		}
			
		weapon.transform.parent = currentPointJoin.transform;
		weapon.layer = layerWeapon;
		weapon.transform.localRotation = weapon.transform.rotation;
		joinPointConfigurable = currentPointJoin.GetComponent<ConfigurableJoint> ();
		joinPointFixed = currentPointJoin.GetComponent<FixedJoint> ();

		if (weapon.name == "Sierra" || weapon.name == "Sierra(Clone)") {
			weapon.transform.localPosition = new Vector3(0f, 0f, 0.3f);
			//weapon.transform.position = transform.InverseTransformPoint(pointJoin.transform.position) + new Vector3(0f,0f,0.3f);
			joinPointConfigurable.connectedBody = weapon.GetComponent<Rigidbody> ();
			joinPointConfigurable.anchor = currentPointJoin.transform.localPosition + weapon.transform.localPosition;

			joinPointConfigurable.xMotion = ConfigurableJointMotion.Locked;
			joinPointConfigurable.yMotion = ConfigurableJointMotion.Locked;
			joinPointConfigurable.zMotion = ConfigurableJointMotion.Locked;
			joinPointConfigurable.angularXMotion = ConfigurableJointMotion.Locked;
			joinPointConfigurable.angularYMotion = ConfigurableJointMotion.Free;
			joinPointConfigurable.angularZMotion = ConfigurableJointMotion.Locked;

		} else if (weapon.name == "Ariete(Clone)") {
			weapon.transform.position = currentPointJoin.transform.position + new Vector3(0f,0f,-0.3f);
			joinPointFixed.connectedBody = weapon.GetComponent<Rigidbody> ();
		}
	}

	public void sendToArena() {
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

		Destroy (containerMenu.gameObject);
		Destroy (backGround.gameObject);
		Destroy (this);

	}
}