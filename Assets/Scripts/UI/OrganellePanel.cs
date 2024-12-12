using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrganellePanel : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] Image orgIcon;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text resourceText;
    [SerializeField] TMP_Text productionText;
    [SerializeField] TMP_Text upgradeText;
    [SerializeField] TMP_Text upgradeProductionText;
    [SerializeField] Button collectButton;


    Organelle displayedOrganelle;
    CellPanel cellPanel;

    void Awake()
    {
        cellPanel = FindFirstObjectByType<CellPanel>(FindObjectsInactive.Include);
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (displayedOrganelle.gameObject.TryGetComponent<ResourceProducer>(out var producer))
        {
            resourceText.text = producer.GetResourceType(true) + ": " + producer.GetStoredAmount() + "/" + producer.GetMaxCapacity();
        }
    }

    public void OnExitClick()
    {
        collectButton.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
    }

    public void DisplayCellMenu()
    {
        cellPanel.gameObject.SetActive(true);
        cellPanel.DisplayCellInfo(displayedOrganelle.GetParentCell());
        gameObject.SetActive(false);
    }

    public void OnUpgradeClick()
    {
        displayedOrganelle.TryLevelUp();
        DisplayOrganelleInfo(displayedOrganelle);
    }

    public void DisplayOrganelleInfo(Organelle newOrg)
    {
        displayedOrganelle = newOrg;

        nameText.text = newOrg.GetName();
        descriptionText.text = newOrg.GetDescription();
        orgIcon.sprite = newOrg.gameObject.GetComponent<SpriteRenderer>().sprite;
        levelText.text = "Level: " + newOrg.GetLevel().ToString();
        upgradeText.text = "Protein: " + newOrg.GetUpgradeCost().ToString();

        if (newOrg.gameObject.TryGetComponent<ResourceProducer>(out var producer))
        {
            resourceText.enabled = true;
            productionText.enabled = true;
            upgradeProductionText.enabled = true;

            productionText.text = producer.GetProductionRate();
            upgradeProductionText.text = producer.GetNextProductionRate();

            if (!producer.IsAutoCollectEnabled())
            {
                resourceText.text = producer.GetResourceType(true) + ": " + producer.GetStoredAmount() + "/" + producer.GetMaxCapacity();

                collectButton.gameObject.SetActive(true);
                collectButton.onClick.AddListener(producer.CollectResources);
            }
            else
            {
                resourceText.text = producer.GetResourceType(true);
                collectButton.gameObject.SetActive(false);
            }
        }
        else if (newOrg.gameObject.TryGetComponent<Lysosome>(out var plantLysosome) && newOrg.gameObject.TryGetComponent<Vacuole>(out var plantVacuole))
        {
            collectButton.gameObject.SetActive(false);
            productionText.enabled = true;
            resourceText.enabled = true;
            upgradeProductionText.enabled = true;

            resourceText.text = plantLysosome.GetRemovalRate();
            productionText.text = plantVacuole.GetStorageAmount();
            upgradeProductionText.text = plantLysosome.GetNextRemovalRate() + "\n" + plantVacuole.GetNextStorageAmount();
        }
        else if (newOrg.gameObject.TryGetComponent<Lysosome>(out var lysosome))
        {
            collectButton.gameObject.SetActive(false);
            productionText.enabled = false;
            resourceText.enabled = true;
            upgradeProductionText.enabled = true;

            resourceText.text = lysosome.GetRemovalRate();
            upgradeProductionText.text = lysosome.GetNextRemovalRate();
        }
        else if (newOrg.gameObject.TryGetComponent<Vacuole>(out var vacuole))
        {
            collectButton.gameObject.SetActive(false);
            productionText.enabled = false;
            resourceText.enabled = true;
            upgradeProductionText.enabled = true;

            resourceText.text = vacuole.GetStorageAmount();
            upgradeProductionText.text = vacuole.GetNextStorageAmount();
        }
        else
        {
            resourceText.enabled = false;
            productionText.enabled = false;
            upgradeProductionText.enabled = false;
            collectButton.gameObject.SetActive(false);
        }
    }
}