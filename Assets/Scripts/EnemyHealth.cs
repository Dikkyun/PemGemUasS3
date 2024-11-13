using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Image healthBarFill;
    [SerializeField] private float fillSpeed;
    [SerializeField] private Gradient colorGradienHp;
    [SerializeField] private Animator animator;
    //[SerializeField] private string animatorName;

    [SerializeField] private float maxHealth = 160;
    private float currHealth;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currHealth -= damage;

        animator.SetTrigger("Hurt");

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float targetFillAmount = currHealth / maxHealth;
        healthBarFill.DOFillAmount(targetFillAmount, fillSpeed);
        healthBarFill.DOColor(colorGradienHp.Evaluate(targetFillAmount), fillSpeed);

        currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
        if (currHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        this.gameObject.SetActive(false);
    }
}
