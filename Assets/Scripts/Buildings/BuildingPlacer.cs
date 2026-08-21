using UnityEngine;
using UnityEngine.InputSystem;
public class BuildingPlacer : MonoBehaviour
{
    public GameObject buildingPrefab1;
    public ResourceType cost1Type;
    public int cost1Amount;

    public GameObject buildingPrefab2;
    public ResourceType cost2Type;
    public int cost2Amount;

    public Inventory inventory;
    public int buildingLayer;
    public GameObject previewPrefab;

    private GameObject selectedBuilding;
    private GameObject previewInstance;

    private GameObject currentPrefab;
    private ResourceType currentCostType;
    private int currentCostAmount;

    void Start()
    {
        buildingLayer = LayerMask.GetMask("Building");
        previewInstance = Instantiate(previewPrefab);
        currentPrefab = buildingPrefab1;
        currentCostType = cost1Type;
        currentCostAmount = cost1Amount;

    }

    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentPrefab = buildingPrefab1;
            currentCostType = cost1Type;
            currentCostAmount = cost1Amount;
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentPrefab = buildingPrefab2;
            currentCostType = cost2Type;
            currentCostAmount = cost2Amount;
        }

        if (Physics.Raycast(ray, out hit))
        {
            previewInstance.transform.position = hit.point;
            Renderer renderer = previewInstance.GetComponent<Renderer>();

            if (Physics.CheckSphere(hit.point, 0.5f, buildingLayer))
            {
                renderer.material.color = Color.red;
            }
            else
            {
                renderer.material.color = Color.green;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Building"))
                {
                    if (selectedBuilding != null)
                    {
                        selectedBuilding.GetComponent<Renderer>().material.color = Color.green;
                    }
                    selectedBuilding = hit.collider.gameObject;
                    selectedBuilding.GetComponent<Renderer>().material.color = Color.yellow;
                }
                else if (Physics.CheckSphere(hit.point, 0.5f , buildingLayer))
                {
                    Debug.Log("Can't place building here.");
                }
                else
                {
                    if (inventory.HasEnough(currentCostType, currentCostAmount))
                    {
                        GameObject newBuilding = Instantiate(currentPrefab, hit.point, Quaternion.identity);
                        ResourceProducer producer = newBuilding.GetComponent<ResourceProducer>();
                        if (producer != null)
                        {
                            producer.inventory = inventory;
                        }
                        ResourceConverter converter = newBuilding.GetComponent<ResourceConverter>();
                        if (converter != null)
                        {
                            converter.inventory = inventory;
                        }
                        inventory.AddResource(currentCostType, -currentCostAmount);
                    }
                    else
                    {
                        Debug.Log("Not enough resources");
                    }
                    

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
