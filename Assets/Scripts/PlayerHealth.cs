using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Para que RectTransform funciones debemos importar "UnityEngine.UI"
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int totalHealth = 3;
    public int heartsRecoveryTime = 30;
    // Aca ponemos el Health que esta en el HUD Menu
    public RectTransform heartUI;

    // Game Over
    public RectTransform gameOverMenu;
    public GameObject hordes;
    public GameObject player;
    public Transform corazones;

    private int health;
    // Tamaño del corazon en pixeles
    private float heartSize = 16f;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = totalHealth;
        InvokeRepeating("ReactivarCorazones", 0f, heartsRecoveryTime);
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
            GameOver();
        }

        // Aca asignamos el Height y el Width
        // diciendole que el Width va a ser los 16px del corazon * la cantidad de vida del player
        // Y el Height siempre se va a mantener en 16px que es la altura del corazon
        heartUI.sizeDelta = new Vector2(heartSize * health, heartSize);
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

        heartUI.sizeDelta = new Vector2(heartSize * health, heartSize);
        Debug.Log("El jugador se ha curado. Su vida actual es de " + health);
    }

    private IEnumerator VisualFeedback()
    {
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.white;
    }
    public void GameOver()
    {
        gameOverMenu.gameObject.SetActive(true);
        hordes.gameObject.SetActive(false);
        player.gameObject.SetActive(false);

        ReactivarCorazones();

        // Se desactiva a si mismo
        gameObject.SetActive(false);
    }

    public void ReactivarCorazones()
    {
        for (int i = 0; i < corazones.childCount; i++)
        {
            if (!corazones.GetChild(i).gameObject.activeSelf)
            {
                corazones.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        AddHealth(totalHealth);
    }
}
