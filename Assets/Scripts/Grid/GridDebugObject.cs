using TMPro;
using UnityEngine;

namespace Grid
{
    public class GridDebugObject : MonoBehaviour
    {
       [SerializeField] private TextMeshPro textMesh;
       private GridObject _gridObject;

       public void Initialize(GridObject gridObject)
       {
           _gridObject = gridObject;
       }

       private void Update()
       {
           textMesh.SetText(_gridObject.ToString());
       }
    }
}
