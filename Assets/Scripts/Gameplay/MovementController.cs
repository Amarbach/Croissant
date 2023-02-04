using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    public UnityEvent finishedMovement = new();
    private Vector2 target;
    private Grid grid;
    private bool isMoving;
    [SerializeField] private float speed = 1f;
    

    // Start is called before the first frame update
    void Start()
    {
        var mainController = GetComponent<DungeonCharacterController>();
        var turnController = transform.parent.GetComponent<TurnController>();
        finishedMovement.AddListener(mainController.EndTurn);
        isMoving = false;
        grid = transform.parent.transform.parent.GetComponent<Grid>();
        target = grid.GetCellCenterLocal(grid.LocalToCell(transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, target.y, 0.0f), speed * Time.deltaTime);
            if (Vector2.Distance(target, new Vector2(transform.position.x, transform.position.y)) <= speed * Time.deltaTime)
            {
                isMoving = false;
                transform.position = target;
                finishedMovement.Invoke();
            }
        }
    }

    public void Move(Vector2 displacement)
    {
        if (!isMoving)
        {
            target = grid.GetCellCenterLocal(grid.LocalToCell(target + displacement));
            isMoving = true;
        }
    }

    public void Bounce(Vector2 displacement)
    {
        if (!isMoving)
        {
            transform.position += new Vector3(displacement.normalized.x, displacement.normalized.y, 0f) * 0.5f;
            isMoving = true;
        }
    }

    public void SetPosition(float x, float y)
    {
        transform.position = new Vector3(x+0.5f, y+0.5f, 0.0f);
        target = grid.GetCellCenterLocal(grid.LocalToCell(transform.position));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(target.x, target.y, 0.0f), 0.15f);
    }
}
