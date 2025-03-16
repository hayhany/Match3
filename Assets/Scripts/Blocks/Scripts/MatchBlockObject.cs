using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// this is just a gameobject wrapper for Matchable, this is for physical representation
public class MatchBlockObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private Outline selectOutline;

    [SerializeField] private bool selectable = true;
    public bool IsSelectable => selectable;

    public Matchable MatchableData { get; private set; }

    public static event Action<MatchBlockObject> OnClicked;

    public void Init(Matchable matchable)
    {
        this.MatchableData = matchable;
        this.icon.sprite = matchable.BlockData.Icon;
    }

    public void OnPointerDown(PointerEventData eventData) { }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (selectable)
            OnClicked?.Invoke(this);
    }

    public void SetSelected(bool selected)
    {
        selectOutline.enabled = selected;
    }
}
