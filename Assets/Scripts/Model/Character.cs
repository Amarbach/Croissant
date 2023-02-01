using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character
{
    private UnityEvent onStatusChange = new UnityEvent();
    public UnityEvent OnStatusChange { get { return onStatusChange; } }
    private UnityEvent onSpellsChange = new UnityEvent();
    public UnityEvent OnSpellsChange { get { return onSpellsChange; } }
    string name;
    public string Name { get { return name; } set { name = value; } }
    float curHealth, maxHealth;
    public float HP { 
        get 
        { 
            return curHealth; 
        } 
        set 
        { 
            curHealth = value; 
            if (curHealth > maxHealth) curHealth = maxHealth; 
            else if (curHealth < 0f) 
            { 
                curHealth = 0f;
                IsDead = true; 
            } 
            onStatusChange.Invoke();
        } 
    }
    public float MaxHP { get { return maxHealth; } private set { maxHealth = Mathf.Floor(value); } }

    float curFlux, maxFlux;
    public float FP
    {
        get
        {
            return curFlux;
        }
        set
        {
            curFlux = value;
            if (curFlux > 2 * maxFlux) curFlux = 2 * maxFlux;
            else if (curFlux < 0f)
            {
                curFlux = 0f;
            }
            onStatusChange.Invoke();
        }
    }
    public float MaxFP { get { return maxFlux; } private set { maxFlux = Mathf.Floor(value); } }

    float constitution, intelligence;
    public float CON { get { return constitution; } set { constitution = Mathf.Floor(value); } }
    public float INT { get { return intelligence; } set { intelligence = Mathf.Floor(value); } }

    float expirience, neededExpirience;
    public float EXP { get { return expirience; } private set { expirience = Mathf.Floor(value); } }
    public float NextEXP { get { return neededExpirience; } private set { neededExpirience = Mathf.Floor(value); } }

    int level, levelUps;
    public int LVL { get { return level; } private set { level = value; } }

    bool isDead;
    public bool IsDead { get { return isDead; } private set { isDead = value; } }

    Spell[] spells;
    public Spell[] Spells { get { return spells; } private set { spells = value; } }
    List<Rune> inventory;
    //List<Effect> effects;
    public List<Rune> Inventory { get { return inventory; } private set { inventory = value; } }

    public Character(string name, float constitution, float intelligence)
    {
        this.Name = name;
        this.CON = constitution;
        this.INT = intelligence;
        this.LVL = 1;
        this.MaxHP = (this.CON + (this.LVL / 2f)) * 5f;
        this.HP = this.MaxHP;
        this.maxFlux = (this.INT * 2 + (this.LVL / 4f)) * 3f;
        this.FP = 0f;
        this.IsDead = false;
        this.EXP = 0f;
        this.levelUps = 0;
        this.NextEXP = 500f * level;
        spells = new Spell[9] { null, null, null, null, null, null, null, null, null };
        inventory = new List<Rune>();
    }

    public void TakeDamage(float damage)
    {
        this.HP -= damage;
        //TODO effects
    }

    public void AddExp(float amount)
    {
        this.EXP += amount;
        if (this.EXP > this.NextEXP) this.LevelUP();
    }

    private void LevelUP()
    {
        this.EXP -= this.NextEXP;
        this.LVL += 1;
        levelUps++;
        this.NextEXP = 500f * level;
        if (this.EXP > this.NextEXP) LevelUP();
    }

    public bool HasLevelUPs()
    {
        return levelUps > 0;
    }

    public void AddSpellInSlot(Spell toAdd, int idx)
    {
        if (idx < 0 || idx > 8) return;
        this.spells[idx] = toAdd;
        this.OnSpellsChange.Invoke();
    }

    public void RemoveSpellInSlot(int idx)
    {
        if (idx < 0 || idx > 8) return;
        this.spells[idx] = null;
        this.OnSpellsChange.Invoke();
    }
}
