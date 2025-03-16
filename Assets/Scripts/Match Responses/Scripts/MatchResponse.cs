using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MatchResponse : ScriptableObject
{
    public abstract void Respond(Matchable thisMatchable, Matchable? otherMatchable, List<Matchable> blocks);
}
