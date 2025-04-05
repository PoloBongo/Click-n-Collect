using UnityEngine;
using System.Collections.Generic;

public class MeshRarityManager : MonoBehaviour
{
    private static readonly int Scaling = Animator.StringToHash("Scaling");
    public float interval;
    private float timer = 0f;

    private RaretyPartObject selectedMesh = null;
    private List<RaretyPartObject> allMeshParts;
    private Vector3 saveScale;

    private void Start()
    {
        interval = Random.Range(1.5f, 7f);
        allMeshParts = new List<RaretyPartObject>(FindObjectsByType<RaretyPartObject>(FindObjectsSortMode.InstanceID));
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            ResetMesh();
            interval = Random.Range(1.5f, 7f);
            timer = 0f;
            HighlightRandomMesh();
        }
    }

    private void ResetMesh()
    {
        if (!selectedMesh) return;

        selectedMesh.transform.localScale = saveScale;
        selectedMesh.GetComponent<Animator>().speed = 0;
        selectedMesh.GetComponent<Animator>().SetBool(Scaling, false);
        selectedMesh.GetComponent<MeshRenderer>().sharedMaterial = selectedMesh.originalMaterial;
    }

    private void HighlightRandomMesh()
    {
        if (allMeshParts.Count == 0) return;

        selectedMesh = GetRandomMeshByRarity();
        saveScale = selectedMesh.transform.localScale;
        GameManager.GameManagerInstance.SetActualPartMeshSelected(selectedMesh.gameObject);
        if (!selectedMesh) return;

        ApplyMaterial(selectedMesh);
        PlayAnimation(selectedMesh);
    }

    private RaretyPartObject GetRandomMeshByRarity()
    {
        float totalProbability = 0f;
        foreach (var part in allMeshParts)
        {
            totalProbability += GetRarityProbability(part);
        }

        float randomValue = Random.Range(0, totalProbability);
        float cumulative = 0f;

        foreach (var part in allMeshParts)
        {
            cumulative += GetRarityProbability(part);
            if (randomValue <= cumulative)
                return part;
        }

        return null;
    }

    private float GetRarityProbability(RaretyPartObject _raretyPartObject)
    {
        if(!_raretyPartObject) return 0;
        return _raretyPartObject.probability;
    }

    private void ApplyMaterial(RaretyPartObject meshPart)
    {
        if (!meshPart) return;
        MeshRenderer meshRenderer = meshPart.GetComponent<MeshRenderer>();
        if (!meshRenderer) return;

        Material matInstance = new Material(meshPart.material);
        meshRenderer.sharedMaterial = matInstance;

        Debug.Log($"Highlight {meshPart.name} with {meshPart.rarety}");
    }
    
    private void PlayAnimation(RaretyPartObject meshPart)
    {
        if (!meshPart) return;
        Animator animatiorMesh = meshPart.GetComponent<Animator>();
        animatiorMesh.speed = 1f;
        if (!animatiorMesh) return;
        
        animatiorMesh.SetBool(Scaling, true);
    }
}
