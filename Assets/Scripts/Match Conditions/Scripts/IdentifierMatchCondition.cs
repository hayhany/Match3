using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdentifierMatchCondition", menuName = "MatchLogic/Conditions/IdentifierMatchCondition")]
public class IdentifierMatchCondition : MatchCondition
{
    public override bool IsConditionMet(Matchable thisMatchable, Matchable otherMatchable, out List<Matchable> relatedBlocks)
    {
        relatedBlocks = new List<Matchable>() { thisMatchable, otherMatchable};
        return thisMatchable.BlockData.Identifier == otherMatchable.BlockData.Identifier;
    }
}
