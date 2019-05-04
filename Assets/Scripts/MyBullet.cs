using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBullet : MonoBehaviour
{
	[SerializeField] private int bulletSpeed;

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
      light= Instance.GetComponent<Light>();
      light.type = LightType.Point;
      rb = Instance.GetComponent<Rigidbody>();
      rb.velocity = transform.forward * bulletSpeed;
      rend = Instance.GetComponentInChildren<Renderer>();
      // Debug.Log("X: " + transform.forward.x);
      // Debug.Log("Y: " + transform.forward.y);
      // Debug.Log("Z: " + transform.forward.z);
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

    void OnCollisionEnter(Collision col){

      // Debug.Log(col.gameObject.name);

      if (col.gameObject.tag != "Intangible"){
          if (col.gameObject.tag == "Player"){
            col.gameObject.GetComponent<Game>().affectHP(-34);
          }
          if (col.gameObject.tag == "Enemy"){
            col.gameObject.GetComponent<Enemy>().affectHP(-34);
          }

          StartCoroutine(killBullet(0.02F));
      }
    }
}
