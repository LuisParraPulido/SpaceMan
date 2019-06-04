using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Player")//importante el tag
        {
            LevelManager.sharedInstance.AddLevelBlock();//gracias al sigleton podemos llamar al LevelManager
            LevelManager.sharedInstance.RemoveLevelBlock();
            //mientras se destruye un bloque se crea otro
        }

    }
}
