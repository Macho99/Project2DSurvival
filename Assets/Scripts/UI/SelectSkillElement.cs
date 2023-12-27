using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkillElement : MonoBehaviour
{
    [SerializeField] private Image weaponImage;
    [SerializeField] private GameObject newIcon;
    [SerializeField] private TextMeshProUGUI desc1;
    [SerializeField] private TextMeshProUGUI desc2;

    public void Init(bool isNew, Sprite sprite, string name, string desc2)
    {
        newIcon.SetActive(isNew);
        weaponImage.sprite= sprite;
        this.desc1.text = name;
        this.desc2.text = desc2;
    }

    public string GetName()
    {
        return this.desc1.text;
    }
}
