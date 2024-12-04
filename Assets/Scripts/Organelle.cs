using UnityEngine;

public class Organelle : MonoBehaviour
{
    [SerializeField] string orgName = "ORGANELLE";
    [SerializeField] string description = "DESCRIBE THIS ORGANELLE.";
    [SerializeField] protected int level = 1;
    OrganellePanel organellePanel;
    protected Cell parentCell;

    void Awake()
    {
        organellePanel = FindFirstObjectByType<OrganellePanel>(FindObjectsInactive.Include);
        parentCell = GetComponentInParent<Cell>(); // does matter if parent, or grandparent, or great grandparent, etc
    }

    public void OrganelleClick()
    {
        organellePanel.gameObject.SetActive(true);
        organellePanel.DisplayOrganelleInfo(this);
    }

    public virtual void LevelUp()
    {
        level += 1;
    }

    public string GetName() { return orgName; }
    public string GetDescription() { return description; }
    public int GetLevel() { return level; }
    public Cell GetParentCell() { return parentCell; }
}
