using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;//no darle el nombre de player, es con el fin de que la camera siga al obejtivo que queramos

    public Vector3 offset = new Vector3(0.2f, 0.0f, -10f);// a que distancia debe seguir al personaje

    public float dampingTime = 0.3f;// se le asigna un tiempo de retraso del seguimiento estetica

    public Vector3 velocity = Vector3.zero;// asignamos velocidad 

    private void Awake()
    {
        Application.targetFrameRate = 60;// le asignamos a los fps a los que queremos que corra
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera(true);
    }

    public void ResetCameraPosition()//función para resetear la camara ej: cuando el personaje muera
    {
        MoveCamera(false);


    }

    void MoveCamera(bool smooth)
        //no sirve para seguir lentamente y que cuando se reinicie cambie de posición de manera instantánea
    {
        Vector3 destination = new Vector3(target.position.x - offset.x, offset.y, offset.z);
        if (smooth)
        {
            this.transform.position = Vector3.SmoothDamp( this.transform.position,
                                                          destination,
                                                          ref velocity,
                                                          dampingTime);
            /*SmoothDamp es una barrido suave con cuatro parámetros: Posición actual de la camara,
             objetivo a donde quiero ir, paso por referencia de un vector, tiempo*/
          //ref velocity es el vector que pasamos por referencia a unity para que nos devuelva la velocidad de la camara
        }
        else
        {
            this.transform.position = destination;
            // no se tiene encuenta el barrido 
        }
    }
}
