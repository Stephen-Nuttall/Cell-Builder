[System.Serializable]
public class SerializedResourceData
{
    public float DNA { get; private set; }
    public float protein { get; private set; }
    public float ATP { get; private set; }
    public float maxDNA { get; private set; }
    public float maxProtein { get; private set; }
    public float maxATP { get; private set; }

    public SerializedResourceData(ResourceCounter counter)
    {
        DNA = counter.GetResourceCount(ResourceType.DNA);
        protein = counter.GetResourceCount(ResourceType.Protein);
        ATP = counter.GetResourceCount(ResourceType.ATP);

        maxDNA = counter.GetResourceMax(ResourceType.DNA);
        maxProtein = counter.GetResourceMax(ResourceType.Protein);
        maxATP = counter.GetResourceMax(ResourceType.ATP);
    }
}
