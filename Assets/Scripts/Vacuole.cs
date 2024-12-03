using UnityEngine;

public class Vacuole : Organelle
{
    [SerializeField] float maxDNAIncreaseBase = 500;
    [SerializeField] float maxProteinIncreaseBase = 500;
    [SerializeField] float maxATPIncreaseBase = 500;

    [SerializeField] float maxDNALevelMult = 0.25f;
    [SerializeField] float maxProteinLevelMult = 0.25f;
    [SerializeField] float maxATPLevelMult = 0.25f;
    ResourceCounter resourceCounter;

    void Start()
    {
        resourceCounter = FindFirstObjectByType<ResourceCounter>();
        resourceCounter.IncreaseMaxResources(ResourceType.DNA, maxDNAIncreaseBase);
        resourceCounter.IncreaseMaxResources(ResourceType.Protein, maxProteinIncreaseBase);
        resourceCounter.IncreaseMaxResources(ResourceType.ATP, maxATPIncreaseBase);

        resourceCounter.IncreaseMaxResources(ResourceType.DNA, maxDNAIncreaseBase * maxDNALevelMult * (level - 1));
        resourceCounter.IncreaseMaxResources(ResourceType.Protein, maxProteinIncreaseBase * maxProteinLevelMult * (level - 1));
        resourceCounter.IncreaseMaxResources(ResourceType.ATP, maxATPIncreaseBase * maxATPLevelMult * (level - 1));
    }

    public override void LevelUp()
    {
        base.LevelUp();
        resourceCounter.IncreaseMaxResources(ResourceType.DNA, maxDNAIncreaseBase * maxDNALevelMult);
        resourceCounter.IncreaseMaxResources(ResourceType.Protein, maxProteinIncreaseBase * maxProteinLevelMult);
        resourceCounter.IncreaseMaxResources(ResourceType.ATP, maxATPIncreaseBase * maxATPLevelMult);
    }
}
