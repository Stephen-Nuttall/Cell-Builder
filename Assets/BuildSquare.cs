using UnityEngine;

public class BuildSquare : MonoBehaviour
{
    GameObject BuildPanel;

    void Awake()
    {
        BuildPanel = GameObject.Find("Build Panel");
    }

    public void OnBuildClick()
    {
        BuildPanel.SetActive(true);
    }
}
