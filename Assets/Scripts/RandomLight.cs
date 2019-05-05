using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLight : MonoBehaviour
{
	private Light light;
	[SerializeField] private float intensity;
    // Start is called before the first frame update
    void Start()
    {
        light = gameObject.GetComponent<Light>();
        light.intensity = intensity;
        StartCoroutine(randomLight());
    }

    // Update is called once per frame
    void Update()
    {
       if(light.range>0){
       	light.range-=0.4F;
       }
    }
    private IEnumerator randomLight(){
    	while(true){
    		float newRange = Random.Range(0.0F,25.0F);
    		Debug.Log(newRange);
    		light.range = Mathf.Max(light.range, newRange);
    		yield return new WaitForSeconds(0.04F);
    	}
    }
}
