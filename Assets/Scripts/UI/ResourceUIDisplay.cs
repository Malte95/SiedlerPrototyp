using UnityEngine;
using TMPro;

public class ResourceUIDisplay : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public Inventory inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displayText.text = inventory.GetInventoryText();
    }
}
