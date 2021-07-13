using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int totalHealth = 3;

    private int health;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = totalHealth;
    }


    public void AddDamage(int amount)
    {
        health = health - amount;

        // Feedback visual
        StartCoroutine("VisualFeedback");

        // Game Over
        if(health <= 0)
        {
            health = 0;
        }

        Debug.Log("El jugador fue golpeado. Su vida actual es de " + health);
    }

    public void AddHealth(int amount)
    {
        health = health + amount;

        // El jugador tiene la vida maxima
        if (health >= totalHealth)
        {
            health = totalHealth;
        }

        Debug.Log("El jugador se ha curado. Su vida actual es de " + health);
    }

    private IEnumerator VisualFeedback()
    {
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.white;
    }
}
