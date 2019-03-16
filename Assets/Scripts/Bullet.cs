using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private int bulletSpeed;
	private Rigidbody rb;
	private Light light;

    // Start is called before the first frame update
    void Start()
    {
    	bulletSpeed = 5;
        light = gameObject.AddComponent<Light>();
        light.type = LightType.Point;
        rb = gameObject.AddComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * bulletSpeed;
    }
}
