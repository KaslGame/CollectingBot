using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Storage : MonoBehaviour
{
    public event UnityAction<int> ResourceAmountChange;

    private int _resourceAmount;

    public void AddResource(int amount)
    {
        _resourceAmount += amount;
        ResourceAmountChange?.Invoke(_resourceAmount);
    }
}
