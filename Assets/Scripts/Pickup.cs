﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private GameObject character;
    private Game game;
    private Camera cam;
    private Throw inventory;
    // Start is called before the first frame update
    void Start()
    {
        cam =   character.GetComponentInChildren<Camera>();
        inventory = character.GetComponent<Throw>();
        game = character.GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
      // Debug.Log("AAAAAAH");
        if(Input.GetKeyDown(KeyCode.E)){
          // Debug.Log("E");
          Ray ray = cam.ViewportPointToRay(new  Vector3(0.5F, 0.5F, 0));
          RaycastHit hit;
          if (Physics.Raycast(ray, out hit,3)){
              // print("I'm looking at " + hit.transform.name);
              if(hit.transform.tag=="Throwable"){
                // Debug.Log("throwable");
                inventory.addItem(hit.transform.name);
                Destroy(hit.transform.gameObject);
              } else if(hit.transform.tag == "Key"){
                // Debug.Log("grabbing key");
                game.hasKey = true;
                Destroy(hit.transform.gameObject);
              } else if (hit.transform.tag == "Door"){
                if(game.hasKey && game.triggeredEnemies == 0){
                  Debug.Log("Change level");
                } else {
                  // Debug.Log("Needs key to enter");
                }
              } else if (hit.transform.tag == "Weapon"){
                Transform player = character.transform.GetChild(0);
                
                if (character.GetComponent<Game>().hasWeapon){
                  character.GetComponent<Shoot>().ThrowWeapon();
                }
                hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                hit.transform.parent = player;
                hit.transform.localPosition = new Vector3(0.3F,-0.3F,0.7F);
                hit.transform.localRotation = new Quaternion(0,0,0,0);
                hit.transform.Rotate(0,90,0);
                character.GetComponent<Game>().hasWeapon = true;

                GUIupdater gui = gameObject.GetComponent<GUIupdater>();
                gui.weapon = hit.transform.gameObject.GetComponent<Weapon>();
              }
           } else {
              // print("I'm looking at nothing!");
          }
        }
    }
}
