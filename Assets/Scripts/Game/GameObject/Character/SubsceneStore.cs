using GameObjects;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{

    public enum SubsceneType
    {
        BATTLEFIELD,
        BUILDING
    }

    public class SubsceneStore
    {
        public SubsceneType Type { get; set; }
        public string SubSceneId { get; set; }
        private List<GameCharacter> characters = new List<GameCharacter>();

        private Dictionary<GameCharacter, Vector3> origPositions = new Dictionary<GameCharacter, Vector3>();

        public void AddCharacter(GameCharacter character)
        {
            characters.Add(character);
            origPositions.Add(character, character.transform.position);
        }

        public void Clear()
        {
            characters.Clear();
            origPositions.Clear();
        }

        public Vector3 GetOrigPosition(GameCharacter character)
        {
            return origPositions[character];
        }

        public List<GameCharacter> GetCharacters()
        {
            return characters;
        }
    }
}
