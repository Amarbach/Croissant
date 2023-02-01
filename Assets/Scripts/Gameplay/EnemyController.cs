using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    override protected void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    void Update()
    {
        if (isTurn)
        {
            Vector2 disp = new Vector2(0, 0);
            if (Random.Range(0, 2) > 0) disp.x = Random.Range(0, 2) * 2 - 1;
            else disp.y = Random.Range(0, 2) * 2 - 1;
            movementController.Move(disp);
        }
    }
}
