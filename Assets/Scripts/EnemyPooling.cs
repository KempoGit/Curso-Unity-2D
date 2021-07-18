using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    public GameObject prefab;
    public float delay = 1f;
    public int instantiateGap = 5;

    // Start is called before the first frame update
    void Start()
    {
        // Comento este Pool porque rompe las animaciones del enemigo
        // Crea el pool de enemigos
        // InitializePool();
        // Invoca repetidamente la funcion "GetEnemyFromPool()"
        // La primera vez se llama con un delay de "1f" segundos
        // Y despues se invoca cada cierto tiempo (instantiateGap)
        // InvokeRepeating("GetEnemyFromPool", 1f, instantiateGap);
    }

    private void OnEnable()
    {
        InvokeRepeating("GenerateEnemy", delay, instantiateGap);
    }

    private void OnDisable()
    {
        CancelInvoke("GenerateEnemy");
    }

    private GameObject GenerateEnemy()
    {
        GameObject enemy = Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform);
        return enemy;
    }

    //private void InitializePool()
    //{
    //    for(int i = 0; i < amount; i++)
    //    {
    //        AddEnemyToPool();
    //    }
    //}

    //private void AddEnemyToPool()
    //{
    //    // Instancia los enemigos con el prefab, la posicion, la postura y dentro del padre asignado
    //    // (para que no queden todos desparramados en el escenario sino en el objeto asignado)
    //    GameObject enemy = Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform);
    //    // Instancia los enemigos desactivados
    //    enemy.SetActive(false);
    //}

    //private GameObject GetEnemyFromPool()
    //{
    //    GameObject enemy = null;

    //    // Busca un enemigo que todavia no este activo y lo asigna a "enemy" si encuentra alguno
    //    for(int i = 0; i < transform.childCount; i++)
    //    {
    //        if(!transform.GetChild(i).gameObject.activeSelf)
    //        {
    //            enemy = transform.GetChild(i).gameObject;
    //            break;
    //        }
    //    }

    //    // Si no encontro un enemigo porque los 10 se encuentran activos, instancia uno nuevo en el pool de enemigos
    //    if (enemy == null)
    //    {
    //        AddEnemyToPool();
    //        // Y lo referencia para utilizarlo
    //        enemy = transform.GetChild(transform.childCount - 1).gameObject;
    //    }

    //    enemy.transform.position = this.transform.position;
    //    // Aca se setea como activo para poder utilizarlo
    //    enemy.SetActive(true);
    //    return enemy;
    //}
}
