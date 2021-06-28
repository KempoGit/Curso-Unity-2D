using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject bulletPrefab;
    private Transform _firePoint;
    public GameObject shooter;

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

    void Shoot()
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
}
