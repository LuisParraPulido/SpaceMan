using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType //lo usamos para identificar los coleccionables en todo el juego y scripts
{
    healthPotion,
    manaPotion,
    money
}




public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.money; // le asigamos un valor por defecto 

    private SpriteRenderer sprite;//variable para acceder al campo visual
    private CircleCollider2D itemCollider;//variable para acceder al collider directamente

    bool hasBeenCollected = false;//indica si fue recolectada

    public int value = 1;//valor del objeto

    GameObject player;//asignamos un varible gameObject para poder obtener el script

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();//inicializamos las variables para que tomen el valor deseado
        itemCollider = GetComponent<CircleCollider2D>();
    }


    private void Start()
    {
        player = GameObject.Find("Player");//asignamos un objeto para la variable
    }



    void Show()
    {
        sprite.enabled = true;
        itemCollider.enabled = true;
        hasBeenCollected = false; 
    }

    void Hide()
    {
        sprite.enabled = false;
        itemCollider.enabled = false;        
    }

    void Collect()
    {
        Hide();
        hasBeenCollected = true;

        switch (this.type)//se usa para los casos de los coleccionables importante el parámetro
        {
            case CollectableType.money:
                //TODO: lógica de la moneda
                GameManager.shareInstance.CollectObject(this);
                GetComponent<AudioSource>().Play();
               
                break;

            case CollectableType.healthPotion:
                //TODO: lógica de la poción de vida
                //GameObject player = GameObject.Find("Player");//obtener un script sin usar un singleton, llamando al gameObject
                player.GetComponent<PlayerController>().CollectHealth(this.value);//usamos asi el script y el método que necesitamos
                break;

            case CollectableType.manaPotion:
                //TODO: lógica de la poción de maná
                //GameObject player = GameObject.Find("Player");
                player.GetComponent<PlayerController>().CollectMana(this.value);
                break;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Collect();

        }
    }

}
