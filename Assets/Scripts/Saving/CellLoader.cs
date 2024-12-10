using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CellLoader : MonoBehaviour
{
    [SerializeField] GameObject bacteriaCellPrefab;
    [SerializeField] GameObject plantCellPrefab;
    [SerializeField] GameObject animalCellPrefab;
    [SerializeField] TMP_Text infoText;
    [SerializeField] GameObject startButton;
    MitosisHandler mitosisHandler;

    Dictionary<int, SerializedCellData> cellDataByID;

    void Awake()
    {
        mitosisHandler = FindFirstObjectByType<MitosisHandler>();
    }

    void Start()
    {
        int i;
        for (i = 0; FileReadWrite.CellDataExists(i); i++)
        {
            SerializedCellData data = FileReadWrite.ReadCellData(i);
            GameObject newCell;

            if (data.evolution == CellEvolution.Bacteria)
            {
                newCell = Instantiate(bacteriaCellPrefab, new Vector3(15 * i, 0, 0), Quaternion.identity);
            }
            else if (data.evolution == CellEvolution.Plant)
            {
                newCell = Instantiate(plantCellPrefab, new Vector3(15 * i, 0, 0), Quaternion.identity);
            }
            else
            {
                newCell = Instantiate(animalCellPrefab, new Vector3(15 * i, 0, 0), Quaternion.identity);
            }

            newCell.GetComponent<Cell>().LoadFromFile(data);
            mitosisHandler.IncrementCellCount();
        }

        if (i == 0)
        {
            infoText.enabled = true;
            startButton.SetActive(false);
            infoText.text = "To load the first save, please place resources.data and the .cell files that came with the zip in this directory:\n" + Application.persistentDataPath;
        }
        else
        {
            infoText.enabled = false;
            startButton.SetActive(true);
        }
    }
}
