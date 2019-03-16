using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{	

	private Light footLight;

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

    public IEnumerator drawSound(int i, int r){

    	footLight.intensity = i;
    	footLight.range = r;

    	while (footLight.intensity > 0){
    		footLight.intensity -= i/60.0F;
    		footLight.range += r/60.0F;
    		yield return new WaitForSeconds(0.02F);
    	}

    }
}
