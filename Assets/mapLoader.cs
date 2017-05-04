using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapLoader : MonoBehaviour {

	public static int mapNum;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void map1() {
		mapNum = 1;
		SceneManager.LoadScene ("Peli");
	}
	public void map2() {
		mapNum = 2;
		SceneManager.LoadScene ("Peli");
	}
	public void map3() {
		mapNum = 3;
		SceneManager.LoadScene ("Peli");
	}
}
