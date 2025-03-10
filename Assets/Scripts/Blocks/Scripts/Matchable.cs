using System;

public class Matchable
{
    public MatchBlockData BlockData {  get; private set; }
    public Board CurrentBoard { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }


    public Matchable(MatchBlockData blockData, Board currentBoard, int x, int y)
    {
        BlockData = blockData;
        CurrentBoard = currentBoard;
        X = x;
        Y = y;
    }

    public bool TryMatch(Matchable otherMatchable)
    {
        foreach (MatchCondition condition in BlockData.MatchConditionsAndResponses.Keys)
        {
            if (condition.IsConditionMet(this, otherMatchable))
            {
                foreach (MatchResponse response in BlockData.MatchConditionsAndResponses[condition])
                    response.Respond(this, otherMatchable);

                return true;
            }
        }

        return false;
    }
}
