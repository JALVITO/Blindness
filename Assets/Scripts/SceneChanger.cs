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
        SceneManager.LoadScene(scene);
    }
}
