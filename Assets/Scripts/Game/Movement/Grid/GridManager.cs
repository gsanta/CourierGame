using UnityEngine;

namespace Movement
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField]
        private int width;
        [SerializeField]
        private int height;
        [SerializeField]
        private GameObject lightTile;
        [SerializeField]
        private GameObject darkTile;
        [SerializeField]
        private int tileSize;

        private void Awake()
        {
            int startX = -Mathf.FloorToInt(width / 2f);
            int startZ = -Mathf.FloorToInt(height / 2f);
            int counter = 0;

            for (int x = startX; x < width; x++)
            {
                for (int z = startZ; z < height; z++)
                {
                    CreateQuad(x, z, counter % 2 == 0);

                    counter++;
                }
            }
        }

        private void CreateQuad(int x, int z, bool dark)
        {
            Vector3 parentPos = transform.position;
            GameObject tile = dark ? darkTile : lightTile;
            float y = tile.transform.position.y;
            var gameObject = Instantiate(dark ? darkTile : lightTile, transform);
            gameObject.transform.position = new Vector3(x * tile.transform.localScale.x + parentPos.x, y + 1, z * tile.transform.localScale.z + parentPos.z);
            gameObject.SetActive(true);
        }
    }
}
