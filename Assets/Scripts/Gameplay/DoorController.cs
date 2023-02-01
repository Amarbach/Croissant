using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        bool destroy = collision.CompareTag("Player") || collision.CompareTag("Enemy");
        if (destroy)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        bool destroy = collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy");
        if (destroy)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        bool destroy = collision.CompareTag("Player") || collision.CompareTag("Enemy");
        if (destroy)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
