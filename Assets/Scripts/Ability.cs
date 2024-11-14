using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ability : MonoBehaviour
{
    [Header("Skill")]
    public Image skillImage;
    public TextMeshProUGUI skillText;
    public float skillCooldown;

    private bool isSkillCooldown = false;

    private float currentSkillCooldown;

    HealthBarRen healthBarRen;

    // Start is called before the first frame update
    void Start()
    {
        healthBarRen = GetComponent<HealthBarRen>();
        skillImage.fillAmount = 0;

        skillText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        SkillInput();

        SkillCooldown(ref currentSkillCooldown, skillCooldown, ref isSkillCooldown, skillImage, skillText);
    }

    private void SkillInput()
    {
        if(Input.GetKeyUp(KeyCode.A) && !isSkillCooldown && healthBarRen.currentMana >= 20)
        {
            Debug.Log("Skill Icon");
            isSkillCooldown = true;
            currentSkillCooldown = skillCooldown;
        }
    }

    private void SkillCooldown(ref float currentCooldown, float MaxCooldown, ref bool isCooldown, Image SkillImage, TextMeshProUGUI skillText)
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;

            if(currentCooldown < 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;

                if(SkillImage != null)
                {
                    SkillImage.fillAmount = 0f;
                }
                if(skillText != null)
                {
                    skillText.text = "";
                }
            }
            else
            {
                if(SkillImage != null)
                {
                    SkillImage.fillAmount = currentCooldown / MaxCooldown;
                }
                if (skillText != null)
                {
                    skillText.text = Mathf.Ceil(currentCooldown).ToString();
                }
            }
        }
    }
}
