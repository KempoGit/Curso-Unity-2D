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
        GameObject enemy = Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform);
        return enemy;
    }

    private GameObject GenerateParticle()
    {
        GameObject enemy = Instantiate(particlesPrefab, this.transform.position, Quaternion.identity, this.transform);
        return enemy;
    }
}
