﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    [SerializeField] private float soundLevel;
    [SerializeField] private float soundDecrease;
    private SphereCollider soundRange;

    // Start is called before the first frame update
    void Start()
    {
        soundRange = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        soundLevel = Mathf.Max(0,soundLevel -= soundDecrease);
        soundRange.radius = soundLevel;
    }

    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Enemy"){
            if (!col.gameObject.GetComponent<Enemy>().dying)
                col.gameObject.GetComponent<Enemy>().trigger(GetComponent<SphereCollider>());
        }
    }

    public void addSound(float newSound){
        soundLevel = Mathf.Max(soundLevel,newSound);
    }
}
