﻿using UnityEngine;
using System.Collections;

public class mainPlayer : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.W)) {
			transform.position = new Vector2(transform.position.x, transform.position.y + 1.0f);
		} else if (Input.GetKeyDown (KeyCode.S)) {
			transform.position = new Vector2(transform.position.x, transform.position.y - 1.0f);
		} else if (Input.GetKeyDown (KeyCode.A)) {
			transform.position = new Vector2(transform.position.x - 1.0f, transform.position.y);
		} else if (Input.GetKeyDown (KeyCode.D)) {
			transform.position = new Vector2(transform.position.x + 1.0f, transform.position.y);
		}
	}
}
