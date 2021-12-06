using GameObjects;
using System;

namespace Model
{
    public class BikerCurrentRoleChangedEventArgs : EventArgs
    {

        public BikerCurrentRoleChangedEventArgs(GameCharacter biker)
        {
            Biker = biker;
        }

        public GameCharacter Biker { get; }
    }
}
