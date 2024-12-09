using System.Collections.Generic;

[System.Serializable]
public class SerializedCellData
{
    public int id { get; private set; }
    public CellEvolution evolution { get; private set; }
    public int level { get; private set; }
    public float waste { get; private set; }
    public List<SerializedOrganelleData> orgList { get; private set; } = new();

    public SerializedCellData(Cell cell)
    {
        id = cell.GetID();
        evolution = cell.GetEvolution();
        level = cell.GetLevel();
        waste = cell.GetCurrentWaste();

        foreach (Organelle org in cell.GetOrganelles())
        {
            SerializedOrganelleData orgData = new(org);
            orgList.Add(orgData);
        }
    }
}
