using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    int HP;
    NavMeshAgent agent;
    Ray ray;
    RaycastHit hit;

    bool triggered;
    bool triggeredLast;
    bool allowFire;
    GameObject FPSController;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject firstCheckpoint;
    [SerializeField] private int sightRange;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotSpawn;


    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
        allowFire = true;
        FPSController = player.transform.parent.gameObject;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(firstCheckpoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (triggeredLast && !triggered)
            FPSController.GetComponent<Game>().affectTriggeredEnemies(-1);
        else if (!triggeredLast && triggered)
            FPSController.GetComponent<Game>().affectTriggeredEnemies(1);

        triggeredLast = triggered;
        // Debug.Log("Am Triggered: " + triggered);

        ray.origin = transform.position;
        ray.direction = player.transform.position-transform.position;
        Debug.DrawRay(ray.origin, ray.direction*100, Color.green);

        if (Physics.Raycast(ray, out hit)){

            //Debug.Log("Player: " + (hit.collider.gameObject.tag == "Player"));
            //Debug.Log("Distance: " + (hit.distance < sightRange));
            //Debug.Log("Angle: " + (Vector3.Angle(ray.direction,transform.forward) < 70));

            if (hit.collider.gameObject.tag == "Player" && hit.distance < sightRange && Vector3.Angle(ray.direction,transform.forward) < 70){
                trigger(hit.collider);
                if (allowFire)
                    StartCoroutine(fireAtPlayer());
            }
            else {
                if (triggered && agent.remainingDistance < 2){
                        triggered = false;
                        agent.SetDestination(firstCheckpoint.transform.position);
                    }
            }
        }
    }

    public void affectHP(int delta){
        HP += delta;
        if (HP <= 0){
            Debug.Log("Enemy dead");
            ThrowWeapon();
            FPSController.GetComponent<Game>().affectTriggeredEnemies(-1);
            gameObject.GetComponent<AudioSource>().Play();
            StartCoroutine(enemyFall());
        }
    }

    public void trigger(Collider col){
        triggered = true;
        agent.SetDestination(col.gameObject.transform.position);
    }

    public IEnumerator fireAtPlayer(){
        allowFire = false;
        GameObject newbullet = Instantiate(bullet, shotSpawn.position,  shotSpawn.rotation);
        newbullet.GetComponent<MyBullet>().weapon = transform.GetChild(1).gameObject.GetComponent<Weapon>();
        
        yield return new WaitForSeconds(transform.GetChild(1).gameObject.GetComponent<Weapon>().rof);
        allowFire = true;
    }

    public IEnumerator enemyFall(){
        for (int x = 0; x < 90; x+=3){
            gameObject.transform.Rotate(-x,0,0);
            yield return new WaitForSeconds(0.001F);
        }
        Destroy(gameObject);
    }

    private void ThrowWeapon(){
        GameObject curGun = gameObject.transform.GetChild(1).gameObject;
        curGun.GetComponent<Rigidbody>().isKinematic = false;
        curGun.GetComponent<Rigidbody>().useGravity = true;
        curGun.transform.parent = null;
        curGun.GetComponent<Rigidbody>().AddForce(curGun.transform.forward * 800);
    }
}
