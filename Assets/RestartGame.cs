using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RestartGame : MonoBehaviour {

    private bool[] playersReady = {false, false};

	// Use this for initialization
	void Start () {
        int i;

        for (i = 0; i < playersReady.Length; i++)
        {
            playersReady[i] = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void restartGame(int indexPlayer) {
        int i;

        playersReady[indexPlayer] = true;

        for (i = 0; i < playersReady.Length; i++) {
            if (!playersReady[i]) {
                return;
            }
        }

        Scene currentScene = SceneManager.GetActiveScene();
        GameObject[] gameObjectsScene = currentScene.GetRootGameObjects();

        for (i = 0; i < gameObjectsScene.Length; i++) {
            Destroy(gameObjectsScene[i]);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
