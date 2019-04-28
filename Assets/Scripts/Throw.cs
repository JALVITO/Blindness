using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    // Start is called before the first frame update
    public Stack items = new Stack();
    [SerializeField] private GameObject item;
	  [SerializeField] private Transform shotSpawn;
    void Start()
    {
      items.Push("A");
      items.Push("A");
      items.Push("A");
      items.Push("A");
      items.Push("A");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && items.Count >0 ) {
          items.Pop();
          Debug.Log("THROWN");
          Instantiate(item, shotSpawn.position,  shotSpawn.rotation);
        }
    }
}
