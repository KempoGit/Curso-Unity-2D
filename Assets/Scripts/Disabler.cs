using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disabler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) {
            collision.SendMessageUpwards("GameOver");
        } else
        {
            collision.gameObject.SetActive(false);
        }
    }
}
