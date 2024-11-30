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
    [SerializeField] Button collectButton;

    Organelle displayedOrganelle;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (displayedOrganelle.gameObject.TryGetComponent<ResourceProducer>(out var producer))
        {
            resourceText.text = producer.GetResourceType() + ": " + producer.GetStoredAmount() + "/" + producer.GetMaxCapacity();
        }
    }

    public void OnExitClick()
    {
        collectButton.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
    }

    public void DisplayOrganelleInfo(Organelle newOrg)
    {
        displayedOrganelle = newOrg;

        nameText.text = newOrg.GetName();
        descriptionText.text = newOrg.GetDescription();
        orgIcon.sprite = newOrg.gameObject.GetComponent<SpriteRenderer>().sprite;
        levelText.text = "Level: " + newOrg.GetLevel().ToString();

        if (newOrg.gameObject.TryGetComponent<ResourceProducer>(out var producer))
        {
            resourceText.enabled = true;
            productionText.enabled = true;
            collectButton.gameObject.SetActive(true);

            resourceText.text = producer.GetResourceType() + ": " + producer.GetStoredAmount() + "/" + producer.GetMaxCapacity();
            productionText.text = producer.GetProductionRate();
            collectButton.onClick.AddListener(producer.CollectResources);
        }
        else
        {
            resourceText.enabled = false;
            productionText.enabled = false;
            collectButton.gameObject.SetActive(false);
        }
    }
}