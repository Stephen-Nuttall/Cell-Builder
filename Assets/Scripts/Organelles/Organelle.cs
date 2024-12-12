using UnityEngine;
using UnityEngine.Events;

public class Organelle : MonoBehaviour
{
    [Header("Basic Info")]
    [SerializeField] string orgName = "ORGANELLE";
    [SerializeField] string description = "DESCRIBE THIS ORGANELLE.";
    [SerializeField] string buildDescription = "This is what this organelle does and why you should build more of it in your cell.";
    [SerializeField] protected int level = 1;
    [SerializeField] int cellLevelUnlockedAt = 1;
    [SerializeField] bool orgBuilt = true;  // will turn itself off if cell level is less than cellLevelUnlockedAt
    [SerializeField] ResourceType buildResource = ResourceType.DNA;
    [SerializeField] ResourceType upgradeResource = ResourceType.Protein;
    [SerializeField] float buildCost = 100f;
    [SerializeField] float upgradeCost = 250f;
    [SerializeField] float upgradeCostLevelMult = 2f;

    CellPanel cellPanel;
    OrganellePanel organellePanel;
    ResourceCounter resourceCounter;
    protected Cell parentCell;
    Sprite icon;
    int id;

    public UnityEvent onLevelUp;

    void Awake()
    {
        cellPanel = FindFirstObjectByType<CellPanel>(FindObjectsInactive.Include);
        if (cellPanel == null) // error check
        {
            Debug.LogError(orgName + "(game object name: " + gameObject.name + ")" + " was not able to find the cell panel");
        }
        organellePanel = FindFirstObjectByType<OrganellePanel>(FindObjectsInactive.Include);
        if (organellePanel == null) // error check
        {
            Debug.LogError(orgName + "(game object name: " + gameObject.name + ")" + " was not able to find the organelle panel");
        }

        resourceCounter = FindFirstObjectByType<ResourceCounter>(FindObjectsInactive.Include);
        if (resourceCounter == null) // error check
        {
            Debug.LogError(orgName + "(game object name: " + gameObject.name + ")" + " was not able to find the resource counter");
        }

        parentCell = GetComponentInParent<Cell>(); // does matter if parent, or grandparent, or great grandparent, etc
        if (parentCell == null) // error check
        {
            Debug.LogError(orgName + "(game object name: " + gameObject.name + ")" + " was not able to find it's parent cell");
        }

        if (parentCell.GetLevel() < cellLevelUnlockedAt || !orgBuilt)
        {
            orgBuilt = false;
            gameObject.SetActive(false);
        }

        icon = GetComponent<SpriteRenderer>().sprite;
    }

    void OnEnable()
    {
        orgBuilt = true;
    }

    public void OrganelleClick()
    {
        cellPanel.gameObject.SetActive(false);
        organellePanel.gameObject.SetActive(true);
        organellePanel.DisplayOrganelleInfo(this);
    }

    public void TryLevelUp()
    {
        float currentProtein = resourceCounter.GetResourceCount(ResourceType.Protein);
        if (currentProtein >= upgradeCost)
        {
            resourceCounter.SpendResources(ResourceType.Protein, upgradeCost);
            level += 1;
            upgradeCost *= upgradeCostLevelMult;
            onLevelUp?.Invoke();
        }
    }

    public void LoadFromFile(SerializedOrganelleData data)
    {
        if (id != data.id)
        {
            Debug.LogError("Failed to save data for " + orgName + " ID: " + id
            + ". Organelle ID does not match the given save data ID " + data.id
            + ". Organelle data not recovered and reverted to default.");
            return;
        }

        level = data.level;
        orgBuilt = data.built;

        if (parentCell.GetLevel() < cellLevelUnlockedAt || !orgBuilt)
        {
            orgBuilt = false;
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public string GetName() { return orgName; }
    public string GetDescription() { return description; }
    public string GetBuildDescription() { return buildDescription; }
    public int GetLevel() { return level; }
    public Sprite GetSprite() { return icon; }
    public int CellLevelUnlockedAt() { return cellLevelUnlockedAt; }
    public Cell GetParentCell() { return parentCell; }
    public ResourceType GetBuildResource() { return buildResource; }
    public ResourceType GetUpgradeResource() { return upgradeResource; }
    public float GetBuildCost() { return buildCost; }
    public float GetUpgradeCost() { return upgradeCost; }
    public bool IsBuilt() { return orgBuilt; }
    public int GetID() { return id; }
    public void SetID(int newID) { id = newID; }
}
