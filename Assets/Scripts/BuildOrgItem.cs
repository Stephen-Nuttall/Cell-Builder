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

    Organelle referenceOrganelle;

    public void DisplayOrganelle(Organelle org)
    {
        //
    }

    void ShowBuildInfo()
    {
        //
    }

    void ShowLockedInfo()
    {
        //
    }
}
