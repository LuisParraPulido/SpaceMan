using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public Canvas menuCanvas;
    public Canvas gameOver;
    public Canvas game;


    public static MenuManager shareInstance;

    private void Awake()
    {
        if(shareInstance == null)
        {
            shareInstance = this;
        }
        gameOver.enabled = false;
        
    }

    public void ShowMainMenu()
    {
        menuCanvas.enabled = true;
    }
    public void HideMainMenu()
    {
        menuCanvas.enabled = false;
        game.enabled = true;
    }

    public void ShowGameOverMenu()
    {
        gameOver.enabled = true;
        game.enabled = false;
    }

    public void HideGameOverMenu()
    {
        gameOver.enabled = false;
    }


    public void ExitGame()
    {
        /*#if UNITY_EDITOR
          UnityEditor.EditorApplication.isPlaying = false;
        #else
          Aplication.Quit();
        #endif*/
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
