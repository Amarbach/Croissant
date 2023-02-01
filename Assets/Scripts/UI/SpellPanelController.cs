using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellPanelController : MonoBehaviour
{
    [SerializeField] Image[] spellIcons = new Image[9];
    public Image[] Icons { get { return spellIcons; } set { spellIcons = value; } }
}
