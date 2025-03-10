using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchBlockData", menuName = "GridItems/MatchBlockData")]
public class MatchBlockData : ScriptableObject
{
    public byte Identifier;
    public string Name;
    public Sprite Icon;
    public Color Color = Color.white;

    public SerializedDictionary<MatchCondition, List<MatchResponse>> MatchConditionsAndResponses;
}
