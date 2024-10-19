using UnityEngine;

namespace Units.Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit _unit;
        protected bool _isActive;

        protected virtual void Awake()
        {
            _unit = GetComponent<Unit>();
        }
    }
}