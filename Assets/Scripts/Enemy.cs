using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    int HP;
    float rof;
    NavMeshAgent agent;
    Ray ray;
    RaycastHit hit;

    bool triggered;
    bool triggeredLast;
    bool allowFire;
    bool hasWeapon;
    bool waiting;
    bool dying;
    GameObject FPSController;
    GameObject curGun;
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
        hasWeapon = true;
        rof = transform.GetChild(1).gameObject.GetComponent<Weapon>().rof;
        curGun = gameObject.transform.GetChild(1).gameObject;
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

		if (curGun != null && curGun.transform.parent != this.transform){
			Debug.Log("It happens.");
			curGun = null;
			hasWeapon = false;
		}

        if (Physics.Raycast(ray, out hit)){

            //Debug.Log("Player: " + (hit.collider.gameObject.tag == "Player"));
            //Debug.Log("Distance: " + (hit.distance < sightRange));
            //Debug.Log("Angle: " + (Vector3.Angle(ray.direction,transform.forward) < 70));

            if (hit.collider.gameObject.tag == "Player" && hit.distance < sightRange && Vector3.Angle(ray.direction,transform.forward) < 80){
                trigger(hit.collider);
                if (allowFire)
                    StartCoroutine(fireAtPlayer());
            }
            else {
                if (triggered && agent.remainingDistance < 2 && !waiting){
                        triggered = false;
                        StartCoroutine(confusion());
                        agent.SetDestination(firstCheckpoint.transform.position);
                    }
            }
        }
    }

    public void affectHP(int delta){
        HP += delta;
        if (HP <= 0 && !dying){
            // Debug.Log("Enemy dead");
            dying = true;
			if(hasWeapon){
				
            	ThrowWeapon();
			}
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
        if (hasWeapon){
            GameObject newbullet = Instantiate(bullet, shotSpawn.position,  shotSpawn.rotation);
            newbullet.GetComponent<MyBullet>().weapon = transform.GetChild(1).gameObject.GetComponent<Weapon>();
            newbullet.GetComponent<MyBullet>().fromPlayer = false;
        }
        
        yield return new WaitForSeconds( Random.Range(rof,rof*2) );
        allowFire = true;
    }

    public IEnumerator enemyFall(){
        for (int x = 0; x < 90; x+=3){
            gameObject.transform.Rotate(-x,0,0);
            yield return new WaitForSeconds(0.001F);
        }
        Destroy(gameObject);
    }

    IEnumerator confusion(){
        waiting = true;
        yield return new WaitForSeconds(3);
        waiting = false;
    }

    private void ThrowWeapon(){
        curGun.GetComponent<Rigidbody>().isKinematic = false;
        curGun.GetComponent<Rigidbody>().useGravity = true;
        curGun.transform.parent = null;
        curGun.GetComponent<Rigidbody>().AddForce(curGun.transform.forward * 800);
        allowFire = false;
        hasWeapon = false;
    }
}
