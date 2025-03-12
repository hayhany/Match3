using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MatchResponse : ScriptableObject
{
    public abstract void Respond(List<Matchable> blocks);
}
