using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIupdater : MonoBehaviour
{
    public Text ammoText;
    // public Image noAmmo;
    // public Image ammo;
    public Text throwText;

    public Game game;
    public Throw thrw;
    public Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        game = gameObject.GetComponent<Game>();
        thrw = gameObject.GetComponent<Throw>(); 
        weapon = gameObject.transform.GetChild(0).GetChild(3).gameObject.GetComponent<Weapon>();
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
}
