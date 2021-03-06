using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHealth : MonoBehaviour
{
    public int healthRestoration = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("AddHealth", healthRestoration);

            this.gameObject.SetActive(false);
        }
    }
}
