﻿using UnityEngine;
using System.Collections;

public class WallsRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0.1f,0.3f,0.6f);
	}
}
