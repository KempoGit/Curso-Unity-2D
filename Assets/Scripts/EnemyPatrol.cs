using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 1f;
    public float minX;
    public float maxX;
    public float waitingTime = 2f;

    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public float wallAware = 0.5f;
    public float playerAware = 3f;
    public float aimingTime= 0.5f;
    public float shootingTime= 1.5f;

    private bool _facingRight;
    private Vector2 _direction;
    private float _horizontalVelocity;
    private bool _isAttacking;

    private GameObject _target;
    private Animator _animator;
    private Weapon _weapon;
    private Rigidbody2D _rigidbody;
    private AudioSource _audio;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Setea para donde esta mirando el enemigo
        setFacing();
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast para ver en que momento gira
        if(!_isAttacking)
        {
            FlipRaycast();
        }

        // Raycast para saber cuando disparar
        if(!_isAttacking)
        {
            ShootRaycast();
        }
    }

    private void FixedUpdate()
    {
        // Aplica movimiento
        Move();
    }

    private void LateUpdate()
    {
        _animator.SetBool("Idle", _rigidbody.velocity == Vector2.zero);
    }

    private void setFacing()
    {
        if(transform.localScale.x > 0f)
        {
            _facingRight = true;
            _direction = Vector2.right;
        } else if (transform.localScale.x < 0f)
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
        if(Physics2D.Raycast(transform.position, _direction, wallAware, groundLayer))
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
        if(_facingRight)
        {
            _direction = Vector2.right;
        } else
        {
            _direction = Vector2.left;
        }
    }

    private void Move()
    {
        // Asigno la velocidad
        _horizontalVelocity = speed;

        // Si mira a la derecha la transformo en negativa
        if(!_facingRight)
        {
            _horizontalVelocity = _horizontalVelocity * -1f;
        }

        if(_isAttacking)
        {
            _horizontalVelocity = 0f;
        }

        // Aplico la velocidad al rigidbody
        _rigidbody.velocity = new Vector2(_horizontalVelocity, _rigidbody.velocity.y);
    }

    private void ShootRaycast()
    {
        if (Physics2D.Raycast(transform.position, _direction, playerAware, playerLayer))
        {
            StartCoroutine("AimAndShoot");
        }
    }

    private IEnumerator AimAndShoot()
    {
        _isAttacking = true;

        yield return new WaitForSeconds(aimingTime);

        _animator.SetTrigger("Shoot");

        yield return new WaitForSeconds(shootingTime);

        _isAttacking = false;
    }

    void CanShoot()
    {
        if(_weapon != null)
        {
            _weapon.Shoot();
            // Con esta funcion le damos play al audio del enemigo
            //_audio.Play();
        }
    }

    private void OnEnable()
    {
        _isAttacking = false;
        _animator.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        StopCoroutine("AimAndShoot");
        _isAttacking = false;
        _animator.gameObject.SetActive(false);
    }
}
