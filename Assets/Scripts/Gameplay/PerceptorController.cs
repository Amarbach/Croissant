using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PerceptorController : MonoBehaviour
{
    public UnityEvent<EnemyController> spotted = new();
    public UnityEvent<EnemyController> away = new();
    private EnemyController parentEnemy;
    private void Start()
    {
        parentEnemy = transform.parent.GetComponent<EnemyController>();
        var turnController = transform.parent.transform.parent.GetComponent<TurnController>();
        if (turnController != null)
        {
            spotted.AddListener(turnController.AddEnemy);
            away.AddListener(turnController.RemoveEnemy);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Faction0"))
        {
            spotted.Invoke(parentEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Faction0"))
        {
            away.Invoke(parentEnemy);
        }
    }
}
