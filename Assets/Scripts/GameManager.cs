using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState //enumerado para detectar los estados del juego en todos los scripts
{
    menu,// estados posibles del jugar en el juego
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.menu; // definimos el estado en el cual esta el jugador

    public static GameManager shareInstance;// este es el singleton creados gracias a la palabra static

    private PlayerController controller;// creamos la clase para poder accerder al script PlayerController

    public int collectedObject = 0;



    private void Awake()
    {
        if (shareInstance == null)//colocamos el if en caso de que hallamos duplicado el Game Manager
        {
            shareInstance = this;// se el asigna a este script el shareInstance
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
        // ya que la clase es privada podemos usarla en Start, usamos el controller para obtejer el objeto player
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && currentGameState != GameState.inGame)//cuando se oprima enter desata el startGame
        {
            StartGame();
        }
    }

    public void StartGame()//estados del juego manejados desde el script principal
    {
        SetGameState(GameState.inGame);//se cambia de estado de menú a en juego
       
    }


    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    private void SetGameState(GameState newGameSate)
    {
        if(newGameSate == GameState.menu)
        {
            //TODO: colocar la lógica del menú
            MenuManager.shareInstance.ShowMainMenu();
        }
        else if(newGameSate == GameState.inGame)
        {
            //todo: hay que preparar la escena para jugar
            LevelManager.sharedInstance.RemoveAllLevelBlocks();
            LevelManager.sharedInstance.GenerateInitialBlocks();
            controller.StartGame();
            MenuManager.shareInstance.HideMainMenu();
            MenuManager.shareInstance.HideGameOverMenu();
            

        }
        else if(newGameSate == GameState.gameOver)
        {
            //TODO: preparar el juego para game over
            MenuManager.shareInstance.ShowGameOverMenu();
        }

        this.currentGameState = newGameSate;
    }

    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;//+= incrementa el valor en más 1 cada vez que recolecta un moneda
    }
    

}
