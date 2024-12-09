using System.Collections.Generic;
using UnityEngine;

public enum CellEvolution { Bacteria, Plant, Animal }

public class Cell : MonoBehaviour
{
    [SerializeField] string cellName = "CELL";
    [SerializeField] string description = "DESCRIBE THIS ORGANELLE.";
    [SerializeField] CellEvolution evolution;
    [SerializeField] int cellLevel = 1;
    [SerializeField] float upgradeCost = 250;
    [SerializeField] float wasteLimit = 100f;
    [SerializeField] float maxWaste = 150f;
    [SerializeField] GameObject warningBubble;

    ResourceCounter resourceCounter;
    float totalWaste = 0;
    List<Organelle> organelles = new();
    Sprite icon;

    void Awake()
    {
        resourceCounter = FindFirstObjectByType<ResourceCounter>(FindObjectsInactive.Include);
        if (resourceCounter == null) // error check
        {
            Debug.LogError(cellName + "(game object name: " + gameObject.name + ")" + " was not able to find the resource counter");
        }
        icon = GetComponent<SpriteRenderer>().sprite;
    }

    void Start()
    {
        warningBubble.SetActive(false);
        FindOrganelles(gameObject.transform);
    }

    void FindOrganelles(Transform parent)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            if (child.TryGetComponent<Organelle>(out var org))
            {
                organelles.Add(org);
            }
            else if (child.childCount > 0)
            {
                FindOrganelles(child); // recurrsion to find any nested children organelles
            }
        }
    }

    public bool AddWaste(float amount)
    {
        if (amount == 0)
        {
            return true;
        }
        else if (amount > 0)
        {
            totalWaste += amount;
            if (totalWaste > maxWaste)
            {
                totalWaste = maxWaste;
                warningBubble.SetActive(true);
            }
            else
            {
                warningBubble.SetActive(false);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveWaste(float amount)
    {
        if (amount == 0)
        {
            return true;
        }
        else if (amount > 0)
        {
            totalWaste -= amount;
            if (totalWaste < 0)
            {
                totalWaste = 0;
            }

            if (totalWaste > maxWaste)
            {
                warningBubble.SetActive(true);
            }
            else
            {
                warningBubble.SetActive(false);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TryLevelUp()
    {
        float currentDNA = resourceCounter.GetResourceCount(ResourceType.DNA);
        float currentProtein = resourceCounter.GetResourceCount(ResourceType.Protein);
        float currentATP = resourceCounter.GetResourceCount(ResourceType.ATP);

        if (currentDNA >= upgradeCost && currentProtein >= upgradeCost && currentATP >= upgradeCost)
        {
            resourceCounter.SpendResources(ResourceType.DNA, upgradeCost);
            resourceCounter.SpendResources(ResourceType.Protein, upgradeCost);
            resourceCounter.SpendResources(ResourceType.ATP, upgradeCost);
            
            cellLevel += 1;
            upgradeCost *= 2;
        }
    }

    public string GetName() { return cellName; }
    public string GetDescription() { return description; }
    public int GetLevel() { return cellLevel; }
    public string GetUpgradeCost() { return upgradeCost.ToString(); }
    public bool IsAboveWasteLimit() { return totalWaste >= wasteLimit; }
    public float GetCurrentWaste() { return totalWaste; }
    public float GetWasteLimit() { return wasteLimit; }
    public float GetMaxWaste() { return maxWaste; }
    public List<Organelle> GetOrganelles() { return organelles; }
}
