using System.Collections;
using UnityEngine;

public class ResourceProducer : Organelle
{
    [SerializeField] ResourceType resourceProduced;
    [SerializeField] float productionAmount = 10f;
    [SerializeField] float productionRate = 1f;
    [SerializeField] float maxCapacity = 500f;
    [SerializeField] float ATPUseRate = 5f;
    [SerializeField] bool running = true;
    [SerializeField] bool autoCollectResources = true;
    float storedAmount;
    ResourceCounter resourceCounter;

    void Start()
    {
        resourceCounter = FindFirstObjectByType<ResourceCounter>();
        StartCoroutine(ProductionTimer());
    }

    IEnumerator ProductionTimer()
    {
        while (running)
        {
            if ((storedAmount + productionAmount <= maxCapacity) && resourceCounter.SpendResources(ResourceType.ATP, ATPUseRate))
            {
                storedAmount += productionAmount;
                if (autoCollectResources)
                {
                    CollectResources();
                }
            }
            yield return new WaitForSeconds(productionRate);
        }
    }

    public void CollectResources()
    {
        resourceCounter.AddResources(resourceProduced, storedAmount);
        storedAmount = 0;
    }

    public string GetResourceType()
    {
        return resourceProduced switch
        {
            ResourceType.DNA => "DNA",
            ResourceType.Protein => "protein",
            ResourceType.ATP => "ATP",
            _ => "ERROR",
        };
    }

    public string GetResourceType(bool capitalizeFirstLetter)
    {
        if (capitalizeFirstLetter)
        {
            return resourceProduced switch
            {
                ResourceType.DNA => "DNA",
                ResourceType.Protein => "Protein",
                ResourceType.ATP => "ATP",
                _ => "ERROR",
            };
        }
        else
        {
            return resourceProduced switch
            {
                ResourceType.DNA => "DNA",
                ResourceType.Protein => "protein",
                ResourceType.ATP => "ATP",
                _ => "ERROR",
            };
        }
    }

    public float GetStoredAmount() { return storedAmount; }
    public float GetMaxCapacity() { return maxCapacity; }
    public string GetProductionRate() { return productionAmount + " " + GetResourceType() + " every " + productionRate + " second(s)"; }
    public bool IsAutoCollectEnabled() { return autoCollectResources; }
}
