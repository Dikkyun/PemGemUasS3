using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirePurpleB : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float damageFirePurple = 80f;
    public GameObject impactEffect;

    public string playerTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //if (hitInfo.tag != playerTag)
        //{
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
        BossHealth Boss = hitInfo.GetComponent<BossHealth>();

        if (enemy != null && enemy.CompareTag("Enemy"))
        {
            Debug.Log("enemy hit");
            enemy.TakeDamage(damageFirePurple);
        }
        if (Boss != null && Boss.CompareTag("Boss"))
        {
            Debug.Log("Boss hit");
            Boss.TakeDamage(damageFirePurple);
        }


        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);

        //}
    }
}
