﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{	

	private Light footLight;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        footLight = GetComponent<Light>();
        footLight.range = 10;
        footLight.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator drawSound(int i, int r, bool isWalking){

        if (isWalking){
            player.GetComponent<SoundSource>().addSound(3);
        }
        else{
            player.GetComponent<SoundSource>().addSound(6);
        }

    	footLight.intensity = i;
    	footLight.range = r;

    	while (footLight.intensity > 0){
        if(Time.timeScale != 0.0f){
          footLight.intensity -= i/60.0F;
          footLight.range += r/60.0F;

        }
          yield return new WaitForSeconds(0.02F);
    	}

    }
}
