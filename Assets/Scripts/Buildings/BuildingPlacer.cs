using UnityEngine;
using UnityEngine.InputSystem;
public class BuildingPlacer : MonoBehaviour
{
    public GameObject buildingPrefab;
    public Inventory inventory;
    public int buildingLayer;

    private GameObject selectedBuilding;

    void Start()
    {
         buildingLayer = LayerMask.GetMask("Building");
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
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Building"))
                {
                    selectedBuilding = hit.collider.gameObject;
                }
                else if (Physics.CheckSphere(hit.point, 0.5f , buildingLayer))
                {
                    Debug.Log("Can't place building here.");
                }
                else
                {
                    GameObject newBuilding = Instantiate(buildingPrefab, hit.point, Quaternion.identity);
                    ResourceProducer producer = newBuilding.GetComponent<ResourceProducer>();
                    producer.inventory = inventory;

                }
            }
            
        }
        if (Keyboard.current.backspaceKey.wasPressedThisFrame && selectedBuilding != null)
        {
            Destroy(selectedBuilding);
            selectedBuilding = null;
        }
    }
}
