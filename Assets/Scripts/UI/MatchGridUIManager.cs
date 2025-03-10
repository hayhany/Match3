using System;
using UnityEngine;

public class MatchGridUIManager : MonoBehaviour
{
    //[SerializeField] private MatchGridPresentor gridPresentor;
    private void Start()
    {
        BoardManager.Instance.OnBoardCreated += OnGeneratedNewGrid;
    }

    private void OnGeneratedNewGrid(Board board)
    {
        //gridPresentor.Display(board);
    }
}
