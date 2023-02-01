using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] CharacterPanelController panel;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            transform.position += new Vector3(0, 1, 0);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            transform.position += new Vector3(0, -1, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test");
        if(collision.CompareTag("Faction1") || collision.CompareTag("Faction0"))
        {
            panel.gameObject.SetActive(true);
            panel.Name = collision.gameObject.name;
            CharacterController seen;
            if(collision.gameObject.TryGetComponent<CharacterController>(out seen))
            {
                Character stats = seen.GetCharacter();
                panel.Name = stats.Name;
                panel.MaxHP = (int)stats.MaxHP;
                panel.HP = (int)stats.HP;
                panel.MaxFP = (int)stats.MaxFP;
                panel.FP = (int)stats.FP;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        panel.gameObject.SetActive(false);
    }
}
