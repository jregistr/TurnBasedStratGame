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
        [SerializeField] private Image selectedImage;
        
        private BaseAction _action;

        public void SetBaseAction(BaseAction action)
        {
            _action = action;
            actionText.text = action.GetActionName().ToUpper();
            button.onClick.AddListener(() =>
            {
                UnitActionSystem.Instance.SetSelectedAction(action);
            });
        }

        public void ShowSelected(bool isSelected)
        {
            selectedImage.enabled = isSelected;
        }

        public bool IsForAction(BaseAction action)
        {
            if (action == null)
            {
                return false;
            }
            return action.GetActionName() == _action.GetActionName();
        }
    }
}