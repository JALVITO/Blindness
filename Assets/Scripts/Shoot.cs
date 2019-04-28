using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

	[SerializeField] private GameObject bullet;
	[SerializeField] private Transform shotSpawn;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            player.GetComponent<SoundSource>().addSound(10);
            Instantiate(bullet, shotSpawn.position,  shotSpawn.rotation);
        }
    }
}
