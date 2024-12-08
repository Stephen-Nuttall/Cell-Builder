using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellPanel : MonoBehaviour
{
    [SerializeField] MitosisHandler mitosisHandler;
    [SerializeField] TMP_Text cellName;
    [SerializeField] GameObject organellePanel;
    [SerializeField] Button evolveButton;
    [SerializeField] TextMeshProUGUI MitosisCostText;
    [SerializeField] TextMeshProUGUI  mitosisDNACostText;
    [SerializeField] TextMeshProUGUI  mitosisProteinCostText;
    [SerializeField] TextMeshProUGUI  mitosisATPCostText;
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

            MitosisCostText.text = "Mitosis and Evolution Cost:";
            mitosisDNACostText.text = "DNA: " + (mitosisHandler.GetMitosisDNACost() + mitosisHandler.GetEvolveDNACost());
            mitosisProteinCostText.text = "Protein: " + (mitosisHandler.GetMitosisProteinCost() + mitosisHandler.GetEvolveProteinCost());
            mitosisATPCostText.text = "ATP: " + (mitosisHandler.GetMitosisATPCost() + mitosisHandler.GetEvolveATPCost());

            mitosisHandler.ToggleEvolve();
        }
        else
        {
            buttonText.text = "Evolve?";
            buttonImage.color = Color.red;

            MitosisCostText.text = "Mitosis Cost:";
            mitosisDNACostText.text = "DNA: " + mitosisHandler.GetMitosisDNACost();
            mitosisProteinCostText.text = "Protein: " + mitosisHandler.GetMitosisProteinCost();
            mitosisATPCostText.text = "ATP: " + mitosisHandler.GetMitosisATPCost();

            mitosisHandler.ToggleEvolve();
        }
    }

    public void OnExitClick()
    {
        gameObject.SetActive(false);
    }

    public void DisplayCellInfo(Cell cellInfo)
    {
        displayedCell = cellInfo;
        cellName.text = cellInfo.GetName();
    }

    public void DisplayBuildMenu()
    {
        buildPanel.gameObject.SetActive(true);
        buildPanel.DisplayOrganelles(displayedCell.GetOrganelles());
        gameObject.SetActive(false);
    }
}
