using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExplosionController : MonoBehaviour
{
    
    Spell source = null;
    DungeonCharacterController caster = null;
    [SerializeField] FadeController fade;
    public UnityEvent Faded { get { return fade.faded; } }

    private void Start()
    {
        fade.Fade(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Faction0") || collision.CompareTag("Faction1"))
        {
            DungeonCharacterController character;
            if(collision.TryGetComponent<DungeonCharacterController>(out character))
            {
                character.GetCharacter().TakeDamage(source.Power);
                if (character.GetCharacter().IsDead) character.died.Invoke();
            }
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    public ExplosionController SetSource(DungeonCharacterController caster, Spell spell)
    {
        this.caster = caster;
        this.source = spell;
        return this;
    }


}
