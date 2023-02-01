using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[System.Xml.Serialization.XmlRoot(ElementName = "Rune")]
public class Rune
{
    [XmlElement]
    private string name;
    public string Name { get { return name; } set { name = value; } }
    [XmlElement]
    private string iconName;
    public string Icon { get { return iconName; } set { iconName = value; } }
    [XmlElement]
    private float power;
    public float Power { get { return power; } set { power = Mathf.Floor(value); } }
    [XmlElement]
    private float cost;
    public float Cost { get { return cost; } set { cost = Mathf.Floor(value); } }
    [XmlElement]
    private string type;
    public string Type { get { return type; } set { type = value; } }
    [XmlElement(ElementName = "Effects")]
    protected List<string> effectTags;
    public List<string> Effects { get { return effectTags; } set { effectTags = value; } }
    [XmlElement]
    protected List<SpellModifier> modifiers;
    public List<SpellModifier> Modifiers { get { return modifiers; } set { modifiers = value; } }

    public Rune()
    {
        effectTags = new List<string>();
        modifiers = new List<SpellModifier>();
    }

    public virtual string GetDesc()
    {
        string ret = this.Name + "\n";
        ret += "Power: " + this.Power + "\n";
        ret += "Cost: " + this.Cost + "\n\n";
        foreach (string effect in this.Effects)
        {
            ret += effect + ", ";
        }
        ret += "\n\n";
        foreach (SpellModifier mod in modifiers)
        {
            ret += mod.ToString() + ", ";
        }

        return ret;
    }
}

[Serializable]
[System.Xml.Serialization.XmlRoot(ElementName = "TargetingRune")]
public class TargetingRune : Rune
{
    [XmlElement]
    private bool[][] area;
    public bool[][] Area { get { return area; } set { area = value; } }
    [XmlElement]
    private bool aimStyle;
    public bool Aim { get { return aimStyle; } set { aimStyle = value; } }
    [XmlElement]
    private bool travelStyle;
    public bool Travel { get { return travelStyle; } set { travelStyle = value; } }

    public TargetingRune()
    {
        area = new bool[5][];
        for(int i=0; i<5; i++)
        {
            area[i] = new bool[5] { false, false, false, false, false };
        }
        this.Type = "Targeting";
        this.Power = 0;
    }

    public override string GetDesc()
    {
        string ret = this.Name + "\n";
        ret += "Power: " + this.Power + "\n";
        ret += "Cost: " + this.Cost + "\n\n";
        ret += "Targeting style: ";
        if (this.aimStyle) ret += "aimed, ";
        else ret += "fixed, ";
        if (this.travelStyle) ret += "ray\n\n";
        else ret += "spark\n\n";
        ret += "Modifiers: ";
        foreach (SpellModifier mod in modifiers)
        {
            ret += mod.ToString() + ", ";
        }

        return ret;
    }
}
