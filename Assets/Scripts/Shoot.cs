﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

	[SerializeField] private GameObject bullet;
	[SerializeField] private Transform shotSpawn;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Instantiate(bullet, shotSpawn.position,  shotSpawn.rotation);
        }
    }
}
