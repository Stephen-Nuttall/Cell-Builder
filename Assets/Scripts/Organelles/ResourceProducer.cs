using System.Collections;
using UnityEngine;

public class ResourceProducer : MonoBehaviour
{
    [SerializeField] ResourceType resourceProduced;
    [SerializeField] float productionAmount = 10f;
    [SerializeField] float productionRate = 1f;  // in seconds.
    [SerializeField] float maxCapacity = 500f;
    [SerializeField] float ATPUseRate = 5f;
    [SerializeField] float wasteProductionAmount = 1f;
    [SerializeField] float maxWasteProductionRateMult = 0.25f;
    [SerializeField] bool running = true;
    [SerializeField] bool autoCollectResources = true;
    
    float productionUpgradeAmountMult = 0.25f;
    float storedAmount;

    ResourceCounter resourceCounter;
    Cell parentCell;

    void Start()
    {
        resourceCounter = FindFirstObjectByType<ResourceCounter>();
        parentCell = GetComponent<Organelle>().GetParentCell();

        StartCoroutine(ProductionTimer());
    }

    IEnumerator ProductionTimer()
    {
        while (running)
        {
            // If there's capacity for more resources to be stored and there's ATP that can be spent, generate resources.
            if ((storedAmount < maxCapacity) && resourceCounter.SpendResources(ResourceType.ATP, ATPUseRate))
            {
                // If the cell is above the waste limit, slow production. Otherwise, produce the normal amount of resources.
                if (parentCell.IsAboveWasteLimit())
                {
                    storedAmount += productionAmount * maxWasteProductionRateMult;
                }
                else
                {
                    storedAmount += productionAmount;
                }

                // If autocollect is enabled (it is by default), collect resources.
                if (autoCollectResources)
                {
                    CollectResources();
                }

                // If we've exceeded the maximum resource capacity for our cell, remove the extra amount off the top.
                if (storedAmount > maxCapacity)
                {
                    storedAmount = maxCapacity;
                }

                parentCell.AddWaste(wasteProductionAmount);
            }

            // wait until next cycle
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

    public void OnLevelUp()
    {
        productionAmount += productionAmount * productionUpgradeAmountMult;
    }

    public float GetStoredAmount() { return storedAmount; }
    public float GetMaxCapacity() { return maxCapacity; }
    public string GetProductionRate() { return productionAmount + " " + GetResourceType() + " every " + productionRate + " second(s)"; }
    public string GetNextProductionRate() { return "+" + productionAmount * productionUpgradeAmountMult + " " + GetResourceType() + " every " + productionRate + " second(s)"; }
    public bool IsAutoCollectEnabled() { return autoCollectResources; }
}
