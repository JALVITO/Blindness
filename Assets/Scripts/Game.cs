using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    private int HP;

    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
    }

    public void affectHP(int delta){
        HP += delta;
        if (HP <= 0){
            Debug.Log("You died");
        }
    }
}
