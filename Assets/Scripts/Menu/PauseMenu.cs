using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // variables
    public Animator _animator;
    public Canvas hud;

    private bool isGamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    private void Resume()
    {
        _animator.SetBool("isHidden", true);
        hud.enabled = true;
        isGamePaused = false;
    }

    private void Pause()
    {
        _animator.SetBool("isHidden", false);
        hud.enabled = false;
        isGamePaused = true;
    }
}
