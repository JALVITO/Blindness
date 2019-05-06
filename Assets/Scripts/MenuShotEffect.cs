using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShotEffect : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    Weapon weapon;
    bool allowFire;

    // Start is called before the first frame update
    void Start()
    {
        allowFire = true;
        weapon = transform.GetChild(0).gameObject.GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (allowFire)
            StartCoroutine(fireMenu());
    }

    public IEnumerator fireMenu(){
        allowFire = false;
        GameObject newbullet = Instantiate(bullet, gameObject.transform.position,  gameObject.transform.rotation);
        newbullet.GetComponent<MyBullet>().weapon = weapon;
        yield return new WaitForSeconds(weapon.rof);
        allowFire = true;
    }
}
