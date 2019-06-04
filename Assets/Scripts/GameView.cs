using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//usar esta libreria para acceder al UI

public class GameView : MonoBehaviour
{
    public Text coinsText, scoreText, maxScoreText; //declaramos los textos a vincular

    private PlayerController controller;//instanciamos el objeto player

    

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();//llamamos al objeto deseado solo una vez
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.shareInstance.currentGameState == GameState.inGame)//obtenemos el ingame
        {
            int coins = GameManager.shareInstance.collectedObject; //conectamos la intefaz grafica con la función
            float score = controller.GetTravelledDistance();//en el update ya solo llamamos el dato deseado
            float maxScore = PlayerPrefs.GetFloat("maxscore", 0);  //recuperamos el valor maxscore de la clase playerprefs utilizada al morir                    
        
            coinsText.text = coins.ToString(); //cambiamos de int a string con una función de unity para usarlo  
            scoreText.text = "Score: " + score.ToString("f1"); //concatenamos el texto y el valor
            maxScoreText.text = "Max score: " + maxScore.ToString("f1");
            //usamos un parametro de la función toString para cortar los decimales a 1 = f1
        }

        if(GameManager.shareInstance.currentGameState == GameState.gameOver)
        {
            float maxScore = PlayerPrefs.GetFloat("maxscore", 0);
            maxScoreText.text = "Max score: " + maxScore.ToString("f1");
            
        }

    }
}
