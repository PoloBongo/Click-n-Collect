using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;
    private float actualMoney;
    private GameObject actualPartMeshSelected;

    private void Awake()
    {
        if (GameManagerInstance != null && GameManagerInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            GameManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddActualMoney(int _amount)
    {
        actualMoney += _amount;
    }
    
    public void RemoveActualMoney(int _amount)
    {
        actualMoney -= _amount;
    }

    public void SetActualPartMeshSelected(GameObject _actualPartMeshSelected)
    {
        actualPartMeshSelected = _actualPartMeshSelected;
    }

    public GameObject GetActualPartMeshSelected()
    {
        return actualPartMeshSelected;
    }
}
