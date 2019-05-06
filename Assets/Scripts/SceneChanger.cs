using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    void Start(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void changeScene(string scene){
		
		StartCoroutine(sceneDelay(scene));
    }

	IEnumerator sceneDelay(string scene){
		yield return new WaitForSeconds(0.75F);
		SceneManager.LoadScene(scene);
	}
}
