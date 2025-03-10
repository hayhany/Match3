using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BoardManager : SingletonBehaviour<BoardManager>
{
    [SerializeField] private Board boardPrefab;
    [SerializeField] private Matchable matchablePrefab;
    private Dictionary<byte, Board> _boards;

    public event System.Action<Board> OnBoardCreated;

    public void CreateBoard(int width, int height, Transform parent)
    {
        Board board = Instantiate(boardPrefab);
        board.transform.SetParent(parent);
        board.Init(MatchGridGenerator.GenerateGrid(width, height));

        OnBoardCreated?.Invoke(board);
    }

    public Board GetBoardById(byte id)
    {
        if (_boards.ContainsKey(id))
            return _boards[id];

        return null;
    }
}
