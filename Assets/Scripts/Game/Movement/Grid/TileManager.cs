using UnityEngine;
using UnityEngine.AI;
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

        private Vector2 positionFix = new Vector2(0.2f, 0.2f);

        private int x;
        private int y;

        private Tile[] tiles;
        private bool visible;

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
            if (!visible) return;

            centerPoint = vector3;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Tile tile = tiles[i * y + j];

                    int halfX = Mathf.RoundToInt(x / 2);
                    int halfY = Mathf.RoundToInt(y / 2);
                    float cX = centerPoint.x + Tile.width * (i - halfX) + positionFix.x;
                    float cY = centerPoint.z + Tile.width * (j - halfY) + positionFix.y;

                    NavMeshHit hit;
                    bool tileVisible = NavMesh.SamplePosition(new Vector3(cX, 0, cY), out hit, 0.5f, NavMesh.AllAreas);
                    tile.SetVisible(tileVisible);
                    tile.SetCenterPoint(new Vector3(cX, 1, cY));
                }
            }
        }

        public void SetVisible(bool visible)
        {
            foreach (var item in tiles)
            {
                item.SetVisible(visible);
            }
            this.visible = visible;
        }
    }

    public class TileManagerProvider
    {
        public TileManager Data { get; set; }
    }
}
