using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerCharacterController : DungeonCharacterController
{
    [SerializeField] private Transform target;
    bool isObserving;
    bool isAiming;
    Spell toCast = null;

    override protected void Start()
    {
        base.Start();
        GameObject.Find("Main Camera").GetComponent<CameraController>().SetFollowed(transform);
        Test();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurn)
        {
            if (Input.GetKeyDown(KeyCode.W) && !isObserving)
            {
                if (!isAiming) CheckDestination(new Vector2(0.0f, 1.0f));
                else target.position += new Vector3(0.0f, 1.0f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.S) && !isObserving)
            {
                if (!isAiming) CheckDestination(new Vector2(0.0f, -1.0f));
                else target.position += new Vector3(0.0f, -1.0f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.A) && !isObserving)
            {
                if (!isAiming) CheckDestination(new Vector2(-1.0f, 0.0f));
                else target.position += new Vector3(-1.0f, 0.0f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.D) && !isObserving)
            {
                if (!isAiming) CheckDestination(new Vector2(1.0f, 0.0f));
                else target.position += new Vector3(1.0f, 0.0f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.Space) && !isObserving && !isAiming)
            {
                movementController.Move(new Vector2(0.0f, 0.0f));
            }
            if (Input.GetKeyDown(KeyCode.Alpha1) && !isObserving)
            {
                toCast = character.Spells[0];
                if (toCast != null)
                {
                    isAiming = true;
                    target.gameObject.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && !isObserving)
            {
                toCast = character.Spells[1];
                if (toCast != null)
                {
                    isAiming = true;
                    target.gameObject.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && !isObserving)
            {
                toCast = character.Spells[2];
                if (toCast != null)
                {
                    isAiming = true;
                    target.gameObject.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && !isObserving)
            {
                toCast = character.Spells[3];
                if (toCast != null)
                {
                    isAiming = true;
                    target.gameObject.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && !isObserving)
            {
                toCast = character.Spells[4];
                if (toCast != null)
                {
                    isAiming = true;
                    target.gameObject.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) && !isObserving)
            {
                toCast = character.Spells[5];
                if (toCast != null)
                {
                    isAiming = true;
                    target.gameObject.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha7) && !isObserving)
            {
                toCast = character.Spells[6];
                if (toCast != null)
                {
                    isAiming = true;
                    target.gameObject.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha8) && !isObserving)
            {
                toCast = character.Spells[7];
                if (toCast != null)
                {
                    isAiming = true;
                    target.gameObject.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha9) && !isObserving)
            {
                toCast = character.Spells[8];
                if (toCast != null)
                {
                    isAiming = true;
                    target.gameObject.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Z) && !isObserving)
            {
                if (isAiming)
                {
                    CastSpellAt(toCast, target.position);
                    toCast = null;
                    isAiming = false;
                    target.gameObject.SetActive(false);
                    target.position = transform.position;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            isObserving = !isObserving;
        }
    }

    void Test()
    {
        Rune letter1 = new Rune();

        Rune letter2 = new Rune();

        TargetingRune letter0 = new TargetingRune();

        XmlSerializer serializer = new XmlSerializer(typeof(Rune));
        using (FileStream file = new FileStream(letter1.Name + "eme.xml", FileMode.Open))
        {
            letter1 = (Rune)serializer.Deserialize(file);
        }
        using (FileStream file = new FileStream(letter2.Name + "kee.xml", FileMode.Open))
        {
            letter2 = (Rune)serializer.Deserialize(file);
        }
        serializer = new XmlSerializer(typeof(TargetingRune));
        using (FileStream file = new FileStream(letter0.Name + "iss.xml", FileMode.Open))
        {
            letter0 = (TargetingRune)serializer.Deserialize(file);
        }

        Spell test = new Spell();
        test.AddTargetingRune(letter0);
        test.AddRune(letter1);
        test.AddRune(letter2);
        

        character.AddSpellInSlot(test, 0);
    }
}
