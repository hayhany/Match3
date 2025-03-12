using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestroyMatchedResponse", menuName = "Scriptable Objects/DestroyMatchedResponse")]
public class DestroyMatchedResponse : MatchResponse
{
    // respond with destroying both blocks
    public override void Respond(List<Matchable> blocks)
    {
        // fuck me if this doesn't work
        blocks[0].CurrentBoard.RemoveBlocks(blocks);
    }
}
