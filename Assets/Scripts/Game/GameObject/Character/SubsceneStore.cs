using GameObjects;
using System.Collections.Generic;

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
        public List<GameCharacter> Enemies { get; set; }
        public List<GameCharacter> Players { get; set; }

        public List<GameCharacter> GetCharacters()
        {
            var all = new List<GameCharacter>();
            all.AddRange(Players);
            all.AddRange(Enemies);

            return all;
        }
    }
}
