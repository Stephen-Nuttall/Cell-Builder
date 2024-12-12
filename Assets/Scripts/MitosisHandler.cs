using UnityEngine;

public class MitosisHandler : MonoBehaviour
{
    [SerializeField] ResourceCounter resourceCounter;
    [SerializeField] GameObject bacteriaCellPrefab;
    [SerializeField] GameObject plantCellPrefab;
    [SerializeField] GameObject animalCellPrefab;
    [SerializeField] float mitosisDNACost = 100;
    [SerializeField] float mitosisProteinCost = 100;
    [SerializeField] float mitosisATPCost = 100;
    [SerializeField] float evolveDNACost = 500;
    [SerializeField] float evolveProteinCost = 500;
    [SerializeField] float evolveATPCost = 500;
    [SerializeField] bool canBuildPlantCell = false;
    [SerializeField] bool canBuildAnimalCell = false;
    [SerializeField] int cellCount = 1;
    bool doEvolve = false;
    int evolveLevel = 0;

    public void TryMitosis()
    {
        float currentDNA = resourceCounter.GetResourceCount(ResourceType.DNA);
        float currentProtein = resourceCounter.GetResourceCount(ResourceType.Protein);
        float currentATP = resourceCounter.GetResourceCount(ResourceType.ATP);

        if (doEvolve)
        {
            if (CanEvolve(currentDNA, currentProtein, currentATP))
            {
                resourceCounter.SpendResources(ResourceType.DNA, evolveDNACost + mitosisDNACost);
                resourceCounter.SpendResources(ResourceType.Protein, evolveProteinCost + mitosisProteinCost);
                resourceCounter.SpendResources(ResourceType.ATP, evolveATPCost + mitosisATPCost);
                Evolve();
                Mitosis();
            }
        }
        else
        {
            if (CanMitosis(currentDNA, currentProtein, currentATP))
            {
                resourceCounter.SpendResources(ResourceType.DNA, mitosisDNACost);
                resourceCounter.SpendResources(ResourceType.Protein, mitosisProteinCost);
                resourceCounter.SpendResources(ResourceType.ATP, mitosisATPCost);
                Mitosis();
            }
        }
    }

    void Evolve()
    {
        evolveLevel++;
        if (evolveLevel == 1)
        {
            canBuildPlantCell = true;
        }
        else if (evolveLevel == 2)
        {
            canBuildAnimalCell = true;
        }
    }

    void Mitosis()
    {
        if (canBuildAnimalCell)
            CreateCell(CellEvolution.Animal);
        else if (canBuildPlantCell)
            CreateCell(CellEvolution.Plant);
        else
            CreateCell(CellEvolution.Bacteria);
    }

    void CreateCell(CellEvolution cellType)
    {
        GameObject newCell;
        SerializedCellData cellData;
        if (cellType == CellEvolution.Bacteria)
        {
            newCell = Instantiate(bacteriaCellPrefab, new Vector3(15 * cellCount, 0, 0), Quaternion.identity);
            cellData = FileReadWrite.ReadCellData(CellEvolution.Bacteria);
        }
        else if (cellType == CellEvolution.Plant)
        {
            newCell = Instantiate(plantCellPrefab, new Vector3(15 * cellCount, 0, 0), Quaternion.identity);
            cellData = FileReadWrite.ReadCellData(CellEvolution.Plant);
        }
        else
        {
            newCell = Instantiate(animalCellPrefab, new Vector3(15 * cellCount, 0, 0), Quaternion.identity);
            cellData = FileReadWrite.ReadCellData(CellEvolution.Animal);
        }

        newCell.GetComponent<Cell>().LoadFromFile(cellData);
        newCell.GetComponent<Cell>().SetCellNumToLatest();
        cellCount++;
    }

    public void ToggleEvolve()
    {
        doEvolve = !doEvolve;
    }

    bool CanMitosis(float currentDNA, float currentProtein, float currentATP)
    {
        return currentDNA >= mitosisDNACost && currentProtein >= mitosisProteinCost && currentATP >= mitosisATPCost;
    }

    bool CanEvolve(float currentDNA, float currentProtein, float currentATP)
    {
        return currentDNA >= evolveDNACost + mitosisDNACost && currentProtein >= evolveProteinCost + mitosisProteinCost
            && currentATP >= evolveATPCost + mitosisATPCost;
    }

    public float GetMitosisDNACost() { return mitosisDNACost; }
    public float GetMitosisProteinCost() { return mitosisProteinCost; }
    public float GetMitosisATPCost() { return mitosisATPCost; }
    public float GetEvolveDNACost() { return evolveDNACost; }
    public float GetEvolveProteinCost() { return evolveProteinCost; }
    public float GetEvolveATPCost() { return evolveATPCost; }
    public int GetCellCount() { return cellCount; }
    public void IncrementCellCount() { cellCount++; }
}
