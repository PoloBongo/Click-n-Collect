using System.Collections.Generic;
using UnityEngine;

public class ListObject : MonoBehaviour
{
    [SerializeField] private List<Objects> objects;
    [SerializeField] private GameObject objectTarget;

    public GameObject SetEquippedObject()
    {
        foreach (var obj in objects)
        {
            if (!obj.isUse) continue;

            SetMeshesObjectTarget(obj.prefab);
            return obj.prefab;
        }

        return null;
    }

    public List<GameObject> GetMeshesObjectTarget()
    {
        foreach (var obj in objects)
        {
            if (obj.prefab != objectTarget) continue;

            return obj.meshes;
        }
        return null;
    }
    
    public void SetMeshesObjectTarget(GameObject _target)
    {
        objectTarget = _target;
    }
}
