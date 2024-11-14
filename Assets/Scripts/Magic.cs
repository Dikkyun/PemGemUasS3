using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public Transform firePoint;
    public GameObject purpleFire;
    public float cooldown = 1.5f;
    public bool canShoot = true;

    HealthBarRen healthBarRen;


    // Start is called before the first frame update
    void Start()
    {
        healthBarRen = GetComponent<HealthBarRen>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && canShoot && healthBarRen.currentMana >= 20)
        {
            healthBarRen.UpdateMana(-20);
            StartCoroutine(ShootCooldown());
        }else if (Input.GetKeyDown(KeyCode.A) && !canShoot)
        {
            Debug.Log("Magic Cooldown");
        }
        else if (Input.GetKeyDown(KeyCode.A) && canShoot && healthBarRen.currentMana <= 0)
        {
            Debug.Log("NO Mana");
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
