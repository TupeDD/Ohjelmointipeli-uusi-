using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelaaja : MonoBehaviour {

	public float freeze = 0;

	private bool closeToFire = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (closeToFire) {
			freeze -= 0.001f;
		} else {
			freeze += 0.0002f;
		}
		GetComponentInChildren<FrostEffect> ().FrostAmount = freeze;
	}

	void OnTriggerEnter(Collider col) {
		closeToFire = true;
	}
	void OnTriggerExit(Collider col) {
		closeToFire = false;
	}
}
