using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHealth : MonoBehaviour
{
    public int healthRestoration = 1;

    private void Awake()
    {
        GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("AddHealth", healthRestoration);

            Destroy(gameObject);
        }
    }
}
