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
    bool allowFire;
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
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(firstCheckpoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("Am Triggered: " + triggered);

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
            Destroy(gameObject);
        }
    }

    public void trigger(Collider col){
        triggered = true;
        agent.SetDestination(col.gameObject.transform.position);
    }

    public IEnumerator fireAtPlayer(){
        allowFire = false;
        Instantiate(bullet, shotSpawn.position,  shotSpawn.rotation);
        yield return new WaitForSeconds(1F);
        allowFire = true;
    }
}
