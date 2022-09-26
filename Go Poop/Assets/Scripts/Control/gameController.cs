using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class gameController : MonoBehaviour
{

    public GameObject   pauseWindow;
    public bool         paused;
    private bool        pausedD;
    public float        defaultTimeSpeed = 1f;


    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void Pause()
    {
        Time.timeScale = 0f;
    }

    private void UnPause()
    {
        Time.timeScale = defaultTimeSpeed;
    }

    public void PauseInput(InputAction.CallbackContext ctx)
    {
        pauseWindow.SetActive(true);
    }

    private void ControlPause()
    {
        if(paused != pausedD)
        {
            pausedD = paused;
            if(paused)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        paused = pauseWindow.activeSelf;
        pausedD = !paused;
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseWindow.activeSelf != paused)
        {
            paused = pauseWindow.activeSelf;
        }

        ControlPause();
    }
}
