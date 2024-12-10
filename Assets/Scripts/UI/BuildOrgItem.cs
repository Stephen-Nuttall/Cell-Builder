using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildOrgItem : MonoBehaviour
{
    [SerializeField] Image icon;

    [SerializeField] GameObject buildInfoPanel;
    [SerializeField] GameObject DNAImage;
    [SerializeField] GameObject proteinImage;
    [SerializeField] GameObject ATPImage;
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text itemDescriptionText;
    [SerializeField] Button buildButton;

    [SerializeField] GameObject lockedInfoPanel;
    [SerializeField] TMP_Text lockedHeadingText;
    [SerializeField] TMP_Text lockedDescriptionText;

    [SerializeField] string tooLowLevelHeading = "LOCKED";
    [SerializeField] string tooLowLevelDescription = "Your cell is not high enough level to build this organelle. Your cell must be at least level ";

    [SerializeField] string upgradeCellMemHeading = "LOCKED";
    [SerializeField] string upgradeCellMemDescription = "Your cell membrane can't hold this many organelles! Upgrade your cell membrane to build this organelle.";

    [SerializeField] string purchasedHeading = "PURCHASED";
    [SerializeField] string purchasedDescription = "You have already built this organelle!";

    [SerializeField] Image orgIcon;

    Organelle referenceOrganelle;
    ResourceCounter resourceCounter;

    void Start()
    {
        resourceCounter = FindFirstObjectByType<ResourceCounter>();
    }

    void OnDisable()
    {
        buildButton.onClick.RemoveAllListeners();
    }

    void Update()
    {
        DisplayOrganelle(referenceOrganelle);
    }

    public void DisplayOrganelle(Organelle org)
    {
        referenceOrganelle = org;

        if (org.GetParentCell().GetMaxOrganelles() <= org.GetParentCell().GetNumOrganellesBuilt())
        {
            ShowUpgradeCellMemInfo();
        }
        else if (org.CellLevelUnlockedAt() > org.GetParentCell().GetLevel())
        {
            ShowTooLowLevelInfo(org.CellLevelUnlockedAt());
        }
        else if (org.gameObject.activeInHierarchy)
        {
            ShowPurchasedInfo();
        }
        else
        {
            ShowBuildInfo();
        }

        orgIcon.sprite = org.GetSprite();
    }

    void ShowBuildInfo()
    {
        buildInfoPanel.SetActive(true);
        lockedInfoPanel.SetActive(false);

        DNAImage.SetActive(false);
        proteinImage.SetActive(false);
        ATPImage.SetActive(false);

        ResourceType buildResource = referenceOrganelle.GetBuildResource();
        switch (buildResource)
        {
            case ResourceType.DNA:
                DNAImage.SetActive(true);
                costText.text = "DNA: ";
                break;
            case ResourceType.Protein:
                proteinImage.SetActive(true);
                costText.text = "Protein: ";
                break;
            case ResourceType.ATP:
                ATPImage.SetActive(true);
                costText.text = "ATP: ";
                break;
        }

        costText.text += referenceOrganelle.GetBuildCost().ToString();
        itemDescriptionText.text = referenceOrganelle.GetBuildDescription();

        buildButton.onClick.AddListener(OnBuildClick);
    }

    public void OnBuildClick()
    {
        if (resourceCounter.SpendResources(referenceOrganelle.GetBuildResource(), referenceOrganelle.GetBuildCost()))
        {
            referenceOrganelle.gameObject.SetActive(true);
            ShowPurchasedInfo();
        }
    }

    void ShowTooLowLevelInfo(int requiredLevel)
    {
        buildInfoPanel.SetActive(false);
        lockedInfoPanel.SetActive(true);

        lockedHeadingText.text = tooLowLevelHeading;
        lockedDescriptionText.text = tooLowLevelDescription + requiredLevel;
    }

    void ShowUpgradeCellMemInfo()
    {
        buildInfoPanel.SetActive(false);
        lockedInfoPanel.SetActive(true);

        lockedHeadingText.text = upgradeCellMemHeading;
        lockedDescriptionText.text = upgradeCellMemDescription;
    }

    void ShowPurchasedInfo()
    {
        buildInfoPanel.SetActive(false);
        lockedInfoPanel.SetActive(true);

        lockedHeadingText.text = purchasedHeading;
        lockedDescriptionText.text = purchasedDescription;
    }
}
