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
    List<Organelle> organelles = new();
    CellMembrane cellMembrane;
    Sprite icon;

    float totalWaste = 0;
    int cellNum;

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
    }

    void OnDisable()
    {
        SaveToFile();
    }

    void OnApplicationQuit()
    {
        SaveToFile();
    }

    void FindOrganelles(Transform parent)
    {
        organelles = new();

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            if (child.TryGetComponent<Organelle>(out var org))
            {
                organelles.Add(org);
                org.SetID(i);
                if (child.TryGetComponent<CellMembrane>(out var cellMem))
                {
                    cellMembrane = cellMem;
                }
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

            SaveToFile();
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

            SaveToFile();
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

    public void LoadFromFile(SerializedCellData data)
    {
        if (data == null)
        {
            Debug.LogError("Attempted to load save for this cell, but save data was null. Cell will not be restored.");
            return;
        }

        cellNum = data.id;
        cellLevel = data.level;
        totalWaste = data.waste;

        if (data.orgList == null)
        {
            Debug.LogError("Attempted to load saves for the organelles in cell #" + cellNum +
            ", but no organelle saves were found. Organelles in this cell will not be restored.");
            return;
        }

        FindOrganelles(gameObject.transform);

        for (int i = 0; i < data.orgList.Count; i++)
        {
            if (i >= organelles.Count)
            {
                Debug.LogError("Attempted to load save for organelle #" + i + " in cell #" + cellNum
                + " but that index is out of range of the organelle list for this cell.");
            }
            else if (organelles[i] == null)
            {
                Debug.LogError("Attempted to load save for organelle #" + i + " in cell #" + cellNum + " but it was not found.");
            }
            else if (data.orgList[i] == null)
            {
                Debug.LogError("Attempted to load save for organelle #" + i + " in cell #" + cellNum + " but no save for it was found.");
            }
            else
            {
                organelles[i].LoadFromFile(data.orgList[i]);
            }
        }
    }

    void SaveToFile()
    {
        FileReadWrite.WriteCellData(this);
    }

    public int GetMaxOrganelles()
    {
        return cellMembrane.GetMaxOrg() + cellMembrane.GetIncreasePerLevel() * (cellMembrane.gameObject.GetComponent<Organelle>().GetLevel() - 1);
    }

    public int GetNumOrganellesBuilt()
    {
        int count = 0;
        foreach (Organelle org in organelles)
        {
            if (org.IsBuilt())
            {
                count++;
            }
        }

        return count;
    }

    public void SetCellNumToLatest() { cellNum = FindFirstObjectByType<MitosisHandler>().GetCellCount(); }

    public string GetName() { return cellName; }
    public string GetDescription() { return description; }
    public int GetLevel() { return cellLevel; }
    public string GetUpgradeCost() { return upgradeCost.ToString(); }
    public CellEvolution GetEvolution() { return evolution; }
    public bool IsAboveWasteLimit() { return totalWaste >= wasteLimit; }
    public float GetCurrentWaste() { return totalWaste; }
    public float GetWasteLimit() { return wasteLimit; }
    public float GetMaxWaste() { return maxWaste; }
    public List<Organelle> GetOrganelles() { return organelles; }
    public int GetID() { return cellNum; }
}
