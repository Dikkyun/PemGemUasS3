using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UseItemRen : MonoBehaviour
{
    [Header("Heal")]
    [SerializeField] private float heal;
    [SerializeField] private float currentHealItem;
    [SerializeField] private Image healFill;
    [SerializeField] private TextMeshProUGUI healText;
    public float healCooldown;
    private bool isHealCooldown = false;
    private float currHealCooldown;


    [Header("Mana")]
    [SerializeField] private float mana;
    [SerializeField] private float currentManaItem;
    [SerializeField] private Image manaFill;
    [SerializeField] private TextMeshProUGUI manaText;
    public float manaCooldown;
    private bool isManaCooldown = false;
    private float currManaCooldown;

    HealthBarRen healthBarRen;

    // Start is called before the first frame update
    void Start()
    {
        healthBarRen = GetComponent<HealthBarRen>();

        healFill.fillAmount = 0;
        manaFill.fillAmount = 0;

        healText.text = "";
        manaText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        healText.text = currentHealItem.ToString();
        manaText.text = currentManaItem.ToString();

        UseHealInput();
        UseManaInput();

        SkillCooldown(ref currHealCooldown, healCooldown, ref isHealCooldown, healFill);
        SkillCooldown(ref currManaCooldown, manaCooldown, ref isManaCooldown, manaFill);
    }

    private void UseHealInput()
    {
        if (Input.GetKeyUp(KeyCode.RightShift) && !isHealCooldown && DataPersonalRen.currentHealthRen < 100 && currentHealItem != 0)
        {
            Debug.Log("Heal");
            healthBarRen.UpdateHeal(heal);
            isHealCooldown = true;
            currHealCooldown = healCooldown;

            currentHealItem -= 1f;
        }
    }

    private void UseManaInput()
    {
        if (Input.GetKeyUp(KeyCode.RightControl) && !isManaCooldown && healthBarRen.currentMana < 100 && currentManaItem != 0)
        {
            Debug.Log("Heal");
            healthBarRen.UpdateMana(mana);
            isManaCooldown = true;
            currManaCooldown = manaCooldown;

            currentManaItem -= 1f;
        }
    }

    private void SkillCooldown(ref float currentCooldown, float MaxCooldown, ref bool isCooldown, Image itemImage)
    {
        Debug.Log("Item Cooldown");
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown < 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;

                if (itemImage != null)
                {
                    itemImage.fillAmount = 0f;
                }
            }
            else
            {
                if (itemImage != null)
                {
                    itemImage.fillAmount = currentCooldown / MaxCooldown;
                }
            }
        }
    }
}
