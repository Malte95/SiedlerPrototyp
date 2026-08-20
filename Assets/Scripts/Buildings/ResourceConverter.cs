using UnityEngine;

public class ResourceConverter : MonoBehaviour
{
    public ResourceType inputResource;
    public int inputAmount;
    public ResourceType outputResource;
    public int outputAmount;
    public Inventory inventory;
    public float productionInterval;

    private float timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= productionInterval)
        {
            if (inventory.HasEnough(inputResource, inputAmount))
            {
                inventory.AddResource(inputResource, -inputAmount);
                inventory.AddResource(outputResource, outputAmount);
            }
            else
            {
                Debug.Log($"Can't produce {outputResource}");
            }

            timer = 0;
        }
        
    }
}
