[System.Serializable]
public class SerializedOrganelleData
{
    public int id { get; private set; }
    public int level { get; private set; }
    public bool built { get; private set; }

    public SerializedOrganelleData(Organelle org)
    {
        id = org.GetID();
        level = org.GetLevel();
        built = org.IsBuilt();
    }
}
