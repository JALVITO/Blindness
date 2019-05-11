using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int throwSpeed;
    private AudioSource sound;
    private Rigidbody rb;
    [SerializeField] GameObject itemHit;
    Game game;
    
    void Start()
    {
        sound = gameObject.GetComponents<AudioSource>()[0];
        game = GameObject.FindWithTag("Player").GetComponent<Game>();
        // Debug.Log(gameObject.name);
    }

    void Update(){
      // Debug.Log(rb.velocity);
    }

    void OnCollisionEnter(Collision colInfo) {
      // Debug.Log("COLLISIONED");
      // Debug.Log(colInfo.transform.name);
      // Debug.Log(game.allowLight);
      if(colInfo.transform.tag=="Inanimate" && game.allowLight){
        // Debug.Log("Inan");
        sound.Play();
        Instantiate(itemHit, gameObject.transform.position, gameObject.transform.rotation);
      }
      else if (colInfo.transform.tag=="Enemy" ){
        if(colInfo.gameObject.GetComponent<Enemy>().triggered){
          float percentage = Random.Range(0F,1F);
          if(percentage <= 0.35f){
            colInfo.gameObject.GetComponent<Enemy>().affectHP(-100);
          }
        } else {
          colInfo.gameObject.GetComponent<Enemy>().returnHome();
        }
      }
    }

}
