using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolWithParticles : MonoBehaviour
{
    public GameObject prefab;
    public GameObject particlesPrefab;
    public float delay = 1f;
    public int instantiateGap = 5;

    private void OnEnable()
    {
        InvokeRepeating("GeneratePrefab", delay, instantiateGap);
        InvokeRepeating("GenerateParticle", delay - 0.2f, instantiateGap);
    }

    private void OnDisable()
    {
        CancelInvoke("GeneratePrefab");
        CancelInvoke("GenerateParticle");
    }

    private GameObject GeneratePrefab()
    {
        GameObject patos = Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform);
        return patos;
    }

    private GameObject GenerateParticle()
    {
        GameObject particulas = Instantiate(particlesPrefab, this.transform.position, Quaternion.identity, this.transform);
        Destroy(particulas, 1);
        return particulas;
    }
}
