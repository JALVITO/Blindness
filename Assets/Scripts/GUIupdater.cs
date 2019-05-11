using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIupdater : MonoBehaviour
{
    public Text ammoText;
    // public Image noAmmo;
    // public Image ammo;
    public Text throwText;
	public Text timerText;

    public Game game;
    public Throw thrw;
    public Weapon weapon;
		public GameObject SafeBG;

		public GameObject AlertBG;
		public GameObject Alarm;


	private bool timerOn;
	private int timerValue;
	private string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        game = gameObject.GetComponent<Game>();
        thrw = gameObject.GetComponent<Throw>(); 
        weapon = gameObject.transform.GetChild(0).GetChild(3).gameObject.GetComponent<Weapon>();
				timerOn = false;
				sceneName = SceneManager.GetActiveScene().name;
				if(Equals(sceneName,"L1 Pillars")){
					timerValue = 20;
				}
				else if(Equals(sceneName,"L2 Technical")){
					timerValue = 20;
				}
				else{
					timerValue = 0;
				}
				//Continuar else if para todos los niveles
    }

    // Update is called once per frame
    void Update()
    {
        if(game.hasWeapon){
          ammoText.text= weapon.ammo.ToString();
        } else {
          ammoText.text = "X";
        }

        throwText.text = thrw.items.Count.ToString();
    }

	public void startTimer(){
		if(!timerOn){
			timerOn = true;
			SafeBG.SetActive(false);
			AlertBG.SetActive(true);
			StartCoroutine(countdown(timerValue));
		}
	}

	IEnumerator countdown(int timerVal){
		while(timerVal > 0 && game.triggeredEnemies > 0){
			AlertBG.GetComponent<RectTransform>().sizeDelta = new Vector2((timerVal/20.0F)*300,50);
			timerText.text = "Time until alarm: " + timerVal.ToString();
			yield return new WaitForSeconds(1);
			timerVal--;
		}
		if (game.triggeredEnemies == 0) {
			SafeBG.SetActive(true);
			AlertBG.SetActive(false);
			timerText.text = "Safe";
			timerOn = false;
		}
		else {
			AlertBG.SetActive(false);
			timerText.text = "Mission failed!";
			Alarm.GetComponent<AudioSource>().Play();
			yield return new WaitForSeconds (3);
			if (sceneName == "L1 Pillars")
					SceneManager.LoadScene("GameOverL1");
			else if (sceneName == "L2 Technical")
					SceneManager.LoadScene("GameOverL2");
		}
	}
}
