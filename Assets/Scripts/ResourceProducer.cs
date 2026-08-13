using UnityEngine;

public class ResourceProducer : MonoBehaviour
{
    public ResourceType resourceToProduce;
    public float productionInterval;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= productionInterval)
        {
            Debug.Log(resourceToProduce.resourceName + " was produced");
            timer = 0;
        }
        
    }
}
