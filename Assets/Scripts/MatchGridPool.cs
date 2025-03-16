using AYellowpaper.SerializedCollections;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class MatchGridPool : SingletonBehaviour<MatchGridPool>
{
    [Serializable]
    private class MatchBlockPoolItem
    {
        public MatchBlockData Block;
        public bool Spawnable = true;
    }

    [SerializeField] private SerializedDictionary<byte, MatchBlockPoolItem> matchBlockPool;
    private Dictionary<byte, MatchBlockData> spawnPool;

    protected override void Awake()
    {
        base.Awake();
        spawnPool = new Dictionary<byte, MatchBlockData>();

        foreach (var spawnItem in matchBlockPool.Values)
        {
            if (spawnItem.Spawnable)
                spawnPool.Add(spawnItem.Block.Identifier, spawnItem.Block);
        }
    }

    public MatchBlockData GetRandomBlock(bool limitToSpawnPool)
    {
        if (limitToSpawnPool)
            return spawnPool[ShuffleBytes(spawnPool.Keys.ToList())[0]];

        return matchBlockPool[ShuffleBytes(matchBlockPool.Keys.ToList())[0]].Block;
    }

    public MatchBlockData GetBlock(byte identifier)
    {
        return matchBlockPool[identifier].Block;
    }

    private List<byte> ShuffleBytes(List<byte> bytes)
    {
        List<byte> tempList = new List<byte>(bytes);
        List<byte> output = new List<byte>(tempList.Count);

        while (tempList.Count > 0)
        {
            int index = Random.Range(0, tempList.Count);
            output.Add(tempList[index]);
            tempList.RemoveAt(index);
        }

        return output;
    }
}
