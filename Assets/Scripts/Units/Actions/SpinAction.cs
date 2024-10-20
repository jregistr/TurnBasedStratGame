using System;
using UnityEngine;

namespace Units.Actions
{
    public class SpinAction : BaseAction
    {
        [SerializeField] private float spinAmount;
        private float _totalSpinAmount;
        

        private void Update()
        {
            if (!IsActive) return;

            var spinAddAmount = 360 * Time.deltaTime;
            _totalSpinAmount += spinAddAmount;
            transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

            if (_totalSpinAmount >= 360)
            {
                IsActive = false;
                OnActionComplete();
            }
        }

        public void Spin(Action onComplete)
        {
            IsActive = true;
            OnActionComplete = onComplete;
            _totalSpinAmount = 0;
        }

        public override string GetActionName() => "Spin";
    }
}