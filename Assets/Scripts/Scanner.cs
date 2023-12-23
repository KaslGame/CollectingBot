using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(MainBase))]
public class Scanner : MonoBehaviour
{
    [SerializeField] private float _timeForScan;

    private SphereCollider _sphereCollider;
    private MainBase _mainBase;
    private bool _isTimerStart;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _mainBase = GetComponent<MainBase>();
        _sphereCollider.enabled = false;
    }

    public void Scan()
    {
        if(_isTimerStart == false)
            StartCoroutine(StartScan(_timeForScan));
    }

    private IEnumerator StartScan(float time)
    {
        _sphereCollider.enabled = true;
        _isTimerStart = true;
        yield return new WaitForSeconds(time);
        _sphereCollider.enabled = false;
        _isTimerStart = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
            if (!_mainBase.FindItems.Contains(item) && item.Detected == false)
                _mainBase.AddItem(item);
    }
}
