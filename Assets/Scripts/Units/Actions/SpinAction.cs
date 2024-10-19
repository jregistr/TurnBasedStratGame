using UnityEngine;

namespace Units.Actions
{
    public class SpinAction : BaseAction
    {
        [SerializeField] private float spinAmount;
        private float _totalSpinAmount;

        private void Update()
        {
            if (!_isActive) return;

            var spinAddAmount = 360 * Time.deltaTime;
            _totalSpinAmount += spinAddAmount;
            transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

            if (_totalSpinAmount >= 360)
            {
                _isActive = false;
            }
            
        }

        public void Spin()
        {
            _isActive = true;
            _totalSpinAmount = 0;
        }
    }
}