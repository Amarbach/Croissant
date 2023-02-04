using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PerceptorController : MonoBehaviour
{
    public UnityEvent<DungeonCharacterController> spotted = new();
    public UnityEvent<DungeonCharacterController> away = new();
    private EnemyController parentEnemy;
    private void Start()
    {
        parentEnemy = transform.parent.GetComponent<EnemyController>();
        var turnController = transform.parent.transform.parent.GetComponent<TurnController>();
        if (turnController != null)
        {
            spotted.AddListener(parentEnemy.OnSpotted);
            away.AddListener(parentEnemy.OnAway);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Faction0"))
        {
            spotted.Invoke(collision.GetComponent<DungeonCharacterController>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Faction0"))
        {
            away.Invoke(collision.GetComponent<DungeonCharacterController>());
        }
    }
}
