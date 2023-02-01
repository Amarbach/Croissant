using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerCharacterController : CharacterController
{
    [SerializeField] private ExplosionController explosionPrefab;
    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private GameObject blockPrefab;
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
                if (!isAiming) movementController.Move(new Vector2(0.0f, 1.0f));
                else target.position += new Vector3(0.0f, 1.0f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.S) && !isObserving)
            {
                if (!isAiming) movementController.Move(new Vector2(0.0f, -1.0f));
                else target.position += new Vector3(0.0f, -1.0f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.A) && !isObserving)
            {
                if (!isAiming) movementController.Move(new Vector2(-1.0f, 0.0f));
                else target.position += new Vector3(-1.0f, 0.0f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.D) && !isObserving)
            {
                if (!isAiming) movementController.Move(new Vector2(1.0f, 0.0f));
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
        //letter1.Name = "eme";
        //letter1.Cost = 3f;
        //letter1.Power = 5f;
        //letter1.Modifiers.Add(new SpellModifier() { Condition = "offensive", Intensity = 0.5f, Type = ModType.PCPOWER });
        //letter1.Effects.Add("fire");
        //letter1.Type = "offensive";

        Rune letter2 = new Rune();
        //letter2.Name = "kee";
        //letter2.Cost = 1f;
        //letter2.Power = 6f;
        //letter2.Modifiers.Add(new SpellModifier() { Condition = "offensive", Intensity = 2f, Type = ModType.NCOST });
        //letter2.Effects.Add("poison");
        //letter2.Effects.Add("earth");
        //letter2.Type = "offensive";

        TargetingRune letter0 = new TargetingRune();
        //letter0.Name = "iss";
        //letter0.Cost = 1f;
        //letter0.Modifiers.Add(new SpellModifier() { Condition = "offensive", Intensity = -1f, Type = ModType.NCOST });
        //letter0.Type = "targeting";
        //letter0.Area[2][2] = true;
        //letter0.Area[2][3] = true;
        //letter0.Area[2][1] = true;
        //letter0.Area[3][2] = true;
        //letter0.Area[1][2] = true;

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

    void CastSpellAt(Spell toCast, Vector3 position)
    {
        character.FP += toCast.Cost;
        for(int i =0; i < toCast.Targeting.Area.Length; i++)
        {
            for(int j=0;j< toCast.Targeting.Area[i].Length; j++)
            {
                if (toCast.Targeting.Area[i][j]) Instantiate<ExplosionController>(explosionPrefab, new Vector3(position.x + (j - 2), position.y + (i - 2), position.z), Quaternion.identity).SetSource(this, toCast);
            }
        }
    }
}
