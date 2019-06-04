using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float runningSpeed = 1.5f;

    public int enemyDamage = 10;

    Rigidbody2D rigidBody;//asignando componente

    public bool facingRight = false;//determirminando dirección de movimiento

    private Vector3 startPosition;//posición inicial


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();//tomando componenete del Unity
        startPosition = this.transform.position;//asignando la posición inicial
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = startPosition;
    }

    private void FixedUpdate()
    {
        float currentRunningSpeed = runningSpeed;

        if (facingRight)
        {
            currentRunningSpeed = runningSpeed;//se mueve a la derecha
            this.transform.eulerAngles = new Vector3(0, 180, 0);//rotamos la imagen en la dirección deseada
        }
        else
        {
            currentRunningSpeed = -runningSpeed;//la velocidad es negativa por la dirección, se mueve a la izquierda
            this.transform.eulerAngles = Vector3.zero;//rotamos otra vez en el sentido original vector zero
        }

        if(GameManager.shareInstance.currentGameState == GameState.inGame)
        {
            rigidBody.velocity = new Vector2(currentRunningSpeed, rigidBody.velocity.y);//la agregamos la velocidad si empieza el juego
        }

        if(GameManager.shareInstance.currentGameState == GameState.gameOver)
        {
            rigidBody.velocity = Vector2.zero;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)//en caso de que el enemigo colisione
    {
        if(collision.tag == "Coin") //no hacemos anda
        {
            return;
        }
        if(collision.tag == "Player")//usamos el mismo método que adicciona vida para restarle pasando un valor negativo
        {
            collision.gameObject.GetComponent<PlayerController>().CollectHealth(-enemyDamage);//pasamos el valor negativo int 
            return;
        }
        //si no choca contra una moneda o el jugador solo podria chocar contra en suelo u otro enemigo por esto

        facingRight = !facingRight;
    }





}
