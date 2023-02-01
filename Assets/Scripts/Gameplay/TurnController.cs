using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    CharacterController character;
    List<EnemyController> enemies = new();
    int enemyTurnIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        character = gameObject.GetComponentInChildren<CharacterController>();
        character.StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextTurn()
    {
        if (enemyTurnIndex >= enemies.Count)
        {
            enemyTurnIndex = 0;
            character.StartTurn();
        }
        else if (enemyTurnIndex < enemies.Count)
        {
            enemies[enemyTurnIndex].StartTurn();
            enemyTurnIndex++;
        }
    }

    public void AddEnemy(EnemyController toAdd)
    {
        enemies.Add(toAdd);
    }

    public void RemoveEnemy(EnemyController toRemove)
    {
        enemies.Remove(toRemove);
    }
}
