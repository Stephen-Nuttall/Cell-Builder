using UnityEngine;

public class Organelle : MonoBehaviour
{
    GameObject OrganellePanel;

    void Awake()
    {
        OrganellePanel = GameObject.Find("Organelle Panel");
    }

    public void OrganelleClick()
    {
        OrganellePanel.SetActive(true);
    }
}
