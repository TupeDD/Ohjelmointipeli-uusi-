using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seuraaja : MonoBehaviour {

	public GameObject pelaaja;
	Vector3 loca;

	float x;
	float y;
	float z;

	// Use this for initialization
	void Start () {
		kohdista ();
	}
	
	// Update is called once per frame
	void Update () {
		kohdista ();
	}

	public void kohdista() {
		x = pelaaja.transform.position.x;
		y = this.transform.position.y;
		z = pelaaja.transform.position.z;
		loca = new Vector3 (x, y, z);
		transform.position = loca;
	}
}
