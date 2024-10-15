using Player;
using UnityEngine;

namespace Grid
{
    public class TestingGrid : MonoBehaviour
    {
        [SerializeField] private Transform gridDebugPrefab;
        
        GridSystem _gridSystem;
        // Start is called before the first frame update
        void Start()
        {
            _gridSystem = new GridSystem(10, 10, 2);
            _gridSystem.CreateDebugObjects(gridDebugPrefab, this.transform);
        }

        // Update is called once per frame
        void Update()
        {
            var mouseWorld = MouseWorld.GetMouseWorldPosition();
            Debug.Log(_gridSystem.GetGridPosition(mouseWorld));
        }
    }
}
