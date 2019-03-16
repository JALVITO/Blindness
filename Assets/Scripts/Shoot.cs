using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

	private GameObject bullet;
	private GameObject shotSpawn;

    // Start is called before the first frame update
    void Start()
    {
        bullet.AddComponent<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            GameObject bullet = Instantiate(bullet, shotSpawn.Transform.position);
    }
}
