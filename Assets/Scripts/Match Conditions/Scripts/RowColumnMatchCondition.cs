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
        // get matching blocks above or below
        relatedBlocks = GetMatchingBlocks(thisMatchable.CurrentBoard, thisMatchable, height:true);

        // if doesn't have enough blocks for matching, check length-wise
        if (relatedBlocks.Count < Length)
            relatedBlocks = GetMatchingBlocks(thisMatchable.CurrentBoard, thisMatchable, height:false);

        return relatedBlocks.Count == Length;
    }

    private List<Matchable> GetMatchingBlocks(Board board, Matchable thisMatchable, bool height)
    {
        int adjusted = Length - 1;
        List<Matchable> matching = new List<Matchable>();

        int checkAxis = height ? thisMatchable.Y : thisMatchable.X;
        int checkDimensions = height ? board.Height : board.Width;

        for (int i = Math.Clamp(checkAxis - adjusted, 0, checkDimensions - 1); i < Math.Min(checkAxis + Length, checkDimensions); i++)
        {
            Matchable matchable = height ? thisMatchable.CurrentBoard.GetBlock(thisMatchable.X, i) : thisMatchable.CurrentBoard.GetBlock(i, thisMatchable.Y);
            if (matching.Count == Length)
                break;
            else if (matchable.BlockData.Identifier == thisMatchable.BlockData.Identifier)
                matching.Add(matchable);
            else
                matching.Clear();
        }

        return matching;
    }
}
