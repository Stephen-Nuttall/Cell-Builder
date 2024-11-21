using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellPanel : MonoBehaviour
{
    [SerializeField] Button EvolveButton;
    bool evolveToggle = false;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnCellClick()
    {
        gameObject.SetActive(true);
    }

    public void OnEvolveClick()
    {
        TextMeshProUGUI buttonText = EvolveButton.GetComponentInChildren<TextMeshProUGUI>();
        Image buttonImage = EvolveButton.GetComponent<Image>();
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

}
