using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganellePanel : MonoBehaviour
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
