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
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private float fillSpeed;
    [SerializeField] private Gradient colorGradientHp;
    [SerializeField] private Animator animator;
    private float currentHealth;

    [Header("Mana")]
    [SerializeField] private float maxMana = 100;
    [SerializeField] private Image ManaBarFill;
    [SerializeField] private TextMeshProUGUI ManaText;
    [SerializeField] private Gradient colorGradientMana;
    public float currentMana;

    [Header("Spawn")]
    public Transform startPosition;
    public bool isInvulnerable = false;
    public GameObject DieUI;
    public bool respawn = false;

    Vector2 startPos;
    Rigidbody2D rb;
    //Magic magic;

    void Start()
    {
        //magic = GetComponent<Magic>();
        currentMana = maxMana;
        // Load health from PlayerPrefs
        DataPersonalRen.LoadHealth();

        currentHealth = DataPersonalRen.currentHealthRen;
        healthText.text = "Health: " + DataPersonalRen.currentHealthRen;
        ManaText.text = "Health: " + currentMana;
        UpdateHealthBar();
        UpdateManaBar();

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
        healthBarFill.DOColor(colorGradientHp.Evaluate(targetFillAmount), fillSpeed);

        if (DataPersonalRen.currentHealthRen == 0)
        {
            Die();
            isInvulnerable = true;
        }
    }

    public void UpdateMana(float amount)
    {
        currentMana += amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);

        ManaText.text = "Health: " + currentMana;
        UpdateManaBar();

        // Save updated health to PlayerPrefs
        DataPersonalRen.SaveHealth();
    }

    private void UpdateManaBar()
    {
        float targetFillAmount = currentMana / maxMana;
        ManaBarFill.DOFillAmount(targetFillAmount, fillSpeed);
        ManaBarFill.DOColor(colorGradientMana.Evaluate(targetFillAmount), fillSpeed);       
    }

    public void Die()
    {
        StartCoroutine(PlayerDie());
    }

    IEnumerator PlayerDie()
    {
        GetComponent<RenMovement>().CanMove(false);
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Death");
        animator.SetBool("noBlood", false);
        
        yield return new WaitForSeconds(1f);
        
        DieUI.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Respawn()
    {
        animator.ResetTrigger("Death");
        animator.SetBool("Respawn",true);
        DataPersonalRen.currentHealthRen = DataPersonalRen.maxHealthRen;
        DataPersonalRen.SaveHealth();
        DataPersonalRen.LoadHealth();
        UpdateHealthBar();
        transform.position = startPos;
        Time.timeScale = 1f;
        
        DieUI.gameObject.SetActive(false);
        isInvulnerable = false;
        StartCoroutine(ResetRespawnAnim());
    }

    IEnumerator ResetRespawnAnim()
    {
        yield return new WaitForSeconds(0.5f);
        respawn = true;
        //animator.SetInteger("AnimState", 0);
        animator.SetBool("Respawn", false);
        GetComponent<RenMovement>().CanMove(true);

        //yield return new WaitForSeconds(1f);
        //respawn = false;

    }

    public void SceneLoadMenu(string SceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneName);
    }
}
