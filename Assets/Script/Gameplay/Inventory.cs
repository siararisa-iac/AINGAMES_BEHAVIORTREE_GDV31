using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<string> items;

    public bool ContainsItem(string id)
    {
        return items.Contains(id);
    }

    public NodeState CheckInventory(string id)
    {
        return items.Contains(id) ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
