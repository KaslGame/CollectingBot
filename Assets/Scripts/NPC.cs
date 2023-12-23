using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Transform _pocketPosition;
    [SerializeField] private float _speed;

    public bool Busy => _busy;

    private Transform _basePosition;
    private Vector3 _targetPosition;
    private Item _selectItem;
    private bool _busy = false;

    public void SetTask(Item item, Transform basePosition)
    {
        _selectItem = item;
        _basePosition = basePosition;
        _targetPosition = new Vector3(_selectItem.transform.position.x, transform.position.y, _selectItem.transform.position.z);
        _busy = true;
    }

    public void SetBusy(bool busy)
    {
        _busy = busy;
    }

    public void CompleteTask(Storage storage)
    {
        _busy = false;
        _targetPosition = transform.position;
        transform.position = _basePosition.position;
        _selectItem.Place(storage);
    }

    private void Update()
    {
        if (_busy && _targetPosition != null)
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Item item) && _busy)
        {
            if (item == _selectItem)
            {
                item.Take(_pocketPosition);
                _targetPosition = _basePosition.transform.position;
            }    
        }
    }
}
