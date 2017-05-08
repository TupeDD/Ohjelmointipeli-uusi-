using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Peli : MonoBehaviour {

	// Liiku paneelin askeleet ja suunta
	public static GameObject liiku_maara;
	public static GameObject suuntaText;

	// Kamerat ja Canvasit
	public Camera fps_cam;
	public Camera main_cam;
	public Camera mini_cam;
	public Canvas mainCanvas;
	public Canvas fpsCanvas;

	// Pelaaja
	public GameObject pelaaja;

	// Pisteet
	public static int coins = 0;
	public static int deaths = 0;
	public static int rounds = 0;
	public static int roundActions = 0;
	public int coinsToWin = 4;

	// Unityn FirstPersonController scripti pelaajassa
	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fps;

	// Pelaajan koordinaatit ja suunta
	public float x;
	public float x_kopio;
	public float z;
	public float z_kopio;
	public float rot_y;
	public float temp_rot_y;
	public string suunta = "up";
	public int askeleet;
	public float turnspeed = 1f;
	public string ksuunta;
	public bool Liiku = false;
	public bool Kaanny = false;

	public float temp_rot = 0;
	private Vector3 originalPosition;
	private Quaternion originalRotation;

	public static List<string> TOIMINTA;
	public static List<string> TOIMINTA2;
	public int Laskin = 0;
	public bool waiting = false;
	public int suuntaNumero = 0;
	public static bool suorita = false;
	public bool die = false;
	public int die_z;

	public GameObject dirLight;
	public GameObject Coin;
	public GameObject canvasCoins;
	public GameObject fpsCoins;

	// Frost
	private float freeze = 0f;
	private bool closeToFire = false;
	private bool freezeOn;

	// Äänet
	public AudioClip collect;
	public AudioClip burn;
	public AudioClip vic1;
	public AudioClip vic2;
	public AudioClip vic3;
	AudioSource audio;

	// Tykit Terrain3
	public GameObject can1;
	public GameObject can2;
	public GameObject can3;

	public GameObject deathScreen;

	// Use this for initialization
	void Start () {
		liiku_maara = GameObject.FindGameObjectWithTag ("Askeleet");
		suuntaText = GameObject.FindGameObjectWithTag ("suuntaText");
		//liiku (5);
		//kaanny("oikea");
		audio = GetComponent<AudioSource>();
		coinsToWin = GameObject.FindGameObjectsWithTag ("Coin").Length;
		TOIMINTA = new List<string> ();
		TOIMINTA2 = new List<string> ();
		originalPosition = new Vector3 (2.6f, 2f, 2.26f);
		originalRotation = pelaaja.transform.rotation;
		freezeOn = GetComponentInChildren<FrostEffect> ().enabled;
		coins = 0;
	}
	
	// Update is called once per frame
	void Update () {
		canvasCoins.GetComponent<Text> ().text = coins + "";
		fpsCoins.GetComponent<Text> ().text = coins + "";

		// Jäätyminen
		if (freezeOn && mapLoader.mapNum == 2) {
			if (freeze >= 0.6f) {
				Die ();
			}
			if (closeToFire) {
				if (freeze > 0) {
					freeze -= 0.001f;
				}
			} else if (suorita) {
				if (freeze < 0.6f) {
					freeze += 0.0001f;
				}
			}
			GetComponentInChildren<FrostEffect> ().FrostAmount = freeze;
		}

		if (suorita) {
			if (TOIMINTA.Count > 0 && !waiting && Laskin + 1 <= TOIMINTA.Count) {
				if (TOIMINTA [Laskin] == "liiku") {
					waiting = true;
					gameObject.SendMessage (TOIMINTA [Laskin], int.Parse (TOIMINTA2 [Laskin]));
					Laskin++;
				} else {
					waiting = true;
					gameObject.SendMessage (TOIMINTA [Laskin], TOIMINTA2 [Laskin]);
					Laskin++;
				}
			}

			if (Liiku) {
				if (suunta == "up") {
					if (z_kopio >= z + askeleet) {
						Liiku = false;
						fps.Vertical = 0;

						waiting = false;
						if (Laskin >= TOIMINTA.Count) {
							TOIMINTA.Clear ();
							TOIMINTA2.Clear ();
						}
					} else {
						z_kopio = pelaaja.transform.position.z;
					}
				} else if (suunta == "down") {
					if (z_kopio <= z - askeleet) {
						Liiku = false;
						fps.Vertical = 0;

						waiting = false;
						if (Laskin >= TOIMINTA.Count) {
							TOIMINTA.Clear ();
							TOIMINTA2.Clear ();
						}
					} else {
						z_kopio = pelaaja.transform.position.z;
					}
				} else if (suunta == "left") {
					if (x_kopio <= x - askeleet) {
						Liiku = false;
						fps.Vertical = 0;

						waiting = false;
						if (Laskin >= TOIMINTA.Count) {
							TOIMINTA.Clear ();
							TOIMINTA2.Clear ();
						}
					} else {
						x_kopio = pelaaja.transform.position.x;
					}
				} else if (suunta == "right") {
					if (x_kopio >= x + askeleet) {
						Liiku = false;
						fps.Vertical = 0;

						waiting = false;
						if (Laskin >= TOIMINTA.Count) {
							TOIMINTA.Clear ();
							TOIMINTA2.Clear ();
						}
					} else {
						x_kopio = pelaaja.transform.position.x;
					}
				}
			}

			if (Kaanny) {
				if (ksuunta == "vasen") {
					if (temp_rot == 0) {
						Kaanny = false;
						waiting = false;
						vaihdaSuunta (-1);
						if (Laskin >= TOIMINTA.Count) {
							TOIMINTA.Clear ();
							TOIMINTA2.Clear ();
						}
					} else {
						temp_rot -= 0.5f;
						pelaaja.transform.Rotate (0, -0.5f, 0);
					}	
				} else if (ksuunta == "oikea") {
					if (temp_rot == 0) {
						Kaanny = false;
						waiting = false;
						vaihdaSuunta (1);
						if (Laskin >= TOIMINTA.Count) {
							TOIMINTA.Clear ();
							TOIMINTA2.Clear ();
						}
					} else {
						temp_rot -= 0.5f;
						pelaaja.transform.Rotate (0, 0.5f, 0);
					}
				}
			}
			if (TOIMINTA.Count == 0 && TOIMINTA2.Count == 0 && !waiting) {
				endRound ();
			}
		}
		if (die) {
			if (Mathf.Round(pelaaja.transform.localEulerAngles.z) == die_z) {
				die = false;
				deathScreen.gameObject.SetActive (true);
			} 
			else {
				if (die_z < 0) {
					pelaaja.transform.Rotate (0, 0, -1f);
				} else {
					pelaaja.transform.Rotate (0, 0, 1f);
				}
			}
		}
	}

	public void kameranVaihto() {
		if (main_cam.enabled) 
		{
			main_cam.enabled = false;
			fps_cam.GetComponent<Camera> ().targetDisplay = 0;
			mainCanvas.enabled = false;
			fpsCanvas.GetComponent<Canvas> ().targetDisplay = 0;
		}
		else 
		{
			fps_cam.GetComponent<Camera> ().targetDisplay = 1;
			fpsCanvas.GetComponent<Canvas> ().targetDisplay = 1;
			mainCanvas.enabled = true;
			main_cam.enabled = true;
		}
	}

	public void liiku(int montako) {
		roundActions++;
		Liiku = true;
		askeleet = montako*4;
		x = pelaaja.transform.position.x;
		x_kopio = x;
		z = pelaaja.transform.position.z;
		z_kopio = z;
		fps.Vertical = 1f;
		
	}

	public void vaihdaSuunta(int Numero) {
		suuntaNumero += Numero;
		if (suuntaNumero < -1) {
			suuntaNumero = 2;
		}
		if (suuntaNumero > 2) {
			suuntaNumero = -1;
		}
		if (suuntaNumero == 0) {
			suunta = "up";
		} else if (suuntaNumero == 1) {
			suunta = "right";
		} else if (suuntaNumero == 2) {
			suunta = "down";
		} else if (suuntaNumero == -1) {
			suunta = "left";
		}
		if (pelaaja.transform.rotation.y == 0) {
			suunta = "up";
		} else if (pelaaja.transform.rotation.y == 1) {
			suunta = "down";
		}
	}

	public void kaanny(string Suunta) {
		roundActions++;
		temp_rot += 90f;
		Kaanny = true;
		ksuunta = Suunta;
		rot_y = pelaaja.transform.eulerAngles.y;
		temp_rot_y = pelaaja.transform.eulerAngles.y;
	}

	public void minus() {
		int luku = int.Parse (liiku_maara.GetComponent<Text> ().text);
		if (luku > 1) {
			luku -= 1;
			liiku_maara.GetComponent<Text> ().text = "" + luku;
		}
	}

	public void plus() {
		int luku = int.Parse(liiku_maara.GetComponent<Text>().text);
		luku += 1;
		liiku_maara.GetComponent<Text> ().text = "" + luku;
	}

	public void vaihdaSuunta() {
		if (suuntaText.GetComponent<Text>().text == "Vasen") {
			suuntaText.GetComponent<Text> ().text = "Oikea";
		} else {
			suuntaText.GetComponent<Text> ().text = "Vasen";
		}
	}

	public void endRound() {
		Laskin = 0;
		suorita = false;
		waiting = false;
		dirLight.GetComponent<SceneController> ().emptyToiminnot();
		Invoke("kameranVaihto", 0.5f);
	}

	public void restart() {
		deathScreen.gameObject.SetActive (false);
		Laskin = 0;
		suorita = false;
		waiting = false;
		dirLight.GetComponent<SceneController> ().emptyToiminnot();
		kameranVaihto();
		pelaaja.transform.rotation = originalRotation;
		pelaaja.transform.position = originalPosition;
	}

	public void Die() {
		TOIMINTA.Clear ();
		TOIMINTA2.Clear ();
		fps.Vertical = 0;
		suunta = "up";
		suorita = false;
		Liiku = false;
		askeleet = 0;
		Kaanny = false;
		die = true;
		die_z = Random.Range (0, 2) == 0 ? -90 : 90;
	}
		
	public void win() {
		TOIMINTA.Clear ();
		TOIMINTA2.Clear ();
		fps.Vertical = 0;
		suorita = false;
		float soundTime;
		if (mapLoader.mapNum == 1) {
			audio.PlayOneShot (vic1, 1f);
			soundTime = vic1.length;
		} else if (mapLoader.mapNum == 2) {
			audio.PlayOneShot (vic2, 0.7f);
			soundTime = vic2.length;
		} else {
			audio.PlayOneShot (vic3, 1f);
			soundTime = vic3.length;
		}
		Invoke ("sceneLoad", soundTime);
	}

	void sceneLoad() {
		string scene;
		if (mapLoader.mapNum == 3) {
			scene = "Loppu";
		} else {
			scene = "Alkusivu";
		}
		mapLoader.mapWon();
		SceneManager.LoadScene(scene);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Coin") {
			Destroy (col.gameObject);
			audio.PlayOneShot (collect, 0.7f);
			coins++;
			//coins+=4;
			if (coins == coinsToWin) {
				win ();
			}
		}
		if (col.gameObject.tag == "Lava") {
			audio.PlayOneShot (burn, 0.7f);
			Die ();
		}
		if (col.gameObject.tag == "Fire") {
			closeToFire = true;
		}
		if (col.gameObject.tag == "cannon") {
			col.gameObject.transform.parent.gameObject.transform.Find("cannon ball pref").gameObject.SetActive(true);
			col.gameObject.transform.parent.gameObject.transform.Find("cannon ball pref").gameObject.GetComponent<explosion>().explode();
			Die ();
		}
	}
	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Fire") {
			closeToFire = false;
		}
	}

}
