using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    int HP;
    int step;
    float rof;
    NavMeshAgent agent;
    Ray ray;
    RaycastHit hit;
    private Animator anim;
    private Light footLight;
	private Light hitLight;
    private float m_StepCycle;
    private float m_NextStep;
    private float m_StepInterval;
    bool triggered;
    bool triggeredLast;
    bool allowFire;
    bool hasWeapon;
    bool waiting;
    public bool dying;
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
        step = 0;
        HP = 100;
        allowFire = true;
        hasWeapon = true;
        rof = transform.GetChild(1).gameObject.GetComponent<Weapon>().rof;
        curGun = gameObject.transform.GetChild(1).gameObject;
        FPSController = player.transform.parent.gameObject;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(firstCheckpoint.transform.position);
        anim = transform.GetChild(2).GetComponent<Animator>();
        footLight = transform.GetChild(3).GetComponent<Light>();
        footLight.range = 10;
        footLight.intensity = 0;
		hitLight = transform.GetChild(4).GetComponent<Light>();
		hitLight.range = 10;
		hitLight.intensity = 0;
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle/2f;
        m_StepInterval = 4;
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

		if(hasWeapon && curGun.transform.parent != this.transform){
			curGun = null;
			hasWeapon = false;
            anim.SetBool("unarmed", true);
		}

        if (Physics.Raycast(ray, out hit)){

            //Debug.Log("Player: " + (hit.collider.gameObject.tag == "Player"));
            //Debug.Log("Distance: " + (hit.distance < sightRange));
            //Debug.Log("Angle: " + (Vector3.Angle(ray.direction,transform.forward) < 70));

            if (hit.collider.gameObject.tag == "Player" && hit.distance < sightRange && Vector3.Angle(ray.direction,transform.forward) < 80){
                if (!dying)
                    trigger(hit.collider);
                if (allowFire)
                    StartCoroutine(fireAtPlayer());
            }
            else {
                if (triggered && agent.remainingDistance < 2 && !waiting){
                        triggered = false;
                        StartCoroutine(confusion());
                        anim.SetBool("alerted",false);
                        agent.SetDestination(firstCheckpoint.transform.position);

                    }
            }
        }
    }

    public void FixedUpdate(){
        float speed = triggered ? 5 : 10;
        ProgressStepCycle(speed);
    }

    public void affectHP(int delta){
        HP += delta;
		StartCoroutine(drawHit(3,5));
        if (HP <= 0 && !dying){
            // Debug.Log("Dying...");
            dying = true;
			if(hasWeapon){
				
            	ThrowWeapon();
			}
            // Debug.Log("Triggered: " + triggered);
            if (triggered){
                FPSController.GetComponent<Game>().affectTriggeredEnemies(-1);
            }
            gameObject.GetComponents<AudioSource>()[0].Play();
            StartCoroutine(enemyFall());
        }
    }

    public void trigger(Collider col){
        if (col.gameObject.tag == "Intangible" || col.gameObject.tag == "Player"){
            triggered = true;
            anim.SetBool("alerted",true);
        }
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
        anim.SetTrigger("dead");
        yield return new WaitForSeconds(2.5F);
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

    private void ProgressStepCycle(float speed){
        m_StepCycle += (float)(speed*(triggered ? 1f : 0.7))*Time.fixedDeltaTime;
        // Debug.Log(m_StepCycle);
        if (!(m_StepCycle > m_NextStep))
            return;

        m_NextStep = m_StepCycle + m_StepInterval;
        PlayFootStepAudio();
    }

    private void PlayFootStepAudio(){
        step = (step+1)%2;
        gameObject.GetComponents<AudioSource>()[step+1].Play();
        StartCoroutine(drawSound(1,5));
    }

    public IEnumerator drawSound(int i, int r){
        footLight.intensity = i/2.0F;
    	footLight.range = r;

        while (footLight.intensity > 0){
    		footLight.intensity -= i/60.0F;
    		footLight.range += r/60.0F;
    		yield return new WaitForSeconds(0.002F);
    	}
    }

	public IEnumerator drawHit(int i, int r){
		Debug.Log("Yeaboi");
		hitLight.intensity = i/2.0F;
		hitLight.range = r;

		while(hitLight.intensity > 0){
			hitLight.intensity -= i/60.0F;
			hitLight.range += r/60.0F;
			yield return new WaitForSeconds(0.002F);
		}
	}

    public void returnHome(){
        agent.SetDestination(firstCheckpoint.transform.position);
    }
}
