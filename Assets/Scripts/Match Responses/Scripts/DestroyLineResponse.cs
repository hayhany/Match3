using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestroyLineResponse", menuName = "Scriptable Objects/DestroyLineResponse")]
public class DestroyLineResponse : MatchResponse
{
    public override void Respond(Matchable thisMatchable, Matchable? otherMatchable, List<Matchable> blocks)
    {
        List<Matchable> destroyBlocks = new List<Matchable>();
        for (int i = 0; i < thisMatchable.CurrentBoard.Width; i++)
        {
            destroyBlocks.Add(thisMatchable.CurrentBoard.GetBlock(i, thisMatchable.Y));
        }
        
        thisMatchable.CurrentBoard.RemoveBlocks(destroyBlocks);
    }
}
