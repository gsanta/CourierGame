﻿using System;

namespace Service
{
    public class PackageStatusChangedEventArgs : EventArgs
    {
        private readonly Package package;

        public PackageStatusChangedEventArgs(Package package)
        {
            this.package = package;
        }

        public Package Package { get => package; }
    }
}
