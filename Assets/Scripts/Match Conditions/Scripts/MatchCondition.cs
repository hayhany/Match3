using System;
using UnityEngine;

public abstract class MatchCondition : ScriptableObject
{
    public abstract bool IsConditionMet(Matchable thisMatchable, Matchable otherMatchable);
}
