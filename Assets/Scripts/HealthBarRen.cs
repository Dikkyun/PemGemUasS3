using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBarRen : MonoBehaviour
{
    private float currentHealth;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private float fillSpeed;
    [SerializeField] private Gradient colorGradientHp;
    [SerializeField] private Animator animator;

    void Start()
    {

        // Load health from PlayerPrefs
        DataPersonalRen.LoadHealth();

        currentHealth = DataPersonalRen.currentHealthRen;
        healthText.text = "Health: " + DataPersonalRen.currentHealthRen;
        UpdateHealthBar();
    }

    private void Update()
    {
        
    }

    public void UpdateHealth(float amount)
    {
        DataPersonalRen.currentHealthRen += amount;
        animator.SetTrigger("Hurt");
        DataPersonalRen.currentHealthRen = Mathf.Clamp(DataPersonalRen.currentHealthRen, 0, DataPersonalRen.maxHealthRen);

        healthText.text = "Health: " + DataPersonalRen.currentHealthRen;
        UpdateHealthBar();

        // Save updated health to PlayerPrefs
        DataPersonalRen.SaveHealth();
    }

    private void UpdateHealthBar()
    {
        float targetFillAmount = DataPersonalRen.currentHealthRen / maxHealth;
        healthBarFill.DOFillAmount(targetFillAmount, fillSpeed);
        healthBarFill.DOColor(colorGradientHp.Evaluate(targetFillAmount), fillSpeed);

        if (DataPersonalRen.currentHealthRen == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetTrigger("Death");
        animator.SetBool("noBlood", false);
    }
}
