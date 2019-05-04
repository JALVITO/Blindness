using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    private int weapon;
    private bool allowFire;
	[SerializeField] private GameObject bullet;
	[SerializeField] private Transform shotSpawn;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        allowFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<Game>().hasWeapon){
            Weapon weapon =  gameObject.transform.GetChild(0).transform.GetChild(3).GetComponent<Weapon>();
            if (weapon.ammo > 0 && allowFire){
                StartCoroutine(fire(weapon));
            }

        }
        if (Input.GetKeyDown("q") && gameObject.GetComponent<Game>().hasWeapon){
            ThrowWeapon();
        }
    }

    public void ThrowWeapon(){
        Transform firstPerson = gameObject.transform.GetChild(0);
        GameObject curGun = firstPerson.transform.GetChild(3).gameObject;
        curGun.GetComponent<Rigidbody>().isKinematic = false;
        curGun.GetComponent<Rigidbody>().useGravity = true;
        curGun.transform.parent = null;
        curGun.GetComponent<Rigidbody>().AddForce(transform.forward * 800);
        gameObject.GetComponent<Game>().hasWeapon = false;
    }

    IEnumerator fire(Weapon weapon){
        allowFire = false;
        player.GetComponent<SoundSource>().addSound(weapon.noise);
        GameObject newbullet = Instantiate(bullet, shotSpawn.position,  shotSpawn.rotation);
        newbullet.GetComponent<MyBullet>().weapon = weapon;
        weapon.ammo--;
        yield return new WaitForSeconds(weapon.rof);
        allowFire = true;
    }
}