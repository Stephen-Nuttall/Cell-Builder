using UnityEngine;
using TMPro;
using System.Collections;

// This enum represents the different types of resources.
public enum ResourceType { DNA, Protein, ATP }

public class ResourceCounter : MonoBehaviour
{
    [SerializeField] TMP_Text DNAText;
    [SerializeField] TMP_Text ProteinText;
    [SerializeField] TMP_Text ATPText;

    [SerializeField] float totalDNA;
    [SerializeField] float totalProtein;
    [SerializeField] float totalATP;

    [SerializeField] float maxDNA;
    [SerializeField] float maxProtein;
    [SerializeField] float maxATP;

    void Awake()
    {
        LoadFromFile();
    }

    void Start()
    {
        UpdateResourceDisplay();
    }

    public void UpdateResourceDisplay()
    {
        DNAText.text = totalDNA.ToString();
        ProteinText.text = totalProtein.ToString();
        ATPText.text = totalATP.ToString();
    }

    public float GetResourceCount(ResourceType resourceType)
    {
        return resourceType switch
        {
            ResourceType.DNA => totalDNA,
            ResourceType.Protein => totalProtein,
            ResourceType.ATP => totalATP,
            _ => -1,
        };
    }

    public float GetResourceMax(ResourceType resourceType)
    {
        return resourceType switch
        {
            ResourceType.DNA => maxDNA,
            ResourceType.Protein => maxProtein,
            ResourceType.ATP => maxATP,
            _ => -1,
        };
    }

    public bool AddResources(ResourceType resourceType, float amountToAdd)
    {
        if (amountToAdd == 0)
        {
            return true;
        }
        else if (amountToAdd < 0)
        {
            return false;
        }
        else
        {
            switch (resourceType)
            {
                case ResourceType.DNA:
                    totalDNA += amountToAdd;
                    if (totalDNA > maxDNA)
                        totalDNA = maxDNA;
                    break;
                case ResourceType.Protein:
                    totalProtein += amountToAdd;
                    if (totalProtein > maxProtein)
                        totalProtein = maxProtein;
                    break;
                case ResourceType.ATP:
                    totalATP += amountToAdd;
                    if (totalATP > maxATP)
                        totalATP = maxATP;
                    break;
            }

            UpdateResourceDisplay();
            SaveToFile();
            return true;
        }
    }

    public bool SpendResources(ResourceType resourceType, float amountToSpend)
    {
        if (amountToSpend == 0)
        {
            return true;
        }
        else if (amountToSpend < 0)
        {
            return false;
        }
        else
        {
            switch (resourceType)
            {
                case ResourceType.DNA:
                    if (totalDNA - amountToSpend >= 0)
                    {
                        totalDNA -= amountToSpend;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case ResourceType.Protein:
                    if (totalProtein - amountToSpend >= 0)
                    {
                        totalProtein -= amountToSpend;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case ResourceType.ATP:
                    if (totalATP - amountToSpend >= 0)
                    {
                        totalATP -= amountToSpend;
                    }
                    else
                    {
                        return false;
                    }
                    break;
            }

            UpdateResourceDisplay();
            SaveToFile();
            return true;
        }
    }

    public bool IncreaseMaxResources(ResourceType resourceType, float amountToAdd)
    {
        if (amountToAdd <= 0)
        {
            return false;
        }
        else
        {
            switch (resourceType)
            {
                case ResourceType.DNA:
                    maxDNA += amountToAdd;
                    break;
                case ResourceType.Protein:
                    maxProtein += amountToAdd;
                    break;
                case ResourceType.ATP:
                    maxATP += amountToAdd;
                    break;
            }

            UpdateResourceDisplay();
            SaveToFile();
            return true;
        }
    }

    void LoadFromFile()
    {
        SerializedResourceData lastSaveData = FileReadWrite.ReadResourceData();

        if (lastSaveData != null)
        {
            totalDNA = lastSaveData.DNA;
            totalProtein = lastSaveData.protein;
            totalATP = lastSaveData.ATP;

            maxDNA = lastSaveData.maxDNA;
            maxProtein = lastSaveData.maxProtein;
            maxATP = lastSaveData.maxATP;
        }
    }

    void SaveToFile()
    {
        FileReadWrite.WriteResourceData(this);
    }
}
