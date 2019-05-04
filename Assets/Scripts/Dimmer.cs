using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimmer : MonoBehaviour
{
    private Light light;
    [SerializeField] int i;
    [SerializeField] int r;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        light.range = r;
        light.intensity = i;
        StartCoroutine(dim());

    }
    private IEnumerator dim(){
      while(light.intensity > 0){
        light.intensity -= i/60.0F;
        light.range += r/60.0F;
        yield return new WaitForSeconds(0.02F);
      }
    }

    // Update is called once per frame
    void Update()
    {
        if(light.intensity == 0){
          Destroy(gameObject);
        }
    }
}
