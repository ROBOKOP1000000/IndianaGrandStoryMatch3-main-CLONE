using System.Linq;
using UnityEngine;

public class BoosterSpawn : MonoBehaviour
{
    public bool IsPlaneActive;
    public bool IsBombActive;
    public bool IsCoinActive;
    public bool IsSparkHorisontalActive;
    public bool IsSparkVerticalActive;

    public Board board;

    private void Awake()
    {
        IsPlaneActive = PreBoost.isPlane;
        IsBombActive = PreBoost.isBomb;
        IsCoinActive = PreBoost.isCoin;
        IsSparkHorisontalActive = PreBoost.isSpark;
        print($"Isplane {IsPlaneActive}, IsBomb {IsBombActive}, IsCoin {IsCoinActive}, IsSpark {IsSparkHorisontalActive}");
    }
    private void Start()
    {
        var cells = board.GetCellList().Where(x => x.CellType == CellType.Playable && x.ItemType == ItemType.Gem).ToList();

        var indexPlane = GetRandomInt(0, cells.Count());
        if (IsPlaneActive)
        {
            cells[indexPlane].SetItem(ItemType.Booster, BoosterType.Plane);
        }

        var indexBomb = GetRandomInt(0, cells.Count(), indexPlane);
        if (IsBombActive)
        {
            cells[indexBomb].SetItem(ItemType.Booster, BoosterType.Bomb);
        }

        var indexCoin = GetRandomInt(0, cells.Count(), indexPlane, indexBomb);
        if (IsCoinActive)
        {
            cells[indexCoin].SetItem(ItemType.Booster, BoosterType.Moodlet);
        }

        var indexSparkHorisontal = GetRandomInt(0, cells.Count(), indexPlane, indexBomb, indexCoin);
        if (IsSparkHorisontalActive)
        {
            cells[indexSparkHorisontal].SetItem(ItemType.Booster, BoosterType.SparkHorisontal);
        }

        var indexSparkVertical = GetRandomInt(0, cells.Count(), indexPlane, indexBomb, indexCoin, indexSparkHorisontal);
        if (IsSparkVerticalActive)
        {
            cells[indexSparkVertical].SetItem(ItemType.Booster, BoosterType.SparkVertical);
        }
        ResetPreBoostData();
    }
    void ResetPreBoostData()
    {
        PreBoost.isBomb = false;
        PreBoost.isCoin = false;
        PreBoost.isSpark = false;
        PreBoost.isPlane = false;
    }
    private int GetRandomInt(int minInclusive, int maxExclusive, params int[] exclude)
    {
        int value = Random.Range(minInclusive, maxExclusive);
        while (IsValueEquelExclude(value, exclude))
        {
            value = Random.Range(minInclusive, maxExclusive);
        }
        return value;
    }

    private bool IsValueEquelExclude(int value, params int[] exclude)
    {
        for (int i = 0; i < exclude.Length; i++)
        {
            if (value == exclude[i])
            {
                return true;
            }
        }
        return false;
    }
}