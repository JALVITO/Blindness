using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int throwSpeed;
    private AudioSource sound;
    private Rigidbody rb;
    [SerializeField] GameObject itemHit;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwSpeed);
        sound = gameObject.GetComponent<AudioSource>();
        Debug.Log(gameObject.name);
    }

    void Update(){
      // Debug.Log(rb.velocity);
    }

    void OnCollisionEnter(Collision colInfo) {
      Debug.Log("COLLISIONED");
      Debug.Log(colInfo.transform.name);
      if(colInfo.transform.tag=="Inanimate"){
        Debug.Log("Inan");
        sound.Play();
        Instantiate(itemHit,  
        colInfo.contacts[colInfo.contactCount-1].point, gameObject.transform.rotation);
      }
    }

}
