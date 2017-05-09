using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour {

	public GameObject explosionEffect;
	private Vector3 orgPos;
	private string Name = "";

	// Use this for initialization
	void Start () {
		orgPos = gameObject.transform.position;
		name = gameObject.transform.parent.name;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void explode() {
		Invoke ("reset", 1f);
		if (Name == "cannon1") {
			explosionEffect.SetActive (true);
			gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3(0, 0, -1500f));
		} else if (Name == "cannon2") {
			explosionEffect.SetActive (true);
			gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3(-1500f, 0, 0));
		} else {
			explosionEffect.SetActive (true);
			gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3(1500f, 0, 0));
		}
	}

	public void disable() {
		gameObject.transform.parent.gameObject.transform.Find ("SmallExplosionEffect").gameObject.SetActive (false);
		gameObject.SetActive (false);
	}

	public void reset() {
		explosionEffect.SetActive (false);
		gameObject.transform.position = orgPos;
		disable ();
	}
}
