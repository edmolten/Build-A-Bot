using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RandomValue : MonoBehaviour {
    public int min = 500;
    public int max = 1000;

    // Use this for initialization
    void Start () {
        float rnd = Random.value;
        rnd = min + rnd * (max - min);
        this.gameObject.GetComponent<Text>().text = Mathf.Round(rnd).ToString();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
