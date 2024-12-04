using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellPanel : MonoBehaviour
{
    [SerializeField] GameObject organellePanel;
    [SerializeField] Button evolveButton;
    bool evolveToggle = false;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnCellClick()
    {
        gameObject.SetActive(true);
        organellePanel.SetActive(false);
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

    public void DisplayCellInfo()
    {
        //
    }
}
