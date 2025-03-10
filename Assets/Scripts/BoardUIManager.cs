using UnityEngine;
using UnityEngine.UI;

public class BoardUIManager : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField] private Board board;

    private void Awake()
    {
        board.OnInit += () => SetGridSizes(board.Width, board.Height);
        //board.OnRemovedBlock += Board_OnRemovedBlock;
    }

    private void SetGridSizes(int width, int height)
    {
        // x = (rectWidth / width) - spacing
        // y = (rectHeight / height) - spacing
        gridLayout.cellSize = new Vector2((gridLayout.GetComponent<RectTransform>().rect.width / width) - gridLayout.spacing.x,
            (gridLayout.GetComponent<RectTransform>().rect.height / height) - gridLayout.spacing.y);

        gridLayout.constraintCount = width;
    }
}
