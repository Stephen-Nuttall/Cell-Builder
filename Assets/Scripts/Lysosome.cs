using System.Collections;
using UnityEngine;

public class Lysosome : Organelle
{
    [SerializeField] float wasteRemovalAmount = 1f;
    [SerializeField] float wasteRemovalRate = 0.25f;
    [SerializeField] float ATPUseRate = 1f;
    [SerializeField] bool running = true;
    ResourceCounter resourceCounter;

    void Start()
    {
        resourceCounter = FindFirstObjectByType<ResourceCounter>();
        StartCoroutine(WasteRemovalLoop());
    }

    IEnumerator WasteRemovalLoop()
    {
        while (running)
        {
            if (resourceCounter.SpendResources(ResourceType.ATP, ATPUseRate))
            {
                parentCell.RemoveWaste(wasteRemovalAmount);
            }

            yield return new WaitForSeconds(wasteRemovalRate);
        }
    }
}
