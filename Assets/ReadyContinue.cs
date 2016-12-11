using UnityEngine;
using System.Collections;

public class ReadyContinue : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void showMessageReady() {
        this.gameObject.transform.Find("Container").gameObject.SetActive(false);
        this.gameObject.transform.Find("Continue").gameObject.SetActive(true);
    }
}
