using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestroyMatchedResponse", menuName = "Scriptable Objects/DestroyMatchedAndReplaceByIdentifier")]
public class DestroyMatchedAndReplaceByIdentifier : MatchResponse
{
    public byte Identifier;
    // respond with destroying both blocks
    public override void Respond(Matchable thisMatchable, Matchable? otherMatchable, List<Matchable> blocks)
    {
        //if (otherMatchable == null)
            thisMatchable.CurrentBoard.SetBlock(thisMatchable.X, thisMatchable.Y, Identifier);

        //else
            //blocks[0].CurrentBoard.SetBlock(otherMatchable.X, otherMatchable.Y, Identifier);
    }
}
