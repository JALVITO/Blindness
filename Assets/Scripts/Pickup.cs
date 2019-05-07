using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pickup : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private Text interactText;
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
        // Debug.Log("E");
        Ray ray = cam.ViewportPointToRay(new  Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,3)){
            // print("I'm looking at " + hit.transform.name);
            if(hit.transform.tag=="Throwable"){
              // Debug.Log("throwable");
              interactText.text = "Press E to Pick Up Item";
              if(Input.GetKeyDown(KeyCode.E)){
                inventory.addItem(hit.transform.name);
                Destroy(hit.transform.gameObject);
                interactText.text = "";
              }
            } else if(hit.transform.tag == "Key"){
              // Debug.Log("grabbing key");
              interactText.text = "Press E to Pick Up Key";
              if(Input.GetKeyDown(KeyCode.E)){
                game.hasKey = true;
                Destroy(hit.transform.gameObject);
                interactText.text = "";
              }
            } else if(hit.transform.tag == "Enemy" && game.triggeredEnemies == 0){
              interactText.text = "Press E to Takedown";
              if(Input.GetKeyDown(KeyCode.E)){
                hit.transform.gameObject.GetComponent<Enemy>().affectHP(-100);
                interactText.text = "";
              }
            }else if (hit.transform.tag == "Door"){
              if(game.hasKey){
                if (game.triggeredEnemies <= 0){
                  interactText.text = "Press E Open Door";
                  if(Input.GetKeyDown(KeyCode.E)){
                    SceneManager.LoadScene("Win");
                  }
                }
                else{
                  interactText.text = "Alerted enemies! Can't escape!";
                }
              } else {
                interactText.text = "Door Locked";
              }
            } else if (hit.transform.tag == "Weapon"){
              interactText.text = "Press E to Pick Up Weapon";
              if(Input.GetKeyDown(KeyCode.E)){
                hit.transform.gameObject.GetComponents<AudioSource>()[1].Play();
                Transform player = character.transform.GetChild(0);
                
                if (character.GetComponent<Game>().hasWeapon){
                  character.GetComponent<Shoot>().ThrowWeapon();
                }
                hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                hit.transform.parent = player;
                hit.transform.localPosition = new Vector3(0.3F,-0.3F,0.7F);
                hit.transform.localRotation = new Quaternion(0,0,0,0);
                if (hit.transform.gameObject.GetComponent<Weapon>().type == 3)
                    hit.transform.Rotate(0,180,0);
                else
                    hit.transform.Rotate(0,90,0);
                        character.GetComponent<Game>().hasWeapon = true;

                GUIupdater gui = gameObject.GetComponent<GUIupdater>();
                gui.weapon = hit.transform.gameObject.GetComponent<Weapon>();
                interactText.text = "";
              }
            }
          } else {
            // print("I'm looking at nothing!");
            interactText.text = "";
        }
    }
}
