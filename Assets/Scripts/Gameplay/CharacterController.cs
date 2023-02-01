using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class CharacterController : MonoBehaviour
{
    public UnityEvent died = new UnityEvent();
    [SerializeField] protected CharacterTemplate template;
    [SerializeField] protected MovementController movementController;
    protected bool isTurn = false;
    protected Character character;

    protected virtual void Start()
    {
        character = new Character(template.name, template.Constitution, template.Intelligence);
        died.AddListener(OnDeath);
    }
    public void EndTurn()
    {
        isTurn = false;
    }

    public void StartTurn()
    {
        character.FP -= 1f + (character.INT / 4f);
        isTurn = true;
    }

    public Character GetCharacter()
    {
        return character;
    }

    public void Attack(CharacterController other)
    {
        var enemy = other.GetCharacter();
        enemy.TakeDamage(character.CON);
        if (enemy.HP == 0f) other.died.Invoke();
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (isTurn)
        {
            movementController.finishedMovement.Invoke();
        }
    }
}
