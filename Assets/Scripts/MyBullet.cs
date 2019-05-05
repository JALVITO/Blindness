using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyBullet : MonoBehaviour
{
	public int bulletSpeed;

  public Weapon weapon;

	private Rigidbody rb;
	private Light light;
  private Renderer rend;

  public static MyBullet Instance;
     
     void Awake(){
        Instance = this;
        
     }
    // Start is called before the first frame update
    void Start()
    {
      // Debug.Log("ur mom gety");
      int range = weapon.range;
      light= Instance.GetComponent<Light>();
      light.type = LightType.Point;
      rb = Instance.GetComponent<Rigidbody>();
      rb.velocity = transform.forward * weapon.speed;
      rend = Instance.GetComponentInChildren<Renderer>();
      // Debug.Log("X: " + transform.forward.x);
      // Debug.Log("Y: " + transform.forward.y);
      // Debug.Log("Z: " + transform.forward.z);
      // Debug.Log(range);
		if(weapon.type == 1){
			gameObject.GetComponents<AudioSource>()[0].Play();
		} 
		if(weapon.type == 2){
			gameObject.GetComponents<AudioSource>()[1].Play();
		}
		else{
			gameObject.GetComponents<AudioSource>()[2].Play();
		}
      StartCoroutine(despawnBullet(range));
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public IEnumerator killBullet(float rate){
      
      rend.enabled = false;
      rb.velocity = new Vector3(0,0,0);
    	while (light.intensity > 0){
    		light.intensity -= rate;
    		yield return new WaitForSeconds(0.02F);
    	}

      Destroy(gameObject);

    }

    public IEnumerator despawnBullet(int range){
      yield return new WaitForSeconds(range);
      StartCoroutine(killBullet(0.1F));
    }

    void OnCollisionEnter(Collision col){

      // Debug.Log(col.gameObject.name);
      if (col.gameObject.tag == "Inanimate"){
        StartCoroutine(killBullet(0.02F));
      } 
      else if (col.gameObject.tag != "Intangible"){
        Destroy(gameObject);
        int damage = weapon.damage; 
        if (col.gameObject.tag == "Player"){
          col.gameObject.GetComponent<Game>().affectHP(-damage);
        }
        else if (col.gameObject.tag == "Enemy"){
          col.gameObject.GetComponent<Enemy>().affectHP(-damage);
        }
      }
    }
}
