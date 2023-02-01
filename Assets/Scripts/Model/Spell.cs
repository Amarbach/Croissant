using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spell
{
    private string name;
    public string Name { get { return name; } set { name = value; } }
    private float power;
    public float Power { get { return power; } private set { power = Mathf.Floor(value); } }
    private float cost;
    public float Cost { get { return cost; } private set { cost = Mathf.Floor(value); } }
    private List<Rune> composition;
    public List<Rune> Runes { get { return composition; } }
    private TargetingRune targeting;
    public TargetingRune Targeting { get { return targeting; } private set { targeting = value; } }
    private List<string> effectTags;
    public List<string> Effects { get { return effectTags; } }
    private List<SpellModifier> modifiers;

    public Spell() 
    {
        modifiers = new List<SpellModifier>();
        effectTags = new List<string>();
        composition = new List<Rune>();
        targeting = null;
    }

    public void AddRune(Rune toAdd)
    {
        if (composition.Where(x => x.Name == toAdd.Name).Count() < 1) this.modifiers.AddRange(toAdd.Modifiers);
        foreach(string effect in toAdd.Effects)
        {
            if (!this.Effects.Contains(effect)) this.Effects.Add(effect);
        }
        composition.Add(toAdd);
        this.RecalculateSpell();
    }

    public void RemoveLastRune()
    {
        if (composition.Count() < 1) return;
        Rune lastRune = this.composition[this.composition.Count - 1];
        this.composition.Remove(lastRune);
        foreach(string effect in lastRune.Effects)
        {
            if(this.composition.Where(x => x.Effects.Contains(effect)).Count() < 1)
            {
                this.Effects.Remove(effect);
            }
        }
        if (this.composition.Where(x => x.Name.Equals(lastRune.Name)).Count() < 1)
        {
            foreach(SpellModifier mod in lastRune.Modifiers)
            {
                this.modifiers.Remove(mod);
            }
        }
        this.RecalculateSpell();
    }

    public void AddTargetingRune(TargetingRune toAdd)
    {
        if (this.Targeting != null) this.RemoveTargetingRune();
        foreach (SpellModifier mod in toAdd.Modifiers)
        {
            this.modifiers.Add(mod);
        }
        this.Targeting = toAdd;
        RecalculateSpell();
    }

    public void RemoveTargetingRune()
    {
        if (targeting != null)
        {
            foreach (SpellModifier mod in this.targeting.Modifiers)
            {
                this.modifiers.Remove(mod);
            }
            targeting = null;
        }
        RecalculateSpell();
    }

    private void RecalculateSpell()
    {
        this.Power = 0f;
        this.Cost = this.Targeting != null ? this.Targeting.Cost : 0f;
        this.Name = this.Targeting != null ? this.Targeting.Name : "";
        float costMult = 1.0f;
        float powerMult = 1.0f;
        float costBon = 0f;
        float powerBon = 0f;
        foreach(Rune rune in composition)
        {
            this.Power += rune.Power;
            this.Cost += rune.Cost;
            this.Name += rune.Name;
        }
        foreach (SpellModifier mod in modifiers)
        {
            if (this.composition.Where(x => x.Type.Equals(mod.Condition)).Count() > 0 || mod.Condition.Equals(""))
            {
                switch (mod.Type)
                {
                    case ModType.NCOST:
                        costBon += mod.Intensity;
                        break;
                    case ModType.PCCOST:
                        costMult += mod.Intensity;
                        break;
                    case ModType.NPOWER:
                        powerBon += mod.Intensity;
                        break;
                    case ModType.PCPOWER:
                        powerMult += mod.Intensity;
                        break;
                    default:
                        break;
                }
            }
        }
        this.Power *= powerMult;
        this.Cost *= costMult;
        this.Power += powerBon;
        this.Cost += costBon;
    }

    public override string ToString()
    {
        string ret = this.Name + "\n";
        ret += "Power: " + this.Power + "\n";
        ret += "Cost: " + this.Cost + "\n\n";
        ret += "Effects: ";
        foreach(string effect in this.Effects)
        {
            ret += effect + ", ";
        }
        ret += "\n\nModifiers: ";
        foreach(SpellModifier mod in modifiers)
        {
            ret += mod.ToString() + ", ";
        }
        return ret;
    }
}