using UnityEngine;

public enum Rarety
{
    Common,
    Rare,
    Epic,
    Legendary
}

public class RaretyPartObject : MonoBehaviour
{
    public Rarety rarety;
    public float probability;
    public Material originalMaterial;
    public Material material;
}
