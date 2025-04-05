using System;
using UnityEngine;

public class ManageClick : MonoBehaviour
{
    [SerializeField] private int rewardMoney;
    [SerializeField] private GameObject parentUI;
    
    public delegate void OnInitAnimation(Transform parentTransform);
    public static event OnInitAnimation OnStartAnimation;

    private void Start()
    {
        GameObject foundParentUI = GameObject.FindGameObjectWithTag("UI");
        if (foundParentUI) parentUI = foundParentUI;
    }

    public void OnMeshClick()
    {
        if (GameManager.GameManagerInstance.GetActualPartMeshSelected() != gameObject) return;
        Debug.Log("OnMeshClick");
        if (parentUI)
        {
            GameObject rewardObj = ObjectPooler.Instance.GetPooledObject();
            OnStartAnimation?.Invoke(this.transform);
            rewardObj.transform.SetParent(parentUI.transform, worldPositionStays: true);
        }
        GameManager.GameManagerInstance.AddActualMoney(rewardMoney);
    }
}
