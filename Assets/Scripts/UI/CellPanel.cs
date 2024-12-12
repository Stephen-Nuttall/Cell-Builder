using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellPanel : MonoBehaviour
{
    [SerializeField] MitosisHandler mitosisHandler;
    [SerializeField] TMP_Text cellName;
    [SerializeField] TMP_Text cellLevelText;
    [SerializeField] TMP_Text cellDescription;
    [SerializeField] Image cellIcon;
    [SerializeField] GameObject organellePanel;
    [SerializeField] Button evolveButton;
    [SerializeField] TMP_Text upgradeDNACostText;
    [SerializeField] TMP_Text upgradeProteinCostText;
    [SerializeField] TMP_Text upgradeATPCostText;
    [SerializeField] TextMeshProUGUI mitosisCostText;
    [SerializeField] TextMeshProUGUI mitosisDNACostText;
    [SerializeField] TextMeshProUGUI mitosisProteinCostText;
    [SerializeField] TextMeshProUGUI mitosisATPCostText;
    bool evolveToggle = false;
    BuildPanel buildPanel;
    Cell displayedCell;

    void Awake()
    {
        buildPanel = FindFirstObjectByType<BuildPanel>(FindObjectsInactive.Include);
    }

    void Start()
    {
        gameObject.SetActive(false);
        mitosisDNACostText.text = "DNA: " + mitosisHandler.GetMitosisDNACost();
        mitosisProteinCostText.text = "Protein: " + mitosisHandler.GetMitosisDNACost();
        mitosisATPCostText.text = "ATP: " + mitosisHandler.GetMitosisDNACost();
    }

    public void OnEvolveClick()
    {
        TextMeshProUGUI buttonText = evolveButton.GetComponentInChildren<TextMeshProUGUI>();
        Image buttonImage = evolveButton.GetComponent<Image>();
        evolveToggle = !evolveToggle;

        if (evolveToggle)
        {
            buttonText.text = "Evolve";
            buttonImage.color = Color.green;

            mitosisCostText.text = "Mitosis and Evolution Cost:";
            mitosisDNACostText.text = "DNA: " + (mitosisHandler.GetMitosisDNACost() + mitosisHandler.GetEvolveDNACost());
            mitosisProteinCostText.text = "Protein: " + (mitosisHandler.GetMitosisProteinCost() + mitosisHandler.GetEvolveProteinCost());
            mitosisATPCostText.text = "ATP: " + (mitosisHandler.GetMitosisATPCost() + mitosisHandler.GetEvolveATPCost());

            mitosisHandler.ToggleEvolve();
        }
        else
        {
            buttonText.text = "Evolve?";
            buttonImage.color = Color.red;

            mitosisCostText.text = "Mitosis Cost:";
            mitosisDNACostText.text = "DNA: " + mitosisHandler.GetMitosisDNACost();
            mitosisProteinCostText.text = "Protein: " + mitosisHandler.GetMitosisProteinCost();
            mitosisATPCostText.text = "ATP: " + mitosisHandler.GetMitosisATPCost();

            mitosisHandler.ToggleEvolve();
        }
    }

    public void OnUpgradeClick()
    {
        displayedCell.TryLevelUp();
        DisplayCellInfo(displayedCell);
    }

    public void OnExitClick()
    {
        gameObject.SetActive(false);
    }

    public void DisplayCellInfo(Cell cellInfo)
    {
        displayedCell = cellInfo;
        cellName.text = cellInfo.GetName();
        cellIcon.sprite = cellInfo.gameObject.GetComponent<SpriteRenderer>().sprite;

        cellLevelText.text = "Level: " + cellInfo.GetLevel();
        cellDescription.text = cellInfo.GetDescription();
        upgradeDNACostText.text = "DNA: " + cellInfo.GetUpgradeCost();
        upgradeProteinCostText.text = "Protein: " + cellInfo.GetUpgradeCost();
        upgradeATPCostText.text = "ATP: " + cellInfo.GetUpgradeCost();
    }

    public void DisplayBuildMenu()
    {
        buildPanel.gameObject.SetActive(true);
        buildPanel.DisplayOrganelles(displayedCell.GetOrganelles());
        gameObject.SetActive(false);
    }
}
