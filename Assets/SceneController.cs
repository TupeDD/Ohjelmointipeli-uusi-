using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

	private string skene;
	public GameObject komentoAlue;
	public GameObject button;
	public bool buttonActive = true;
	private bool done = false;
	public GameObject Liiku;
	public GameObject Kaanny;

	// Loppusivu
	public GameObject aikaText;
	public GameObject pisteText;
	public float time;
	public static int pisteet;
	public static int rounds;
	public static int deaths;
	public static int roundActions;

	public GameObject FPS;

	// Mapit ja mapNum
	public int mapNum;
	public GameObject map1;
	public GameObject map2;
	public GameObject map3;
	public Material sky1;
	public Material sky2;
	public Material sky3;

	// Use this for initialization
	void Start () {
		skene = SceneManager.GetActiveScene().name;

		if (skene == "Peli") {
			mapNum = mapLoader.mapNum;
			if (mapNum == 1) {
				FPS.GetComponentInChildren<FrostEffect> ().enabled = false;
				GetComponent<Light> ().intensity = 1.25f;
				RenderSettings.skybox = sky1;
				RenderSettings.ambientIntensity = 2.27f;
				map1.SetActive (true);
				map2.SetActive (false);
				map3.SetActive (false);
			} else if (mapNum == 2) {
				FPS.GetComponentInChildren<FrostEffect> ().enabled = true;
				GetComponent<Light> ().intensity = 0.1f;
				RenderSettings.skybox = sky2;
				RenderSettings.ambientIntensity = 0;
				map1.SetActive (false);
				map2.SetActive (true);
				map3.SetActive (false);
				print (map2.activeSelf);
			} else if (mapNum == 3) {
				FPS.GetComponentInChildren<FrostEffect> ().enabled = false;
				GetComponent<Light> ().intensity = 1.25f;
				RenderSettings.skybox = sky3;
				RenderSettings.ambientIntensity = 2.27f;
				map1.SetActive (false);
				map2.SetActive (false);
				map3.SetActive (true);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Slot.toiminnot = komentoAlue.transform.childCount;
		if (Slot.toiminnot == 5) {
			Liiku.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			Kaanny.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		} else {
			Liiku.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			Kaanny.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		}
		if (skene == "Alkusivu") {
			
		} 
		else if (skene == "Peli") {
			
		}
		else if (skene == "Loppu") {
			rounds = Peli.rounds;
			deaths = Peli.deaths;
			roundActions = Peli.roundActions;
			pisteet = 1 - (deaths + rounds);
		}
		if (buttonActive && !Peli.suorita && komentoAlue.transform.childCount > 0) {
			button.GetComponent<Button> ().interactable = true;
		} else {
			button.GetComponent<Button> ().interactable = false;
		}
		if (!buttonActive && !done) {
			int maara = komentoAlue.transform.childCount;
			if (maara > 0) {
				Peli.suorita = true;
				for(int i = 0; i < maara; i++) {
					if (komentoAlue.transform.GetChild(i).tag == "liiku") {
						int askeleet = int.Parse(komentoAlue.transform.GetChild(i).transform.GetChild(0).FindChild("Askeleet").GetComponent<Text> ().text);
						string komento = "liiku";
						//this.gameObject.SendMessage(komento, askeleet);
						Peli.TOIMINTA.Add(komento);
						Peli.TOIMINTA2.Add(""+askeleet);

					} 
					else if (komentoAlue.transform.GetChild(i).tag == "kaanny") {
						string komento = "kaanny";
						string suunta = komentoAlue.transform.GetChild(i).transform.GetChild(0).FindChild("Button").transform.GetChild(0).GetComponent<Text>().text;
						if (suunta == "Vasen") {
							suunta = "vasen";
						} else {
							suunta = "oikea";
						}
						//this.gameObject.SendMessage(komento, suunta);
						Peli.TOIMINTA.Add(komento);
						Peli.TOIMINTA2.Add(suunta);
					}
					komentoAlue.transform.GetChild (i).GetComponent<CanvasGroup> ().blocksRaycasts = false;
				}
			}
			done = true;
		}
	}

	public void Aloita() {
		buttonActive = false;
		Peli.rounds++;
	}

	public void delayAloita() {
		FPS.SendMessage ("kameranVaihto");
		Invoke ("Aloita", 0.5f);
	}

	public void emptyToiminnot() {
		//FPS.SendMessage ("kameranVaihto");
		for(int i = 0; i < komentoAlue.transform.childCount; i++) {
			Destroy (komentoAlue.transform.GetChild(i).gameObject);
		}
		buttonActive = true;
		done = false;
	}
}
