using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardObjectPresentor : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField] private MatchBlockObject matchObjectPrerfab;
    [SerializeField] private GameObject emptyObjectPrefab;
    [SerializeField] private Board board;

    [Header("Other Options")]
    [SerializeField] private float RelativeCascadeSpeed = 1f;

    private bool _dequeueing;
    private readonly Queue<Tuple<Matchable, Action<Matchable>>> displayActionQueue = new();
    private readonly Dictionary<Vector2, MatchBlockObject> matchObjects = new();

    private void Awake()
    {
        StartCoroutine(DequeueDisplay());
        board.OnInit += Display;
        board.OnRemovedBlock += Board_OnRemovedBlock1;
        board.OnAddedBlock += (matchable) => displayActionQueue.Enqueue(new Tuple<Matchable, Action<Matchable>>(matchable, CreateAction));
    }

    private void Board_OnRemovedBlock1(Matchable matchable, bool affectPhysics)
    {
        if (!matchObjects.ContainsKey(new Vector2(matchable.X, matchable.Y)))
            return;

        displayActionQueue.Enqueue(new Tuple<Matchable, Action<Matchable>>(matchable, RemoveAction));
    }

    // TODO: use object pooling
    public void Display()
    {
        DestroyAllChildren(gridLayout.transform);
        SetGridSizes(board.Width, board.Height);
    }

    private void RemoveAction(Matchable matchable)
    {
        Vector2 matchPos = new Vector2(matchable.X, matchable.Y);
        if (!matchObjects.TryGetValue(matchPos, out MatchBlockObject matchObject))
            return;

        Destroy(matchObject.gameObject);
        GameObject empty = Instantiate(emptyObjectPrefab, gridLayout.transform);
        empty.transform.SetSiblingIndex(GetSiblingIndexForPos(matchable.X, matchable.Y));

        matchObjects.Remove(matchPos);
        Debug.Log($"<color=red> removed block in location {matchable.X}, {matchable.Y}</color>");
    }

    private void CreateAction(Matchable matchable)
    {
        int siblingIndex = GetSiblingIndexForPos(matchable.X, matchable.Y);
        if (gridLayout.transform.childCount > siblingIndex)
        {
            GameObject placeObject = gridLayout.transform.GetChild(siblingIndex).gameObject;
            if (placeObject.CompareTag("Empty"))
                Destroy(placeObject);
        }

        MatchBlockObject blockObject = Instantiate(matchObjectPrerfab, gridLayout.transform);
        blockObject.transform.SetSiblingIndex(siblingIndex);
        blockObject.Init(matchable);

        Debug.Log($"<color=green> created block in location {matchable.X}, {matchable.Y}</color>");

        matchObjects[new Vector2(matchable.X, matchable.Y)] = blockObject;
    }

    private int GetSiblingIndexForPos(int x, int y) => (board.Width * y) + x;

    // changes the grid sizes so it fits all the blocks nicely :)
    private void SetGridSizes(int width, int height)
    {
        // x = (rectWidth / width) - spacing
        // y = (rectHeight / height) - spacing

        gridLayout.cellSize = new Vector2((gridLayout.GetComponent<RectTransform>().rect.width / width) - gridLayout.spacing.x,
            (gridLayout.GetComponent<RectTransform>().rect.height / height) - gridLayout.spacing.y);

        gridLayout.constraintCount = width;
    }

    // no fbi pls
    private static void DestroyAllChildren(Transform trans)
    {
        foreach (Transform child in trans)
            Destroy(child.gameObject);
    }

    private IEnumerator DequeueDisplay()
    {
        while (true)
        {
            _dequeueing = displayActionQueue.Count > 0;
            if (_dequeueing)
            {
                Tuple<Matchable, Action<Matchable>> matchableAction = displayActionQueue.Dequeue();
                matchableAction.Item2?.Invoke(matchableAction.Item1);
            }

            yield return new WaitForSeconds(0.05f);
            //yield return new WaitForSeconds(RelativeCascadeSpeed / (gridLayout.cellSize.x * gridLayout.cellSize.y));
        }
    }
}
