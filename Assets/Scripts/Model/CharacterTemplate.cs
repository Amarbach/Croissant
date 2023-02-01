using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Rogal/Character", order = 1)]
public class CharacterTemplate : ScriptableObject
{
    public string Name;

    public float Intelligence, Constitution;

    public Sprite CharacterSprite;
}