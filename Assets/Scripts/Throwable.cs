using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int throwSpeed;
    private Rigidbody rb;
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwSpeed);
        Debug.Log(this.name);
    }

    void Update(){
      Debug.Log(rb.velocity);
    }

}
