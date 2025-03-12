using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RowMatchCondition", menuName = "MatchLogic/Conditions/RowColumnMatchCondition")]
public class RowColumnMatchCondition : MatchCondition
{
    public int Length;

    // add out parameter for blocks that we checked for, also add list parameter for responses
    public override bool IsConditionMet(Matchable thisMatchable, Matchable otherMatchable, out List<Matchable> relatedBlocks)
    {
        relatedBlocks = new List<Matchable>();

        int adjusted = Length - 1;

        for (int i = Math.Clamp(thisMatchable.Y - adjusted, 0, thisMatchable.CurrentBoard.Height - 1); i < Math.Min (thisMatchable.Y + Length, thisMatchable.CurrentBoard.Height); i++)
        {
            Matchable matchable = thisMatchable.CurrentBoard.GetBlock(thisMatchable.X, i);
            if (relatedBlocks.Count == Length)
                break;
            else if (matchable.BlockData.Identifier == thisMatchable.BlockData.Identifier)
                relatedBlocks.Add(matchable);
            else
                relatedBlocks.Clear();
        }

        if (relatedBlocks.Count == Length)
            return true;

        relatedBlocks.Clear();
        for (int i = Math.Clamp(thisMatchable.X - adjusted, 0, thisMatchable.CurrentBoard.Width - 1); i < Math.Min(thisMatchable.X + Length, thisMatchable.CurrentBoard.Width); i++)
        {
            Matchable matchable = thisMatchable.CurrentBoard.GetBlock(i, thisMatchable.Y);
            if (relatedBlocks.Count == Length)
                break;
            else if (matchable.BlockData.Identifier == thisMatchable.BlockData.Identifier)
                relatedBlocks.Add(matchable);
            else
                relatedBlocks.Clear();
        }

        return relatedBlocks.Count == Length;
    }
}
