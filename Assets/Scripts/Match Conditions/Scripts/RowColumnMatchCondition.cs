using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RowMatchCondition", menuName = "MatchLogic/Conditions/RowColumnMatchCondition")]
public class RowColumnMatchCondition : MatchCondition
{
    public int Length;

    // add out parameter for blocks that we checked for, also add list parameter for responses
    public override bool IsConditionMet(Matchable thisMatchable, Matchable otherMatchable)
    {
        int matchCount = 0;

        // check column


        int startCheckY = Math.Max(0, thisMatchable.Y - (Length - 1)); // spots available above for checking1
        for (int i = startCheckY; i < thisMatchable.Y + (Length - 1); i++)
        {
            if (thisMatchable.CurrentBoard.GetBlock(thisMatchable.X, i).BlockData.Identifier == thisMatchable.BlockData.Identifier)
                matchCount++;
        }

        if (matchCount < Length)
        {
            matchCount = 0;

            int startCheckX = Math.Max(0, thisMatchable.X - (Length - 1)); // spots available above for checking1
            for (int i = startCheckX; i < thisMatchable.X + (Length - 1); i++)
            {
                if (thisMatchable.CurrentBoard.GetBlock(i, thisMatchable.Y).BlockData.Identifier == thisMatchable.BlockData.Identifier)
                    matchCount++;
            }
        }

        return matchCount == Length;
    }
}
