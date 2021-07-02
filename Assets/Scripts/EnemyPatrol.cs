using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 1f;
    public float minX;
    public float maxX;
    public float waitingTime = 2f;

    private GameObject _target;
    private Animator _animator;
    private Weapon _weapon;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //UpdateTarget();
        //StartCoroutine("PatrolToTarget");

        // Setea la animacion Idle en el primer frame
        _animator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        // Cuando presiona el click activa la animacion de disparo
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetTrigger("Shoot");
        }
    }

    // Esta funcion sirve para crear el target del enemigo y que mire en la direccion del target
    private void UpdateTarget()
    {
        // Si es la primera vez, crea el target a la izquierda
        if(_target == null)
        {
            _target = new GameObject("Target");
            _target.transform.position = new Vector2(minX, this.transform.position.y);
            // Pone este objeto mirando hacia atras con el Vector3
            this.transform.localScale = new Vector3(-1, 1, 1);
            // Y el return para que no ejecute el resto de la funcion
            return;
        }

        // Si esta en la izquierda, crea el target a la derecha
        if(_target.transform.position.x == minX)
        {
            _target.transform.position = new Vector2(maxX, this.transform.position.y);
            // Pone este objeto mirando hacia adelante con el Vector3
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        // Si esta en la derecha, crea el target a la izquierda
        else if(_target.transform.position.x == maxX)
        {
            _target.transform.position = new Vector2(minX, this.transform.position.y);
            // Pone este objeto mirando hacia atras con el Vector3
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Esta funcion sirve para mover el enemigo hasta el target
    private IEnumerator PatrolToTarget()
    {
        // Si la distancia entre el enemigo y el target es mayor a 0.05f, se mueve hacia el
        while(Vector2.Distance(this.transform.position, _target.transform.position) > 0.005f)
        {
            // Actualiza el animator
            _animator.SetBool("Idle", false);

            // Aca setea la direccion a la que tiene que moverse y se translada hacia la direccion por la velocidad.
            Vector2 direction = _target.transform.position - this.transform.position;
            this.transform.Translate(direction.normalized * speed * Time.deltaTime);

            // El 'yield return null' sirve para salir de la funcion y se vuelva a ejecutar
            yield return null;
        }

        // Aca llega al target y lo posicionamos en la posicion X del target
        this.transform.position = new Vector2(_target.transform.position.x, this.transform.position.y);

        // Una vez que llego a su destino es hora de actualizar el objetivo
        UpdateTarget();

        // Actualiza el animator
        _animator.SetBool("Idle", true);

        // Dispara
        _animator.SetTrigger("Shoot");

        // Con esto hacemos que espere el tiempo asignado y vuelva a ejecutar la funcion
        yield return new WaitForSeconds(waitingTime);

        // Una vez que llego a su destino y esperó el tiempo deseado, es hora de empezar a patrullar nuevamente
        StartCoroutine("PatrolToTarget");
    }

    void CanShoot()
    {
        if(_weapon != null)
        {
            // Dispara la Coroutine del arma
            StartCoroutine(_weapon.ShootWithRayCast());
        }
    }
}
