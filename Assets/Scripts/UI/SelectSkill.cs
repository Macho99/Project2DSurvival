using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkill : MonoBehaviour
{
    private SelectSkillElement[] elements;
    private ToggleGroup group;

    private void Awake()
    {
        elements = GetComponentsInChildren<SelectSkillElement>();
        group = GetComponent<ToggleGroup>();
    }

    public void Init(Weapon[] weapons, bool[] isNews)
    {
        gameObject.SetActive(true);
        for(int i=0;i<elements.Length;i++)
        {
            elements[i].Init(isNews[i], weapons[i].GetBoxedSprite(),
                weapons[i].name, weapons[i].GetDescription());
        }
    }

    public void OnButtonClick()
    {
        string skillName = group.ActiveToggles().ToArray()[0].GetComponent<SelectSkillElement>().GetName();
        SkillManager.Instance.OnSkillSelect(skillName);
        gameObject.SetActive(false);
    }
}