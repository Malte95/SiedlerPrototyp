using UnityEngine;
using System.Collections.Generic;

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

    public string GetInventoryText()
    {
        string result = "";

        foreach (var resource in resources)
        {
            result += $"{resource.Key.resourceName} {resource.Value}\n";
        }

        return result;
    }

    public bool HasEnough(ResourceType type, int amount)
    {
        return resources.ContainsKey(type) && resources[type] >= amount;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
