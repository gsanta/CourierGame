
using System.Collections.Generic;
using UnityEngine;

public class PlayerPool : MonoBehaviour
{
    // TODO: custom editor for this field
    public List<PlayerConfig> playerConfigs = new List<PlayerConfig>();

    public void AddPlayerConfig(PlayerConfig playerConfig)
    {
        playerConfigs.Add(playerConfig);
    }

    public List<PlayerConfig> GetAll()
    {
        return playerConfigs;
    }
}
