using System.Collections.Generic;
using UnityEngine;

public class BuildPanel : MonoBehaviour
{
    [SerializeField] BuildOrgItem[] listItems;
    List<Organelle> organelleList = new();
    int currentPage = 0;

    void Start()
    {
        HideAllItems();
        gameObject.SetActive(false);
    }

    public void DisplayOrganelles(List<Organelle> organelles)
    {
        organelleList = new();

        foreach (Organelle org in organelles)
        {
            if (!org.gameObject.activeInHierarchy)
            {
                organelleList.Add(org);
            }
        }

        DisplayOrganellePage(0);
    }

    bool DisplayOrganellePage(int pageNum)
    {
        HideAllItems();

        if (pageNum < 0 || pageNum > organelleList.Count)
        {
            return false;
        }

        for (int i = 0; i < listItems.Length && i + (pageNum * listItems.Length) < organelleList.Count; i++)
        {
            listItems[i].gameObject.SetActive(true);
            listItems[i].DisplayOrganelle(organelleList[i + (pageNum * listItems.Length)]);
        }

        currentPage = pageNum;
        return true;
    }

    void HideAllItems()
    {
        foreach (BuildOrgItem listItem in listItems)
        {
            listItem.gameObject.SetActive(false);
        }
    }

    public void DisplayNextPage()
    {
        DisplayOrganellePage(currentPage + 1);
    }

    public void DisplayPreviousPage()
    {
        DisplayOrganellePage(currentPage - 1);
    }

    public void OnExitClick()
    {
        HideAllItems();
        currentPage = 0;
        gameObject.SetActive(false);
    }
}
