using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager sharedInstance;// singleton ya que solo puede haber un level manager

    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    //List es dinamica y permite guardar todo los level blocks para ser usados
    // con el new se crea la lista vacia

    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();
    // en esta esta todos los level Blocks en pantalla para ser usados

    public Transform LevelStartPosition;    

    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance =this; //inicializamos sharedInstance
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLevelBlock()
    {
        int randomIdx = Random.Range(0, allTheLevelBlocks.Count);//se usa para genrar un numero aleatorio del array

        LevelBlock block;

        Vector3 spawnPosition = Vector3.zero;// donde se va ubicar el bloque generado

        if(currentLevelBlocks.Count == 0)
        {
            block = Instantiate(allTheLevelBlocks[0]);// Instantiate es tomar algo del array este caso el bloque 0
            spawnPosition = LevelStartPosition.position;
        }
        else
        {
            block = Instantiate(allTheLevelBlocks[randomIdx]);// aca se toma un bloque aleatorio del array
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position;
            // la spawnPosition se asigna a la salida del ultimo bloque colocado para encadenar
        }

        block.transform.SetParent(this.transform, false); // se asigna que todos los level block son hijos de este script
        // el false es forma de asegurar que ninguna transformación que tengan los padres pasen a los hijos

        Vector3 correction = new Vector3(spawnPosition.x - block.startPoint.position.x,
                                         spawnPosition.y - block.startPoint.position.y,
                                         0 );
        // este vector nos permite corregir la posición de los nuevos bloque que se generan para que se encadenen
        block.transform.position = correction;// se le asiganan la posición corregida
        currentLevelBlocks.Add(block);//ya que se genero el bloque se agrega a los bloques actuales en escena

    }

    public void RemoveLevelBlock()
    {
        LevelBlock oldBlock = currentLevelBlocks[0]; 
        // al ser un array solo necesitamos eliminar la posición 0
        currentLevelBlocks.Remove(oldBlock);// se elimina de la lista
        Destroy(oldBlock.gameObject);// se destruye de la pantalla
    }

    public void RemoveAllLevelBlocks()
    {
        while (currentLevelBlocks.Count > 0)//usamos un ciclo para eliminar todo los bloques
        {
            RemoveLevelBlock();
        }
    }


    public void GenerateInitialBlocks()
    {
        for(int i =0; i < 2; i++)
        {
            AddLevelBlock();
        }
    }
}
