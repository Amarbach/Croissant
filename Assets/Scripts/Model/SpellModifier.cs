


using System;
using System.Xml.Serialization;

public enum ModType
{
    NPOWER,
    NCOST,
    PCPOWER,
    PCCOST,
}

[Serializable]
[System.Xml.Serialization.XmlRoot(ElementName = "Modifier")]
public class SpellModifier
{
    [XmlElement]
    private ModType type;
    public ModType Type { get { return type; } set { type = value; } }
    [XmlElement]
    private float intensity;
    public float Intensity { get { return intensity; } set { intensity = value; } }
    [XmlElement]
    private string condition;
    public string Condition { get { return condition; } set { condition = value; } }

    public override string ToString()
    {
        string intensityStr;
        if (type == ModType.NPOWER || type == ModType.NCOST)
        {
            intensityStr = intensity.ToString("+#;-#;0");
        }
        else
        {
            intensityStr = (intensity*100f).ToString("+#;-#;0");
        }
        string typeStr;
        switch (type)
        {
            case ModType.NPOWER: typeStr = " power"; break;
            case ModType.NCOST: typeStr = " cost"; break;
            case ModType.PCPOWER: typeStr = "% power"; break;
            case ModType.PCCOST: typeStr = "% cost"; break;
            default: typeStr = ""; break;
        }
        return intensityStr + typeStr + " with " + condition + " runes";
    }
}