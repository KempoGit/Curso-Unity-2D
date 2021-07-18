using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatoPatrol : MonoBehaviour
{

    public float speed = 1f;

    public LayerMask groundLayer;
    public float wallAware = 0.5f;

    private bool _facingRight;
    private Vector2 _direction;
    private float _horizontalVelocity;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Setea para donde esta mirando el enemigo
        setFacing();

        // Tira un random facing
        float randomNumber = Random.Range(0, 99);
        if(randomNumber >= 50)
        {
            Flip();
        }

        // Hace que el pato parpadee
        StartCoroutine("VisualInvincible");
    }

    // Update is called once per frame
    void Update()
    {
        FlipRaycast();
    }

    private void FixedUpdate()
    {
        // Aplica movimiento
        Move();
    }


    private void setFacing()
    {
        if (transform.localScale.x > 0f)
        {
            _facingRight = true;
            _direction = Vector2.right;
        }
        else if (transform.localScale.x < 0f)
        {
            _facingRight = false;
            _direction = Vector2.left;
        }
    }

    private void FlipRaycast()
    {
        // Creo un raycast desde mi posicion
        // hacia una direccion,
        // seteo la distancia
        // y el tipo de layer que deseo que detectar
        if (Physics2D.Raycast(transform.position, _direction, wallAware, groundLayer))
        {
            Flip();
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = this.transform.localScale.x;
        // Da vuelta el valor del scale X
        localScaleX = localScaleX * -1f;
        // Y lo asigna
        this.transform.localScale = new Vector3(localScaleX, this.transform.localScale.y, this.transform.localScale.z);
        // Asigna la direccion en un vector2
        if (_facingRight)
        {
            _direction = Vector2.right;
        }
        else
        {
            _direction = Vector2.left;
        }
    }

    private void Move()
    {
        // Asigno la velocidad
        _horizontalVelocity = speed;

        // Si mira a la derecha la transformo en negativa
        if (!_facingRight)
        {
            _horizontalVelocity = _horizontalVelocity * -1f;
        }

        // Aplico la velocidad al rigidbody
        _rigidbody.velocity = new Vector2(_horizontalVelocity, _rigidbody.velocity.y);
    }

    private void OnEnable()
    {
        _animator.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _animator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessageUpwards("Invincible");
            Destroy(gameObject);
        }
    }

    private IEnumerator VisualInvincible()
    {
        _renderer.color = Color.yellow;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.white;

        yield return new WaitForSeconds(0.1f);

        StartCoroutine("VisualInvincible");
    }
}
