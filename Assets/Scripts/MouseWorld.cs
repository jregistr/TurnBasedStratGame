using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    [SerializeField]
    private LayerMask mousePlaneLayerMask;
    // Start is called before the first frame update
 
    private static MouseWorld _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out var hitInfo, float.MaxValue, _instance.mousePlaneLayerMask);
        return hitInfo.point;
    }
}
