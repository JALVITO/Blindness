using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int throwSpeed;
    private Rigidbody rb;
    [SerializeField] GameObject itemHit;
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwSpeed);
        Debug.Log(this.name);
    }

    void Update(){
      // Debug.Log(rb.velocity);
    }

    void OnCollisionEnter(Collision colInfo) {
      Debug.Log("COLLISIONED");
      Debug.Log(colInfo.transform.name);
      if(colInfo.transform.tag=="Inanimate"){
        Debug.Log("Inan");
        Instantiate(itemHit,  
        colInfo.contacts[colInfo.contactCount-1].point, gameObject.transform.rotation);
      }
    }

}
