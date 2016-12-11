using UnityEngine;
using System.Collections;


public class Init : MonoBehaviour {

	private AudioSource musicaEscogida;
	private UnityEngine.UI.Text nombreMusica;
	public AudioClip[] musicas;

	// Use this for initialization
	void Start () {

		nombreMusica = GameObject.Find ("Canvas 1").transform.GetChild (0).GetChild (1).GetChild (3).GetChild (0).gameObject.GetComponent<UnityEngine.UI.Text> ();

		musicas = new AudioClip[4];

		musicaEscogida = this.gameObject.GetComponent<AudioSource> ();
		musicas.SetValue (Resources.Load<AudioClip> ("Sounds/TheFatRat - Infinite Power"), 0);
		musicas.SetValue (Resources.Load<AudioClip> ("Sounds/TheFatRat - Time Lapse"), 1);
		musicas.SetValue (Resources.Load<AudioClip> ("Sounds/TheFatRat - Unity"), 2);
		musicas.SetValue (Resources.Load<AudioClip> ("Sounds/TheFatRat - Windfall"), 3);

		int random = Random.Range (0, 3);
		musicaEscogida.clip = musicas[random];
		nombreMusica.text = "Song: " + musicas [random].name;
		musicaEscogida.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
