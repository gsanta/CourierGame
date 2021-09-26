using Bikers;
using System;

namespace Model
{
    public class BikerCurrentRoleChangedEventArgs : EventArgs
    {

        public BikerCurrentRoleChangedEventArgs(Biker biker)
        {
            Biker = biker;
        }

        public Biker Biker { get; }
    }
}
