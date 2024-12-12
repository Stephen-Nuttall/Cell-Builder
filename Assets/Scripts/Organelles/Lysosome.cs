using System.Collections;
using UnityEngine;

public class Lysosome : MonoBehaviour
{
    [SerializeField] float wasteRemovalAmount = 1f;
    [SerializeField] float wasteRemovalRate = 0.25f;
    [SerializeField] float ATPUseRate = 1f;
    [SerializeField] bool running = true;

    float wasteUpgradeAmountMult = 0.1f;

    ResourceCounter resourceCounter;
    Cell parentCell;

    void Start()
    {
        resourceCounter = FindFirstObjectByType<ResourceCounter>();
        parentCell = GetComponent<Organelle>().GetParentCell();

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

    public void OnLevelUp()
    {
        wasteRemovalAmount += wasteRemovalAmount * wasteUpgradeAmountMult;
    }

    public string GetRemovalRate() { return "Removes " + wasteRemovalAmount + " Waste every " + wasteRemovalRate + " second(s)"; }
    public string GetNextRemovalRate() { return "+" + wasteRemovalAmount * wasteUpgradeAmountMult + " Waste every " + wasteRemovalRate + " second(s)"; }
}
