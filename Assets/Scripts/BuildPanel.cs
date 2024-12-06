using System.Collections.Generic;
using UnityEngine;

public class BuildPanel : MonoBehaviour
{
    [SerializeField] GameObject organelleItem;
    [SerializeField] Vector2 listStartLocation;
    [SerializeField] BuildOrgItem[] listItems;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void DisplayOrganelles(GameObject[] organelles)
    {
        List<string> orgList = new();
        int itemsUsed = 0;

        foreach (GameObject orgObject in organelles)
        {
            if (orgObject.TryGetComponent<Organelle>(out var orgComponent) && orgList.Exists(x => x.Contains(orgComponent.GetName())))
            {
                listItems[itemsUsed].DisplayOrganelle(orgComponent);
                itemsUsed++;
                if (itemsUsed >= listItems.Length)
                {
                    Debug.LogError("More organelles than list items in build panel. Only showing first " + listItems.Length + " Organelles.");
                    return;
                }
            }
        }
    }

    public void OnExitClick()
    {
        gameObject.SetActive(false);
    }
}
