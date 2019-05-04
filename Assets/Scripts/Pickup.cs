using System.Collections;
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
          Debug.Log("E");
          Ray ray = cam.ViewportPointToRay(new  Vector3(0.5F, 0.5F, 0));
          RaycastHit hit;
          if (Physics.Raycast(ray, out hit,3)){
              print("I'm looking at " + hit.transform.name);
              if(hit.transform.tag=="Throwable"){
                Debug.Log("throwable");
                inventory.addItem(hit.transform.name);
                Destroy(hit.transform.gameObject);
              } else if(hit.transform.tag == "Key"){
                Debug.Log("grabbing key");
                game.hasKey = true;
                Destroy(hit.transform.gameObject);
              } else if (hit.transform.tag == "Door"){
                if(game.hasKey){
                  Debug.Log("Change level");
                } else {
                  Debug.Log("Needs key to enter");
                }
              }
          } else {
              print("I'm looking at nothing!");
          }
        }
    }
}
