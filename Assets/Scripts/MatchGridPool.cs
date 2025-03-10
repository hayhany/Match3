using AYellowpaper.SerializedCollections;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class MatchGridPool : SingletonBehaviour<MatchGridPool>
{
    [SerializeField] private SerializedDictionary<byte, MatchBlockData> matchBlockPool;

    public MatchBlockData GetRandomBlock()
    {
        byte random = ShuffleBytes(matchBlockPool.Keys.ToList())[0];
        return matchBlockPool[random];
    }

    public MatchBlockData GetBlock(byte identifier) => matchBlockPool[identifier];

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
