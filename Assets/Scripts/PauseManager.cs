using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {
    
    Canvas canvas;
    
    void Start()
    {
        Debug.Log("Pause script");
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
          Debug.Log("Pause script run");
            Pause();
        }
    }
    public void Pause()
    {
        if(canvas.enabled){
          Time.timeScale = 1.0f;
          Cursor.visible = false;
          Cursor.lockState = CursorLockMode.Locked;
          canvas.enabled = false;
        } else {
          Debug.Log(Time.timeScale);
          Time.timeScale = 0.0f;
          Cursor.visible = true;
          Cursor.lockState = CursorLockMode.None;
          canvas.enabled = true;
        }
    }
    
    public void Quit()
    {
        #if UNITY_EDITOR 
        EditorApplication.isPlaying = false;
        #else 
        Application.Quit();
        #endif
    }
}