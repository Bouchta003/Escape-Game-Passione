using UnityEngine;

[CreateAssetMenu(fileName = "MaterialData", menuName = "Puzzles/Anode & Cathode/MaterialData")]
public class MaterialData : ScriptableObject
{
    public string materialName;
    public int efficiency;
    public int capacity;
    public int safety;
    public Color displayColor;
}
