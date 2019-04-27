using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    NavMeshAgent agent;
    Ray ray;
    RaycastHit hit;

    bool triggered;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject firstCheckpoint;
    [SerializeField] private int sightRange;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(firstCheckpoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = transform.position;
        ray.direction = player.transform.position-transform.position;
        Debug.DrawRay(ray.origin, ray.direction*100, Color.green);

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Player" && hit.distance < sightRange && Vector3.Angle(ray.direction,transform.forward) < 70){
            // Debug.Log("HA, gottem");

        }
    }
}
