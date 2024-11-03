using UnityEngine;

namespace Player
{
    public class ActionBusyUI : MonoBehaviour
    {
        private void Start()
        {
            UnitActionSystem.Instance.OnUnitExecutingActionChanged += UnitActionSystem_OnUnitRunningActionChange;
            gameObject.SetActive(false);
        }

        private void UnitActionSystem_OnUnitRunningActionChange(object sender, bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
