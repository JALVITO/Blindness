using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    private int HP;
    private int triggeredEnemies;
    [SerializeField] private GameObject BGsongs;

    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
        triggeredEnemies = 0;
    }

    void Update(){
        // Debug.Log("Triggered: " + triggeredEnemies);
    }

    public void affectHP(int delta){
        HP += delta;
        if (HP <= 0){
            Debug.Log("You died");
        }
    }

    public void affectTriggeredEnemies(int delta){
        triggeredEnemies += delta;
        AudioSource calm = BGsongs.GetComponents<AudioSource>()[0];
        AudioSource alert = BGsongs.GetComponents<AudioSource>()[1];

        if (triggeredEnemies != 0){
            StartCoroutine(transition(true,calm,alert));
        }
        else{
            StartCoroutine(transition(false,calm,alert));
        }
    }

    public IEnumerator transition(bool spotted, AudioSource calm, AudioSource alert){
        if (spotted){
            while (calm.volume > 0 && spotted){
                calm.volume -= 0.125F;
                alert.volume += 0.125F;
                yield return new WaitForSeconds(0.375F);
                spotted = (triggeredEnemies != 0);
            }
        }
        else{
            while (alert.volume > 0 && !spotted){
                calm.volume += 0.125F;
                alert.volume -= 0.125F;
                yield return new WaitForSeconds(0.375F);
                spotted = (triggeredEnemies != 0);
            }
        }
    }
}
