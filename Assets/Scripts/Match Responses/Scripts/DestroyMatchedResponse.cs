using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestroyMatchedResponse", menuName = "Scriptable Objects/DestroyMatchedResponse")]
public class DestroyMatchedResponse : MatchResponse
{
    // respond with destroying both blocks
    public override void Respond(Matchable thisMatchable, Matchable otherMatchable)
    {
        // fuck me if this doesn't work
        thisMatchable.CurrentBoard.RemoveBlocks(new List<Vector2> { new Vector2(thisMatchable.X, thisMatchable.Y), new Vector2(otherMatchable.X, otherMatchable.Y) });
    }

    private void RemoveBlocksByHeight(List<Matchable> matchablesToRemove)
    {
        matchablesToRemove.Sort((i1, i2) => i2.Y.CompareTo(i1.Y));
        //matchablesToRemove.Reverse();
        foreach (Matchable matchable in matchablesToRemove)
        {
            matchable.CurrentBoard.RemoveBlock(matchable.X, matchable.Y);
        }
    }
}
