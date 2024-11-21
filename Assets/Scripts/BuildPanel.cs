using UnityEngine;

public class BuildPanel : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnExitClick()
    {
        gameObject.SetActive(false);
    }
}
