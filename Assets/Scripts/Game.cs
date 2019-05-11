using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    public bool hasKey;
    private int HP;
    public int triggeredEnemies;
    public bool hasWeapon;
    public bool allowLight;
	public GUIupdater guiupdater;
    [SerializeField] private GameObject BGsongs;
    [SerializeField] private GameObject Hurt;

    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
        triggeredEnemies = 0;
        hasWeapon = true;
        allowLight = false;
        StartCoroutine(heal());
        StartCoroutine(Mask());
		guiupdater = gameObject.GetComponent<GUIupdater>();
    }

    void Update(){
        Debug.Log("Triggered: " + triggeredEnemies);
        // Debug.Log(HP);
        
        Hurt.GetComponent<Image>().color = new Color32(255,255,225,(byte)((1-HP/100.0)*255));

        if (HP > 66){
            gameObject.GetComponents<AudioSource>()[2].enabled = false;
        }
        else if (HP > 33){
            gameObject.GetComponents<AudioSource>()[2].enabled = true;
            gameObject.GetComponents<AudioSource>()[1].enabled = false;
        }
        else{
            gameObject.GetComponents<AudioSource>()[2].enabled = false;
            gameObject.GetComponents<AudioSource>()[1].enabled = true;
        }
    }

    public void affectHP(int delta){
        HP += delta;
        gameObject.transform.GetChild(0).gameObject.GetComponent<CameraShake>().enabled = true;
        if (HP <= 0){
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "L1 Pillars")
                SceneManager.LoadScene("GameOverL1");
            else if (sceneName == "L2 Technical")
                SceneManager.LoadScene("GameOverL2");
        }
    }

    public void affectTriggeredEnemies(int delta){
        triggeredEnemies += delta;
		if(triggeredEnemies < 0){
			triggeredEnemies = 0;
		}
		if(triggeredEnemies > 0){
			guiupdater.startTimer();
		}
        AudioSource calm = BGsongs.GetComponents<AudioSource>()[0];
        AudioSource alert = BGsongs.GetComponents<AudioSource>()[1];

        if (triggeredEnemies > 0){
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

    public IEnumerator heal(){
        while (HP > 0){
            HP = Mathf.Min(HP+4,100);
            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator Mask(){
        yield return new WaitForSeconds(2);
        allowLight = true;
    }
}