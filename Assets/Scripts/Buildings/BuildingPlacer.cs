using UnityEngine;
using UnityEngine.InputSystem;
public class BuildingPlacer : MonoBehaviour
{
    public GameObject buildingPrefab;
    public Inventory inventory;
    public int buildingLayer;
    public GameObject previewPrefab;

    private GameObject selectedBuilding;
    private GameObject previewInstance;

    void Start()
    {
         buildingLayer = LayerMask.GetMask("Building");
         previewInstance = Instantiate(previewPrefab);
    }

    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

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
