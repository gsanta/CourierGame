using UnityEngine;
using Zenject;

namespace Movement
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField]
        private Vector3 centerPoint;
        [SerializeField]
        private Tile tileTemplate;
        [SerializeField]
        private Vector2 dimensions;

        private int x;
        private int y;

        private Tile[] tiles;

        [Inject]
        public void Construct(TileManagerProvider tileManagerProvider)
        {
            tileManagerProvider.Data = this;
        }

        private void Awake()
        {
            x = Mathf.RoundToInt(dimensions.x);
            y = Mathf.RoundToInt(dimensions.y);
        }

        private void Start()
        {
            tiles = new Tile[x * y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Tile tile = Instantiate(tileTemplate, transform);
                    tiles[i * y + j] = tile;
                    tile.InitMesh();
                }
            }
        }

        public void SetTilesCenter(Vector3 vector3)
        {
            centerPoint = vector3;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Tile tile = tiles[i * y + j];

                    int halfX = Mathf.RoundToInt(x / 2);
                    int halfY = Mathf.RoundToInt(y / 2);

                    tile.SetCenterPoint(new Vector3(centerPoint.x + Tile.width * (i - halfX), 1, centerPoint.z + Tile.width * (j - halfY)));
                }
            }
        }
    }

    public class TileManagerProvider
    {
        public TileManager Data { get; set; }
    }
}
