using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI hpValueText;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TextMeshProUGUI fpValueText;
    [SerializeField] private Slider fpSlider;
    [SerializeField] private Slider fpOverflowSlider;
    private int hp;
    private int maxHP;
    private int fp;
    private int maxFP;
    public string Name { get { return nameText.text; } set { nameText.text = value; } }
    public int HP { get { return hp; } set { hp = value; hpValueText.text = hp + "/" + maxHP; hpSlider.value = (float)hp / (float)maxHP; } }
    public int MaxHP { get { return maxHP; } set { maxHP = value; } }
    public int FP
    {
        get
        {
            return fp;
        }
        set
        {
            fp = value;
            fpValueText.text = fp + "/" + maxFP;
            if (fp <= maxFP)
            {
                fpSlider.value = (float)fp / (float)maxFP;
                fpOverflowSlider.gameObject.SetActive(false);
            }
            else
            {
                fpSlider.value = 1f;
                fpOverflowSlider.gameObject.SetActive(true);
                fpOverflowSlider.value = (float)(fp - maxFP) / (float)(maxFP);
            }
        }
    }
    public int MaxFP { get { return maxFP; } set { maxFP = value; } }
}
