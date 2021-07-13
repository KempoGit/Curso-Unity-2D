using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction;
    public Color initialColor = Color.white;
    public Color finalColor;

    public float livingTime = 3f;

    private SpriteRenderer _renderer;
    private float _startingTime;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update 
    void Start()
    {
        // Guarda el tiempo inicial
        _startingTime = Time.time;

        // Destruye la bala despues del tiempo de vida
        Destroy(this.gameObject, livingTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Cambia el color de la bala con el tiempo
        float _timeSinceStarted = Time.time - _startingTime;
        float _percentageCompleted = _timeSinceStarted / livingTime;

        _renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);
    }

    private void FixedUpdate()
    {
        // Mover el objeto
        Vector2 movement = direction.normalized * speed;
        _rigidbody.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Encontre al player");
            Destroy(this.gameObject);
        }
    }
}
