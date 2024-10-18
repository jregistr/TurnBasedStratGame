using UnityEngine;

namespace Grid
{
    public class GridVisualItem : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

        public void Show()
        {
            meshRenderer.enabled = true;
        }

        public void Hide()
        {
            meshRenderer.enabled = false;
        }
    }
}