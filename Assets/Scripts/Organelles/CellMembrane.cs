using UnityEngine;

public class CellMembrane : MonoBehaviour
{
    [SerializeField] int maxOrganelles = 4;
    [SerializeField] int increasePerLevel = 2;

    public int GetMaxOrg() { return maxOrganelles; }
    public int GetIncreasePerLevel() { return increasePerLevel; }
}
