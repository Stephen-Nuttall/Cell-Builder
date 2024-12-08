using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellPanel : MonoBehaviour
{
    [SerializeField] MitosisHandler mitosisHandler;
    [SerializeField] TMP_Text cellName;
    [SerializeField] GameObject organellePanel;
    [SerializeField] Button evolveButton;
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
        }
        else
        {
            buttonText.text = "Evolve?";
            buttonImage.color = Color.red;
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
