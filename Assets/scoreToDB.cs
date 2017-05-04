using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scoreToDB : MonoBehaviour {

	public GameObject inputField;
	private int pisteet;
	private string nimi;

	// Use this for initialization
	IEnumerator Start () {
		inputField = GameObject.FindGameObjectWithTag ("input");

		pisteet = SceneController.pisteet;
		nimi = inputField.GetComponent<Text>().text;
		if (nimi.Length > 0) {
			WWW itemData = new WWW ("http://localhost/Unity(ohjelmointipeli)/index.php?security=NeverGonnaLetYouDownXD&function=postData&nimi=" + nimi + "&pisteet=" + pisteet);
			yield return itemData;
		} else {
			WWW itemData = new WWW ("http://localhost/Unity(ohjelmointipeli)/index.php?security=NeverGonnaLetYouDownXD&function=postData&pisteet="+pisteet);
			yield return itemData;
		}

		SceneManager.LoadScene ("Alkusivu");
		Destroy (this);

	}
	
	// Update is called once per frame
	void Update () {
	}
}
