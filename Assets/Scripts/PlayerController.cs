using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.5f;
    public float jumpForce = 2.5f;
    public float longIdleTime= 5f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _renderer;

    private float _longIdleTimer;
    private int _jumpCount;
    private int _jumpMax = 2;
    private float _jumpStartingTime;

    private Vector2 _movement;
    private bool _facingRight = true;
    private bool _isGrounded;
    private bool _isAttacking;

    private float _initialPositionX;
    private float _initialPositionY;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();

        _initialPositionX = this.transform.position.x;
        _initialPositionY = this.transform.position.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Se mueve o gira si NO esta atacando
        if (_isAttacking == false)
        {
            // Setea hacia donde tiene que moverse
            // El GetAxisRaw redondea el valor del axis para sacarle la gravedad y que reacciones instantaneamente al input
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            _movement = new Vector2(horizontalInput, 0f);

            // Da vuelta el personaje
            if(horizontalInput < 0f && _facingRight == true)
            {
                Flip();
            } else if (horizontalInput > 0f && _facingRight == false)
            {
                Flip();
            }
        }

        // Esta en el piso?
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Esta saltando? Salto 1 o menos veces?
        if(Input.GetButtonDown("Jump") && (_isGrounded == true || _jumpCount < _jumpMax) && _isAttacking == false)
        {
            // Agrega un salto al contador
            _jumpCount = _jumpCount + 1;
            _jumpStartingTime = Time.time;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        } else if(_isGrounded == true && (Time.time - _jumpStartingTime) > 0.1f)
        {
            // Seteo un tiempo desde que presiona el salto, para que al player no se le reinicie el salto antes de dejar el suelo
            // Si esta en tierra reinicia el contador
            _jumpCount = 0;
        }

        // Quiere pegar
        if(Input.GetButtonDown("Fire1") && _isGrounded == true && _isAttacking == false)
        {
            _movement = Vector2.zero;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger("Attack");
        }
    }

    // Este se llama despues de update
    void FixedUpdate()
    {
        // Aplica el movimiento mientras NO este atacando
        if (_isAttacking == false)
        {
            // Calcula el movimiento con el axis y la velocidad
            float horizontalVelocity = _movement.normalized.x * speed;
            // Aplica el movimiento
            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
        }
    }

    // Y este es el ultimo update donde se suelen poner las animaciones
    void LateUpdate()
    {
        // Seteamos la animacion Idle si el personaje no tiene movimiento alguno
        _animator.SetBool("Idle", _movement == Vector2.zero);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);

        // Checkeamos si la animacion de ataque esta en curso
        if(_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            _isAttacking = true;
        } else
        {
            _isAttacking = false;
        }

        // Long Idle
        if(_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            // Le asignamos tiempo al timer si esta en Idle
            _longIdleTimer += Time.deltaTime;

            // Si el timer supera el tiempo del idle, entra al long idle
            if(_longIdleTimer >= longIdleTime)
            {
                _animator.SetTrigger("LongIdle");
            }

        } else
        {
            // Si no esta en Idle reseteamos el timer
            _longIdleTimer = 0f;
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
    }

    private void OnEnable()
    {
        _renderer.color = Color.white;
        this.transform.position = new Vector2(_initialPositionX, _initialPositionY);
    }
}
