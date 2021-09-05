
using System;

namespace Domain
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
