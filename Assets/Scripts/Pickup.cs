using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private GameObject character;
    private Camera cam;
    private Throw inventory;
    // Start is called before the first frame update
    void Start()
    {
        cam = character.GetComponentInChildren<Camera>();
        inventory = character.GetComponent<Throw>();
    }

    // Update is called once per frame
    void Update()
    {
      // Debug.Log("AAAAAAH");
        if(Input.GetKeyDown(KeyCode.E)){
          Debug.Log("E");
          Ray ray = cam.ViewportPointToRay(new  Vector3(0.5F, 0.5F, 0));
          RaycastHit hit;
          if (Physics.Raycast(ray, out hit,3))
              print("I'm looking at " + hit.transform.name);
              if(hit.transform.tag=="Throwable"){
                Debug.Log("throwable");
                inventory.addItem(hit.transform.name);
                Destroy(hit.transform.gameObject);
              }
          else
              print("I'm looking at nothing!");
        }
    }
}
