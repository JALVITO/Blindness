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
			timerValue = 10;
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
			StartCoroutine(countdown(timerValue));
		}
	}

	IEnumerator countdown(int timerVal){
		while(timerVal > 0 && game.triggeredEnemies > 0){
			timerText.text = timerVal.ToString();
			yield return new WaitForSeconds(1);
			timerVal--;
		}
		if (game.triggeredEnemies == 0) {
			timerText.text = "Safe";
			timerOn = false;
		}
		else {
			timerText.text = "Time's up!";
			yield return new WaitForSeconds (1);
			SceneManager.LoadScene ("GameOver");
		}
	}
}
