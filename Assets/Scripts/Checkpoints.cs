using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private GameObject nextCheckpoint;

    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Enemy")
            col.gameObject.GetComponent<NavMeshAgent>().SetDestination(nextCheckpoint.transform.position);
    }
}
