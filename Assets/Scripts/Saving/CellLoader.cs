using System.Collections.Generic;
using UnityEngine;

public class CellLoader : MonoBehaviour
{
    [SerializeField] Cell startingCell;
    MitosisHandler mitosisHandler;

    Dictionary<int, SerializedCellData> cellDataByID;

    void Awake()
    {
        mitosisHandler = FindFirstObjectByType<MitosisHandler>();
    }

    void Start()
    {
        SerializedCellData data = FileReadWrite.ReadCellData(1);
        startingCell.LoadFromFile(data);
    }
}
