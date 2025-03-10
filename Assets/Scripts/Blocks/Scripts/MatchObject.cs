using UnityEngine;
using UnityEngine.UI;

public class MatchObject : MonoBehaviour
{
    [SerializeField] private Image icon;
    private MatchBlockData matchData;

    public void Init(MatchBlockData matchableData)
    {
        matchData = matchableData;
        icon.sprite = matchData.Icon;
    }
}
