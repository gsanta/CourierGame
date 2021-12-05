using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Movement
{
    public class TileManager : MonoBehaviour
    {
        private Tile[,] tiles;
        private bool visible;
        private GridSystem gridSystem;

        public IntPos TopLeft { get; private set; }

        [Inject]
        public void Construct(GridSystem gridSystem)
        {
            this.gridSystem = gridSystem;
            gridSystem.TileManager = this;
        }

        private void Start()
        {
            GridConfig gridConfig = gridSystem.GridConfig;

            tiles = new Tile[gridConfig.tileRows, gridConfig.tileCols];
            for (int z = 0; z < gridConfig.tileRows; z++)
            {
                for (int x = 0; x < gridConfig.tileCols; x++)
                {
                    Tile tile = Instantiate(gridConfig.tileTemplate, transform);
                    tiles[z, x] = tile;
                    tile.InitMesh();
                }
            }

            UpdateTilePositions(new IntPos(gridConfig.xSectionStart, gridConfig.zSectionStart));
        }

        public void UpdateTilePositions(IntPos from)
        {
            TopLeft = from;

            for (int z = 0; z < tiles.GetLength(0); z++)
            {
                for (int x = 0; x < tiles.GetLength(1); x++)
                {
                    Tile tile = tiles[z, x];

                    int gridX = x + from.x;
                    int gridY = z + from.y;
                    GridNode gridNode = gridSystem.GetNodeAt(gridX, gridY);

                    tile.SetCenterPoint(new Vector3(gridNode.Position.x, 1, gridNode.Position.z));
                }
            }
        }

        public void UpdateTileVisibility()
        {
            if (!visible) return;

            for (int z = 0; z < tiles.GetLength(0); z++)
            {
                for (int x = 0; x < tiles.GetLength(1); x++)
                {
                    Tile tile = tiles[z, x];

                    int gridX = x + TopLeft.x;
                    int gridY = z + TopLeft.y;
                    GridNode gridNode = gridSystem.GetNodeAt(gridX, gridY);

                    NavMeshHit hit;
                    bool tileVisible = NavMesh.SamplePosition(new Vector3(gridNode.Position.x, 0, gridNode.Position.z), out hit, 0.5f, NavMesh.AllAreas);
                    tile.SetVisible(tileVisible);
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
}
