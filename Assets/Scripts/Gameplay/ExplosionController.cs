using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    Spell source = null;
    CharacterController caster = null;
    float lifetime = 0.1f;
    float life = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (life > lifetime)
        {
            Destroy(gameObject);
        }
        life += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Faction0") || collision.CompareTag("Faction1"))
        {
            CharacterController character;
            if(collision.TryGetComponent<CharacterController>(out character))
            {
                character.GetCharacter().TakeDamage(source.Power);
                if (character.GetCharacter().IsDead) character.died.Invoke();
            }
        }
    }

    public void SetSource(CharacterController caster, Spell spell)
    {
        this.caster = caster;
        this.source = spell;
    }
}
