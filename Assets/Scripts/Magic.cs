using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public Transform firePoint;
    public GameObject purpleFire;
    public float cooldown = 1.5f;
    public bool canShoot = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && canShoot)
        {
            StartCoroutine(ShootCooldown());
        }else if (Input.GetKeyDown(KeyCode.D) && !canShoot)
        {
            Debug.Log("Magic Cooldown");
        }
    }

    void Shoot()
    {
        Instantiate(purpleFire, firePoint.position, firePoint.rotation);
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        Shoot();
        yield return new WaitForSeconds(cooldown);

        canShoot=true;
    }
}
