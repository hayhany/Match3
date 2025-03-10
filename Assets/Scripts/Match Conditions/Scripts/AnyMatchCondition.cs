using UnityEngine;

[CreateAssetMenu(fileName = "IdentifierMatchCondition", menuName = "MatchLogic/Conditions/AnyMatchCondition")]
public class AnyMatchCondition : MatchCondition
{
    public override bool IsConditionMet(Matchable thisMatchable, Matchable otherMatchable)
    {
        return true;
    }
}
