using UnityEngine;
using Zenject;

namespace Movement
{
    public class GridConfig : MonoBehaviour
    {
        [SerializeField]
        public GameObject topLeftPoint;
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
        public void Construct(GridConfigProvider gridConfigProvider)
        {
            gridConfigProvider.Data = this;
        }
    }

    public class GridConfigProvider
    {
        public GridConfig Data;
    }
}
