using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : DungeonCharacterController
{
    bool isDeciding = true;
    [SerializeField] DungeonCharacterController focusedEnemy = null;
    override protected void Start()
    {
        base.Start();        
    }
    // Update is called once per frame
    void Update()
    {
        if (isTurn && isDeciding)
        {
            Vector2 disp = new Vector2(0, 0);
            if (focusedEnemy != null) 
            {
                var absX = Mathf.Abs(transform.position.x - focusedEnemy.transform.position.x);
                var absY = Mathf.Abs(transform.position.y - focusedEnemy.transform.position.y);
                if (absX > 0.1f) disp.x = (focusedEnemy.transform.position.x - transform.position.x) /absX;
                else if (absY > 0.1f) disp.y = (focusedEnemy.transform.position.y - transform.position.y) / absY;
            }
            CheckDestination(disp);
            isDeciding = false;
        }
    }

    public override void EndTurn()
    {
        base.EndTurn();
        isDeciding = true;
    }

    public void OnSpotted(DungeonCharacterController other)
    {
        turnController.AddEnemy(this);
        focusedEnemy = other;
    }

    public void OnAway(DungeonCharacterController other)
    {
        turnController.RemoveEnemy(this);
        focusedEnemy = null;
    }
}
