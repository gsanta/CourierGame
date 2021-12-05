using UnityEngine;
using Zenject;

namespace Movement
{
    public class GridConfig : MonoBehaviour
    {
        [SerializeField]
        public GameObject bottomLeft;
        [SerializeField]
        public GameObject bottomLeftSubScene;
        [SerializeField]
        public float gridSize;
        [SerializeField]
        public int gridRows;
        [SerializeField]
        public int gridCols;
        [SerializeField]
        public Tile tileTemplate;
        [SerializeField]
        public int tileRows;
        [SerializeField]
        public int tileCols;
        [SerializeField]
        public int xSectionStart;
        [SerializeField]
        public int zSectionStart;

        [Inject]
        public void Construct(GridSystem gridSystem)
        {
            gridSystem.GridConfig = this;
        }
    }
}
