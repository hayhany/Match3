using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Xml.Serialization;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Matchable[,] boardData;
    public int Width {  get; private set; }
    public int Height {  get; private set; }

    public event System.Action<Matchable, bool> OnRemovedBlock;
    public event System.Action<List<Matchable>, bool> OnRemovedBlocks;
    public event System.Action<Matchable, int, int> OnChangedBlock;
    public event System.Action<Matchable> OnAddedBlock;
    public event Action OnInit;

    public void Init(byte[,] boardData)
    {
        this.boardData = new Matchable[boardData.GetLength(0), boardData.GetLength(1)];
        Height = boardData.GetLength(0);
        Width = boardData.GetLength(1);


        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                CreateBlock(j, i, boardData[i, j]);
            }
        }

        OnInit?.Invoke();
    }

    private void CreateBlock(int x, int y, byte identifier)
    {
        Matchable matchable = new Matchable(MatchGridPool.Instance.GetBlock(identifier), this, x, y);
        boardData[y, x] = matchable;
        OnAddedBlock?.Invoke(matchable);
    }

    public bool TryMatch(Matchable matchable1, Matchable matchable2)
    {
        // only try match them if they're next to each other
        if ((matchable1.X == matchable2.X && Math.Abs(matchable1.Y - matchable2.Y) == 1) ||
            (matchable1.Y == matchable2.Y && Math.Abs(matchable1.X - matchable2.X) == 1))
        {
            // swap blocks
            SetBlock(matchable2.X, matchable2.Y, matchable1.BlockData.Identifier);
            SetBlock(matchable1.X, matchable1.Y, matchable2.BlockData.Identifier);

            bool matched = false;

            if (GetBlock(matchable1.X, matchable1.Y).TryMatch(GetBlock(matchable2.X, matchable2.Y)))
                matched = true;
            else if (GetBlock(matchable2.X, matchable2.Y).TryMatch(GetBlock(matchable1.X, matchable1.Y)))
                matched = true;

            else
            {
                SetBlock(matchable1.X, matchable1.Y, matchable1.BlockData.Identifier);
                SetBlock(matchable2.X, matchable2.Y, matchable2.BlockData.Identifier);
                // swap them back
            }

            return matched;
        }

        return false;
    }

    public Matchable GetBlock(int x, int y)
    {
        if (boardData == null || !IsPositionValid(x, y))
            return null;

        return boardData[y, x];
    }

    public Matchable[,] GetBlocks()
    {
        return boardData;
    }

    // maybe add an invisible block for display purposes
    public bool RemoveBlock(int x, int y, bool affectPhysics = true)
    {
        if (!IsPositionValid(x, y)) 
            return false;

        Matchable removedBlock = boardData[y, x];
        boardData[y, x] = null;

        OnRemovedBlock?.Invoke(removedBlock, affectPhysics);

        return true;
    }

    public bool RemoveBlocks(List<Vector2> blocks, bool affectPhysics = true)
    {
        List<Matchable> removedBlocks = new List<Matchable>();
        foreach (var pos in blocks)
        {
            int x = (int)pos.x, y = (int)pos.y;
            if (IsPositionValid(x, y))
            {
                removedBlocks.Add(GetBlock(x, y));
                RemoveBlock(x, y, false);
            }
        }

        if (removedBlocks.Count > 0)
        {
            OnRemovedBlocks?.Invoke(removedBlocks, affectPhysics);
            return true;
        }

        return false;
    }

    public bool SetBlockRandom(int x, int y) => SetBlock(x, y, MatchGridPool.Instance.GetRandomBlock().Identifier);

    public bool SetBlock(int x, int y, byte identifier)
    {
        if (!IsPositionValid(x, y))
            return false;

        if (boardData[y, x] != null)
            RemoveBlock(x, y, false);

        CreateBlock(x, y, identifier);
        return true;
    }

    private bool IsPositionValid(int x, int y)
    {
        return (y >= 0 && y < boardData.GetLength(0)) && (x >= 0 && x < boardData.GetLength(1));
    }
}
