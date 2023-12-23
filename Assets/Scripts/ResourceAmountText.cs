using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ResourceAmountText : MonoBehaviour
{
    [SerializeField] private Storage _storage;
    [SerializeField] private string _descriptionItem;

    private TMP_Text _amountText;

    private void OnEnable()
    {
        _storage.ResourceAmountChange += OnAmountChange;
    }

    private void OnDisable()
    {
        _storage.ResourceAmountChange -= OnAmountChange;
    }

    private void Awake()
    {
        _amountText = GetComponent<TMP_Text>();
    }

    private void OnAmountChange(int amount)
    {
        _amountText.text = _descriptionItem + amount.ToString();
    }
}
