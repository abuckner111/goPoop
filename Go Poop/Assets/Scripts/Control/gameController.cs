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

    private InputActions _input;

    private void Pause()
    {
        Time.timeScale = 0f;
    }

    private void UnPause()
    {
        Time.timeScale = defaultTimeSpeed;
    }

    private void PauseInput(InputAction.CallbackContext ctx)
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


    private void OnEnable()
    {
        _input = new InputActions();
        _input.menu.Enable();

        _input.menu.Pause.started += PauseInput;
    }

    private void OnDisable()
    {
        _input.menu.Pause.started -= PauseInput;

        _input.menu.Disable();
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
