using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class TurnSystemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI turnText;
        [SerializeField] private Button turnButton;

        private void Awake()
        {
            turnButton.onClick.AddListener(() =>
            {
                TurnSystem.Instance.NextTurn();
            });
        }

        private void Start()
        {
            TurnSystem.Instance.TurnChangedTo += TurnSystem_OnTurnChanged;
            SetTurnText(TurnSystem.Instance.GetCurrentTurn());
        }

        private void TurnSystem_OnTurnChanged(object sender, int turnCount)
        {
            SetTurnText(turnCount);
        }

        private void SetTurnText(int turnCount)
        {
            turnText.text = $"Turn {turnCount}";
        }
    }
}