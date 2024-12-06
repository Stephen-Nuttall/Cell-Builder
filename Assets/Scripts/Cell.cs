using UnityEngine;

public enum CellEvolution { Bacteria, Plant, Animal }

public class Cell : MonoBehaviour
{
    [SerializeField] string cellName = "CELL";
    [SerializeField] string description = "DESCRIBE THIS ORGANELLE.";
    [SerializeField] CellEvolution evolution;
    [SerializeField] int cellLevel = 1;

    [SerializeField] float wasteLimit = 100f;
    [SerializeField] float maxWaste = 150f;
    [SerializeField] GameObject warningBubble;
    float totalWaste = 0;

    [SerializeField] GameObject[] organelles;

    void Start()
    {
        warningBubble.SetActive(false);
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

    public string GetName() { return cellName; }
    public string GetDescription() { return description; }
    public int GetLevel() { return cellLevel; }
    public bool IsAboveWasteLimit() { return totalWaste >= wasteLimit; }
    public float GetCurrentWaste() { return totalWaste; }
    public float GetWasteLimit() { return wasteLimit; }
    public float GetMaxWaste() { return maxWaste; }
}
