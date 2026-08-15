using UnityEngine;
using UnityEngine.InputSystem;
public class BuildingPlacer : MonoBehaviour
{
    public GameObject buildingPrefab;
    public Inventory inventory;

    void Start()
    {
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject newBuilding = Instantiate(buildingPrefab, hit.point, Quaternion.identity);
                ResourceProducer producer = newBuilding.GetComponent<ResourceProducer>();
                producer.inventory = inventory;
            }
        }
    }
}
