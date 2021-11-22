using Bikers;
using System;

namespace Model
{
    public class BikerCurrentRoleChangedEventArgs : EventArgs
    {

        public BikerCurrentRoleChangedEventArgs(Player biker)
        {
            Biker = biker;
        }

        public Player Biker { get; }
    }
}
