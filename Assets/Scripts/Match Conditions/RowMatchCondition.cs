using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RowMatchCondition", menuName = "MatchLogic/Conditions/RowMatchCondition")]
public class RowMatchCondition : MatchCondition
{
    public override bool IsConditionMet(Matchable thisMatchable, Matchable otherMatchable, out List<Matchable> relatedBlocks)
    {
        throw new System.NotImplementedException();
    }
}
