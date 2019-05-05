﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    // Start is called before the first frame update
    public Stack items = new Stack();
    [SerializeField] private GameObject[] throwables;
	  [SerializeField] private Transform shotSpawn;
    private GameObject clone;
    private string myName;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && items.Count >0 ) {
          myName = (string)items.Pop();
          // Debug.Log(myName);
          // Debug.Log("THROWN");
          int index= -1;
          switch (myName)
          {
            case "Mug":
              index = 0;
            break;
            case "Clipboard":
              index = 1;
            break; 
          }
          if(index != -1){
            clone = Instantiate(throwables[index], shotSpawn.position,  shotSpawn.rotation);
            clone.name = myName;
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * clone.GetComponent<Throwable>().throwSpeed);
          }
        }
    }
    public void addItem(string name){
      items.Push(name);
    }
}
