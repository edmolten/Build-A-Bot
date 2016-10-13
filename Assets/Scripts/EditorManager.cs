using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EditorManager : MonoBehaviour {
	public GameObject camera;
	public float distanceCamera;
	public GameObject canvas; //reference to the canvas associated to the camera of the bot.
	public Vector3 positionEditor; //position to set at the start of the game.
	public GameObject[] weapons = new GameObject[2];

	private Vector3 initPosCar; //save the initial position spawn.
	private GameObject bot; //reference to the bot owner.
	private Rigidbody rbBot; // reference to the rBody of the bot.

	// Use this for initialization
	void Start () {
		bot = this.gameObject;
		rbBot = bot.GetComponent<Rigidbody> ();
	
		initPosCar = bot.transform.position;
		bot.transform.position = positionEditor;

		//modify started properties to avoid the bot fall
		rbBot.useGravity = false;
		rbBot.isKinematic = true;

		camera.GetComponent<SmoothFollow> ().enabled = false;
		camera.transform.position = new Vector3(positionEditor.x, positionEditor.y, distanceCamera);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void sendToArena(Button button) {
		//set back the normal properties.
		bot.transform.position = initPosCar;
		rbBot.useGravity = true;
		rbBot.isKinematic = false;

		button.gameObject.SetActive(false);

		//Enable/disable scripts
		camera.GetComponent<SmoothFollow> ().enabled = true;
		bot.GetComponent<EditorManager> ().enabled = false;
		bot.GetComponent<FollowCamera> ().enabled = false;
	}
}