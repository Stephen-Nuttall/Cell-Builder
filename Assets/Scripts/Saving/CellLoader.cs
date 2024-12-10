using System.Collections.Generic;
using UnityEngine;

public class CellLoader : MonoBehaviour
{
    [SerializeField] GameObject bacteriaCellPrefab;
    [SerializeField] GameObject plantCellPrefab;
    [SerializeField] GameObject animalCellPrefab;
    MitosisHandler mitosisHandler;

    Dictionary<int, SerializedCellData> cellDataByID;

    void Awake()
    {
        mitosisHandler = FindFirstObjectByType<MitosisHandler>();
    }

    void Start()
    {
        for (int i = 0; FileReadWrite.CellDataExists(i); i++)
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
    }
}
