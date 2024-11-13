using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HealthBarRen : MonoBehaviour
{
    [Header("Health")]
    private float currentHealth;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private float fillSpeed;
    [SerializeField] private Gradient colorGradientHp;
    [SerializeField] private Animator animator;

    [Header("Mana")]
    private float currentMana;
    [SerializeField] private float maxMana = 100;
    [SerializeField] private Image ManaBarFill;
    [SerializeField] private TextMeshProUGUI ManaText;
    [SerializeField] private Gradient colorGradientMana;

    [Header("Spawn")]
    public Transform startPosition;
    public bool isInvulnerable = false;
    public GameObject DieUI;

    Vector2 startPos;
    Rigidbody2D rb;

    void Start()
    {
        currentMana = maxMana;
        // Load health from PlayerPrefs
        DataPersonalRen.LoadHealth();

        currentHealth = DataPersonalRen.currentHealthRen;
        healthText.text = "Health: " + DataPersonalRen.currentHealthRen;
        UpdateHealthBar();

        startPos = startPosition.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    public void UpdateHealth(float amount)
    {
        if (isInvulnerable)
        {
            return;
        }

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
        healthBarFill.DOColor(colorGradientMana.Evaluate(targetFillAmount), fillSpeed);

        if (currentMana == 0)
        {
            isInvulnerable = true;
        }
    }

    public void UpdateMana(float amount)
    {
        if (isInvulnerable)
        {
            return;
        }

        currentMana += amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);

        ManaText.text = "Health: " + currentMana;
        UpdateHealthBar();

        // Save updated health to PlayerPrefs
        DataPersonalRen.SaveHealth();
    }

    private void UpdateManaBar()
    {
        float targetFillAmount = currentMana / maxMana;
        ManaBarFill.DOFillAmount(targetFillAmount, fillSpeed);
        ManaBarFill.DOColor(colorGradientHp.Evaluate(targetFillAmount), fillSpeed);

        if (DataPersonalRen.currentHealthRen == 0)
        {
            Die();
            isInvulnerable = true;
        }
    }

    public void Die()
    {
        StartCoroutine(PlayerDie());
    }

    IEnumerator PlayerDie()
    {
        GetComponent<RenMovement>().CanMove(true);
        rb.velocity = new Vector2(0,0);
        animator.SetTrigger("Death");
        animator.SetBool("noBlood", false);
        
        yield return new WaitForSeconds(1f);
        
        DieUI.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Respawn()
    {
        DataPersonalRen.currentHealthRen = DataPersonalRen.maxHealthRen;
        DataPersonalRen.SaveHealth();
        DataPersonalRen.LoadHealth();
        UpdateHealthBar();
        transform.position = startPos;
        Time.timeScale = 1f;
        
        DieUI.gameObject.SetActive(false);
        isInvulnerable = false;

    }

    public void SceneLoadMenu(string SceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneName);
    }
}
