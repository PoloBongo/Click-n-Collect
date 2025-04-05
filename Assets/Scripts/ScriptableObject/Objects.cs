using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objects", menuName = "Scriptable Objects/Objects")]
public class Objects : ScriptableObject
{
    public GameObject prefab;
    public List<GameObject> meshes;
    public bool isUse;
}
