using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[RequireComponent(typeof(Scanner))]
[RequireComponent(typeof(Storage))]
public class MainBase : MonoBehaviour
{
    [SerializeField] private List<NPC> _npcs = new List<NPC>();

    public List<Item> FindItems => _findItems;

    private List<Item> _findItems = new List<Item>();
    private List<Transform> _spawnPoints;
    private Scanner _scanner;
    private Storage _storage;

    private void Start()
    {
        SpawnUnits();
    }

    private void Awake()
    {
        _spawnPoints = new List<Transform>(transform.childCount);

        foreach (Transform spawPoint in transform)
            _spawnPoints.Add(spawPoint);

        _scanner = GetComponent<Scanner>();
        _storage = GetComponent<Storage>();
    }

    public void AddItem(Item item)
    {
        item.SetDetectState(true);
        _findItems.Add(item);
    }

    private void Update()
    {
        if (_npcs.Count > 0 && _findItems.Count > 0)
        {
            if (_npcs[_npcs.Count - 1].Busy == false)
            {
                _npcs[_npcs.Count - 1].SetTask(_findItems[_findItems.Count - 1], transform);
                _findItems.Remove(_findItems[_findItems.Count - 1]);
                _npcs.Remove(_npcs[_npcs.Count - 1]);
            }
        }
    }

    private void SpawnUnits()
    {
        for (int i = 0; i < _npcs.Count; i++)
        {
            _npcs[i].transform.position = _spawnPoints[i].transform.position;
            _npcs[i].SetBusy(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out NPC npc))
        {
            if (npc.Busy)
            {
                npc.CompleteTask(_storage);
                _npcs.Add(npc);
                SpawnUnits();
            }
        }
    }
}
