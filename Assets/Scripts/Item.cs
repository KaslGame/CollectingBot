using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected int AmountReward = 1;

    private bool _used;
    private bool _detected;

    public bool Used => _used;
    public bool Detected => _detected;

    public void Take(Transform pocketTransform)
    {
        transform.position = pocketTransform.position;
        transform.parent = pocketTransform;
    }

    public void SetDetectState(bool state)
    {
        _detected = state;
    }

    public void SetUsedStatus(bool used)
    {
        _used = used;
    }

    public void Place(Storage storage)
    {
        storage.AddResource(AmountReward);
        Destroy(gameObject);
    }
}
