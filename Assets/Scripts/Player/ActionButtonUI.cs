using TMPro;
using Units.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class ActionButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI actionText;
        [SerializeField] private Button button;

        public void SetBaseAction(BaseAction action)
        {
            actionText.text = action.GetActionName().ToUpper();
        }
    }
}