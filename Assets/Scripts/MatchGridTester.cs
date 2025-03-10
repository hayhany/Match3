using Codice.Client.Common.GameUI;
using UnityEngine;

public class MatchGridTester : MonoBehaviour
{
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private Transform gridParent;
    [SerializeField] private Board board;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            board.Init(MatchGridGenerator.GenerateGrid((int)gridSize.x, (int)gridSize.y));
        }
    }
}
