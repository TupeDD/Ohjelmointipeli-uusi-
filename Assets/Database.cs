using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Database : MonoBehaviour {

	public string[] items;

	// Use this for initialization
	IEnumerator Start () {
		this.gameObject.GetComponent<Text> ().text = "";

		WWW itemData = new WWW ("http://localhost/Unity(ohjelmointipeli)/index.php?security=NeverGonnaLetYouDownXD&function=getData");
		yield return itemData;
		string itemDataString = itemData.text;

		items = itemDataString.Split (';');

		foreach (string item in items) {
			if (item.Length > 0) {
				this.gameObject.GetComponent<Text> ().text += item+"\n";
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
