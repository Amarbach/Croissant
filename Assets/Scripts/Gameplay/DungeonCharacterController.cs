using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class DungeonCharacterController : MonoBehaviour
{
    public UnityEvent died = new UnityEvent();
    [SerializeField] protected CharacterTemplate template;
    [SerializeField] protected MovementController movementController;
    [SerializeField] protected ExplosionController explosionPrefab;
    [SerializeField] protected GameObject cloudPrefab;
    [SerializeField] protected GameObject blockPrefab;
    protected Grid grid;
    protected bool isTurn = false;
    protected Character character;
    protected TurnController turnController;
    private int spellCooldown = 0;

    protected virtual void Start()
    {
        character = new Character(template.name, template.Constitution, template.Intelligence);
        grid = transform.parent.transform.parent.GetComponent<Grid>();
        turnController = transform.parent.gameObject.GetComponent<TurnController>();
        died.AddListener(OnDeath);
    }
    public virtual void EndTurn()
    {
        if (isTurn)
        {
            isTurn = false;
            turnController.NextTurn();
        }
    }

    public void StartTurn()
    {
        if (spellCooldown < 1) character.FP -= 1f + (character.INT / 4f);
        else spellCooldown--;
        isTurn = true;
    }

    public Character GetCharacter()
    {
        return character;
    }

    public void Attack(DungeonCharacterController other)
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

    protected void CheckDestination(Vector2 displacement)
    {
        var targetContent = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y) + displacement, 0.45f);
        if (targetContent.Length <= 0)
        {
            movementController.Move(displacement);
        }
        else
        {
            if (targetContent.Where(x => !x.CompareTag("Untagged")).Count() > 0)
            {
                movementController.Bounce(displacement);
                var doors = targetContent.Where(x => x.CompareTag("Door"));
                if (doors.Count() > 0)
                {
                    GameObject.Destroy(doors.First().gameObject);
                }
                else
                {
                    var enemies = targetContent.Where(x => !x.CompareTag(gameObject.tag) && (x.CompareTag("Faction1") || x.CompareTag("Faction0")));
                    if (enemies.Count() > 0)
                    {
                        Attack(enemies.First().gameObject.GetComponent<DungeonCharacterController>());
                    }
                }
            }
            else
            {
                movementController.Move(displacement);
            }
        }
    }

    protected void CastSpellAt(Spell toCast, Vector3 position)
    {
        character.FP += toCast.Cost;
        spellCooldown = 3;
        for (int i = 0; i < toCast.Targeting.Area.Length; i++)
        {
            for (int j = 0; j < toCast.Targeting.Area[i].Length; j++)
            {
                if (toCast.Targeting.Area[i][j])
                {
                    var spell = Instantiate<ExplosionController>(explosionPrefab, new Vector3(position.x + (j - 2), position.y + (i - 2), position.z), Quaternion.identity);
                    spell.SetSource(this, toCast);
                    spell.Faded.AddListener(EndTurn);
                }
            }
        }
    }
}
