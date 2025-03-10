using System;
using UnityEngine;

public abstract class MatchResponse : ScriptableObject
{
    public abstract void Respond(Matchable thisMatchable, Matchable otherMatchable);
}
