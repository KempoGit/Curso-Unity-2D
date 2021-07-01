using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject bulletPrefab;
    private Transform _firePoint;
    public GameObject shooter;

    public GameObject explosionEffect;
    public LineRenderer lineRenderer;

    void Awake()
    {
        // Esto busca el FirePoint y lo asigna a una variable
        _firePoint = this.transform.Find("FirePoint");
    }

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("Shoot", 1f);
        //Invoke("Shoot", 2f);
        //Invoke("Shoot", 3f);
        //Invoke("Shoot", 4f);
        //Invoke("Shoot", 5f);
        //Invoke("Shoot", 6f);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetButtonDown("Fire1"))
        //{
        //    Shoot();
        //}
    }

    public void Shoot()
    {
        if(bulletPrefab != null && _firePoint != null && shooter != null)
        {
            GameObject myBullet = Instantiate(bulletPrefab, _firePoint.position, Quaternion.identity) as GameObject;
            Bullet bulletComponent = myBullet.GetComponent<Bullet>();
            if(shooter.transform.localScale.x < 0f)
            {
                bulletComponent.direction = Vector2.left;
            } else
            {
                bulletComponent.direction = Vector2.right;
            }
        }
    }

    public IEnumerator ShootWithRayCast()
    {
        if(explosionEffect != null && lineRenderer != null)
        {
            // Dice de donde sale y Raycast y en que direccion
            RaycastHit2D hitInfo = Physics2D.Raycast(_firePoint.position, _firePoint.right);

            if(hitInfo)
            {
                // Codigo de ejemplo
                //(hitInfo.collider.tag == "Player") {
                //    Transform player = hitInfo.transform;
                //    player.GetComponent<PlayerHealth>().ApplyDamage(5);
                //}

                // Instancia la explosion en el punto que colisiona
                Instantiate(explosionEffect, hitInfo.point, Quaternion.identity);

                // Luego setea el lineRenderer
                lineRenderer.SetPosition(0, _firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point);
            } else {
                lineRenderer.SetPosition(0, _firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point + Vector2.right * 100);
            }

            // Muestro el componente lineRenderer
            lineRenderer.enabled = true;

            // Espero 0.1 segundos para que se vea el lineRenderer
            yield return new WaitForSeconds(0.1f);

            // Y cuando vuelve a la funcion lo desactivo
            lineRenderer.enabled = false;
        }
    }
}
