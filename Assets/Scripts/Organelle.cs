using UnityEngine;

public class Organelle : MonoBehaviour
{
    [Header("Basic Info")]
    [SerializeField] string orgName = "ORGANELLE";
    [SerializeField] string description = "DESCRIBE THIS ORGANELLE.";
    [SerializeField] string buildDescription = "This is what this organelle does and why you should build more of it in your cell.";
    [SerializeField] protected int level = 1;
    [SerializeField] int cellLevelUnlockedAt = 1;
    [SerializeField] bool unlocked = true;
    [SerializeField] ResourceType buildResource = ResourceType.DNA;
    [SerializeField] ResourceType upgradeResource = ResourceType.Protein;
    [SerializeField] float buildCost = 100f;
    [SerializeField] float upgradeCost = 250f;
    [SerializeField] float upgradeCostLevelMult = 2f;
    OrganellePanel organellePanel;
    protected Cell parentCell;
    Sprite icon;

    void Awake()
    {
        organellePanel = FindFirstObjectByType<OrganellePanel>(FindObjectsInactive.Include);
        if (organellePanel == null) // error check
        {
            Debug.LogError(orgName + "(game object name: " + gameObject.name + ")" + " was not able to find the organelle panel");
        }

        parentCell = GetComponentInParent<Cell>(); // does matter if parent, or grandparent, or great grandparent, etc
        if (parentCell == null) // error check
        {
            Debug.LogError(orgName + "(game object name: " + gameObject.name + ")" + " was not able to find it's parent cell");
        }

        if (!unlocked)
        {
            gameObject.SetActive(false);
        }

        icon = GetComponent<SpriteRenderer>().sprite;
    }

    public void OrganelleClick()
    {
        organellePanel.gameObject.SetActive(true);
        organellePanel.DisplayOrganelleInfo(this);
    }

    public virtual void LevelUp()
    {
        level += 1;
        upgradeCost *= upgradeCostLevelMult;
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
}
