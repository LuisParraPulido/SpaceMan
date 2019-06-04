using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables del movimiento del personaje
    public float jumpForce = 5.5f;
    public float runningSpeed = 2f;
    public float jumpRaycastDistance = 1.5f;//poder agregar el bug del salto atascado

    Rigidbody2D playerRigidbody;
    Animator animator;
    Vector3 startPositon;


    private const string STATE_ALIVE = "isAlive";//las constantes es mejor escribirlas en mayúscula
    private const string STATE_ON_THE_GROUND = "isOnTheGround";

    [SerializeField]//nos permite ver las variables privadas en el editor de unity
    private int healthPoints, manaPoints;

    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15,
        MAX_HEALTH = 200, MAX_MANA = 30,
        MIN_HEALTH = 10, MIN_MANA = 0;//rangos limites para las variables

    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.5f;


    //la mayuscula representa las constantes

    public LayerMask groudMask;
    //layer que se le asigna a los objetos que pertenecen al suelo para identificarlos


    //awake en lo primero que pasa antes de cargar el juego
    void Awake()
    {

        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //se le asigna a esta variable la caracteristica que queremos modificar en el codigo
    }

    // Start is called before the first frame update
    void Start()
    {
             
        startPositon = this.transform.position;
   

    }

    public void StartGame()
    {
        
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);

        healthPoints = INITIAL_HEALTH;//inicializamos las varibles de mana y vida
        manaPoints = INITIAL_MANA;

        

        Invoke("RestartPosition", 0.2f);//retrasa la entrada de un metodo 
    }



    void RestartPosition()
    {
        this.transform.position = startPositon;// guardamos la posicion inicial
        this.playerRigidbody.velocity = Vector2.zero;// ponemos la velocidad en cero para que no siga cayendo
        GameObject maincamera = GameObject.Find("Main Camera"); //creamos una variable para guardar la camara por nombre
        maincamera.GetComponent<CameraFollow>().ResetCameraPosition();//invocamos el método de camarafollow
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump(false);
        }

        if (Input.GetButtonDown("Superjump"))
        {
            Jump(true);
        }         
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        Debug.DrawRay(this.transform.position, Vector2.down * 2f, Color.red);
    }

    void FixedUpdate() // es un update que se actualiza cada vez que se indique no por fps
    {
        if (GameManager.shareInstance.currentGameState == GameState.inGame)// si estamos en la partida
        {
            if (playerRigidbody.velocity.x < runningSpeed)// agragegando velocidad sobre el eje x
            {
                playerRigidbody.velocity = new Vector2(runningSpeed, playerRigidbody.velocity.y);
            }
        }
        else//si no estamos dentro de la partida
        {
            playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
        }
    }

    void Jump(bool superjump)//variable boleano para saber si es un super salto
    {

        float jumpForceFactor = jumpForce;

        if (superjump && manaPoints >= SUPERJUMP_COST)
        {
            manaPoints -= SUPERJUMP_COST;//disminuimos el mana si se hace super salto
            jumpForceFactor *= SUPERJUMP_FORCE;//multiplicamos la fuerza de salto *=
        }

        if (GameManager.shareInstance.currentGameState == GameState.inGame)
        {

            if (IsTouchingTheGround())
            {
                playerRigidbody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
                //vector2 debido a que es en 2d
                GetComponent<AudioSource>().Play();
            }
            else
            {
                
            }
        }   
    }


    //Nos indica si el personaje esta tocando el suelo
    bool IsTouchingTheGround()
    {
        //RAYCAST es un rayo invisible para medir la distancia entre dos objetos. personaje-suelo
        if (Physics2D.Raycast(this.transform.position, Vector2.down, jumpRaycastDistance, groudMask))
        {

            //animator.enabled = true; //reanudar animación
            //GameManager.shareInstance.currentGameState = GameState.inGame; sirve para modificar el estado del GM
            return true;

        }
        else
        {
            //TODO: programar lógica de no contacto
            //animator.enabled = false; //pausar animación
            return false;
        }

    }


    public void Die()
    {
        float travelledDistance = GetTravelledDistance();//almacenamos la distancia actual recorrida
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore", 0f);//PlayersPrefs sirve para persistir datos en ficheros
        if(travelledDistance > previousMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance);//cambiamos la configuración a la mejor puntuación
        }

        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.shareInstance.GameOver();

    }

    public void CollectHealth(int points)//pasamos como parametro lo points
    {
        this.healthPoints += points;
        if(this.healthPoints >= MAX_HEALTH)//usamos el limite para no tener vida infinita
        {
            this.healthPoints = MAX_HEALTH;
        }
        if(this.healthPoints <= 0)
        {
            Die();
        }
    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;
        if(this.manaPoints>= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealth()//acceder al punto de vida para poder pintarlo en el interfaz
    {
        return healthPoints;
    }

    public int GetMana()
    {
        return manaPoints;
    }

    public float GetTravelledDistance()
    {
        return this.transform.position.x - startPositon.x;  //distancia entre dos puntos para el puntaje             
    }
}
