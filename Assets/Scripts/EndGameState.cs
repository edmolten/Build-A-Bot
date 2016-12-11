using UnityEngine;
using System.Collections;

public class EndGameState : MonoBehaviour {

    public GameObject cameraBot;
    public Canvas canvas; //reference to the canvas associated to the camera of the bot.

    private GameObject bot; //reference to the bot owner.

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void restartGame() {
        bot = this.gameObject;
        bot.SetActive(true);

        Transform menuFinalBot = canvas.gameObject.transform.Find("MenuGame");
        Transform menuInicialBot = canvas.gameObject.transform.Find("MenuPrincipal");

        menuFinalBot.gameObject.SetActive(false);
        menuInicialBot.gameObject.SetActive(true);

        //desabilitar menu final
        //habilitar menu inicial
        bot.GetComponent<EditorManager>().enabled = true;
        
    }
}
