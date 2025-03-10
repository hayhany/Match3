using UnityEngine;

[CreateAssetMenu(fileName = "IdentifierMatchCondition", menuName = "MatchLogic/Conditions/IdentifierMatchCondition")]
public class IdentifierMatchCondition : MatchCondition
{
    public override bool IsConditionMet(Matchable thisMatchable, Matchable otherMatchable)
    {
        return thisMatchable.BlockData.Identifier == otherMatchable.BlockData.Identifier;
    }
}
