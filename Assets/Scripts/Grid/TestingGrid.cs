using Player;
using Units;
using UnityEngine;

namespace Grid
{
    public class TestingGrid : MonoBehaviour
    {
        [SerializeField] private Transform gridDebugPrefab;
        [SerializeField] private Unit unit;
        
        // GridSystem _gridSystem;
        // Start is called before the first frame update
        void Start()
        {
            // _gridSystem = new GridSystem(10, 10, 2);
            // _gridSystem.CreateDebugObjects(gridDebugPrefab, this.transform);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                var validPositions = unit.MoveAction.GetValidActionGridPositionList();
                Debug.Log(validPositions);
                // GridSystemVisual.
                // GridVisualSystem.Instance.HideAllGridVisuals();
                // GridVisualSystem.Instance.ShowPositions(validPositions);
            }
            
        }
    }
}
