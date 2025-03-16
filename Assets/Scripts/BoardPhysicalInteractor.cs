using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Board))]
public class BoardPhysicalInteractor : MonoBehaviour
{
    private MatchBlockObject selectedMatchable;
    private Board board;

    private void Awake()
    {
        board = GetComponent<Board>();
        MatchBlockObject.OnClicked += Matchable_OnClicked;
        board.OnRemovedBlock += Board_OnRemovedBlock;
        board.OnRemovedBlocks += OnRemovedBlocks;
    }

    // block ""gravity"", try implement queue, might make this more performant when removing vertical blocks

    // make this a coroutine, otherwise crushes on while loop
    // when removing blocks, search up until encountering a block, push it down, keep pushing all blocks down
    private void Board_OnRemovedBlock(Matchable matchable, bool affectPhysics)
    {
        if (!affectPhysics)
            return;

        int x = matchable.X, y = matchable.Y;

        // move each block down
        if (y >= 0)
        {
            if (y > 0)
            {
                Matchable above;
                do
                {
                    above = board.GetBlock(x, y - 1);
                } while (above == null);


                byte aboveType = above.BlockData.Identifier;
                board.RemoveBlock(x, above.Y);
                board.SetBlock(x, y, aboveType);
            }
            else
                board.SetBlockRandom(x, y);
        }
    }

    private void OnRemovedBlocks(List<Matchable> blocksToRemove, bool affectPhysics)
    {
        if (!affectPhysics)
            return;

        // we only want the unique X positioned blocks, and the lowest ones if we have more
        blocksToRemove.Sort((blockA, blockB) => blockB.Y.CompareTo(blockA.Y));
        blocksToRemove = blocksToRemove.GroupBy((matchable) => matchable.X).Select(x => x.FirstOrDefault()).ToList();

        foreach (var block in blocksToRemove)
        {
            int setY = block.Y;

            // climb up the board until encountering a block
            for (int checkY = setY - 1; checkY >= 0; checkY--)
            {
                Matchable checkBlock = board.GetBlock(block.X, checkY);

                // if encountered a block, remove it and set it on our (empty) location
                if (checkBlock != null)
                {
                    board.RemoveBlock(checkBlock.X, checkBlock.Y, false);
                    board.SetBlock(checkBlock.X, setY, checkBlock.BlockData.Identifier);
                    setY--;
                }
            }

            // set blocks for all the remaining empty spots
            for (; setY >= 0; setY--)
                board.SetBlockRandom(block.X, setY);
        }
    }

    private void Matchable_OnClicked(MatchBlockObject matchObject)
    {
        if (!matchObject.MatchableData.CurrentBoard.Equals(board) || !matchObject.IsSelectable || !matchObject.MatchableData.CurrentBoard.IsSelectable)
            return;

        if (selectedMatchable != null)
        {
            if (!selectedMatchable.Equals(matchObject))
                board.TryMatch(selectedMatchable.MatchableData, matchObject.MatchableData);

            matchObject.SetSelected(false);
            selectedMatchable.SetSelected(false);
            selectedMatchable = null;
        }

        else
        {
            selectedMatchable = matchObject;
            matchObject.SetSelected(true);
        }
    }
}
