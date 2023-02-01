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
    private CharacterController mainController;
    private bool isMoving;
    public bool IsMoving { get { return isMoving; } }
    [SerializeField] private float speed = 1f;
    

    // Start is called before the first frame update
    void Start()
    {
        mainController = GetComponent<CharacterController>();
        var turnController = transform.parent.GetComponent<TurnController>();
        finishedMovement.AddListener(mainController.EndTurn);
        finishedMovement.AddListener(turnController.NextTurn);
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
            var targetContent = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y) + displacement, 0.45f);
            if (targetContent.Length <= 0)
            {
                target = grid.GetCellCenterLocal(grid.LocalToCell(target + displacement));
                isMoving = true;
            }
            else
            {
                if (targetContent.Where(x => !x.CompareTag("Untagged")).Count() > 0)
                {
                    transform.position += new Vector3(displacement.x / 2.0f, displacement.y / 2.0f, 0.0f);
                    isMoving = true;
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
                            mainController.Attack(enemies.First().gameObject.GetComponent<CharacterController>());
                        }
                    }
                }
                else
                {
                    target = grid.GetCellCenterLocal(grid.LocalToCell(target + displacement));
                    isMoving = true;
                }
            }
        }
    }

    public void SetPosition(float x, float y)
    {
        transform.position = new Vector3(x+0.5f, y+0.5f, 0.0f);
        target = grid.GetCellCenterLocal(grid.LocalToCell(transform.position));
    }

    public void EndTurn()
    {
        finishedMovement.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(target.x, target.y, 0.0f), 0.5f);
    }
}
