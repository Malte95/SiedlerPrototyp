using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{

    Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

    public void AddResource(ResourceType type, int amount)
    {
        if (resources.ContainsKey(type))
        {
            resources[type] += amount;
        }
        else
        {
            resources[type] = amount;
        }
    }

    public void PrintInventory()
    {
        foreach (var resource in resources)
        {
            Debug.Log($"Resource: {resource.Key.resourceName}  Amount: {resource.Value}");
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            PrintInventory();
        }

    }
}
