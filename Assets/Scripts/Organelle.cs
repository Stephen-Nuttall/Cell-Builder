using UnityEngine;

public class Organelle : MonoBehaviour
{
    [SerializeField] string orgName = "ORGANELLE";
    [SerializeField] string description = "DESCRIBE THIS ORGANELLE.";
    [SerializeField] protected int level = 1;
    OrganellePanel organellePanel;

    void Awake()
    {
        organellePanel = FindFirstObjectByType<OrganellePanel>(FindObjectsInactive.Include);
    }

    public void OrganelleClick()
    {
        organellePanel.gameObject.SetActive(true);
        organellePanel.DisplayOrganelleInfo(this);
    }

    public string GetName() { return orgName; }
    public string GetDescription() { return description; }
    public int GetLevel() { return level; }
}
