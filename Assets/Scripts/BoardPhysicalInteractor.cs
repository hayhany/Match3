using Codice.Client.Common.GameUI;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;

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
        // list blocks from bottom to top
        blocksToRemove.Sort((blockA, blockB) => blockB.Y.CompareTo(blockA.Y));

        // 2, 6
        // 2, 7
        // 4, 7

        Matchable matchable = blocksToRemove[0];
        // climb up the board until encountering a block
        int setY = matchable.Y;
        for (int checkY = matchable.Y - 1; checkY >= 0; checkY--)
        {
            Matchable checkBlock = board.GetBlock(matchable.X, checkY);

            // if encountered a block, remove it and set it on our (empty) location
            if (checkBlock != null)
            {
                board.RemoveBlock(checkBlock.X, checkBlock.Y, false);
                board.SetBlock(checkBlock.X, setY, checkBlock.BlockData.Identifier);
                setY--;
                //break;
            }
        }

        Matchable highestRemoved = blocksToRemove[blocksToRemove.Count - 1];

        for (; setY >= 0; setY--)
        {
            Debug.Log($"set block at {highestRemoved.X}, {setY}");
            board.SetBlockRandom(highestRemoved.X, setY);
        }
        /*
        */
    }

    private void Matchable_OnClicked(MatchBlockObject matchObject)
    {
        if (!matchObject.MatchableData.CurrentBoard.Equals(board))
            return;

        if (selectedMatchable != null)
        {
            if (!selectedMatchable.Equals(matchObject))
            {
                board.TryMatch(selectedMatchable.MatchableData, matchObject.MatchableData);
                Debug.Log("Trying to match");
            }

            selectedMatchable = null;
            Debug.Log("Deselected");
        }

        else
        {
            selectedMatchable = matchObject;
            Debug.Log("Selected");
        }
    }
}
