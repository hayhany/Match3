using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MatchCondition : ScriptableObject
{
    public abstract bool IsConditionMet(Matchable thisMatchable, Matchable otherMatchable, out List<Matchable> relatedBlocks);
}
