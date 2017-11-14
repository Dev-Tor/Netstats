using System;

namespace Netstats.Core
{
    //===============================================================================
    // Copyright © Edosa Kelvin. All rights reserved.
    // THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
    // OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
    // LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
    // FITNESS FOR A PARTICULAR PURPOSE.
    //===============================================================================

    public struct SessionFeed : IEquatable<SessionFeed>
    {
        public double Total { get; set; }

        public double Used { get; set; } 

        public double Download { get; set; } 

        public double Upload { get; set; } 

        public bool Equals(SessionFeed other)
        {
            return Download == other.Download && Upload == other.Upload
                    && Total == other.Total && Used == other.Used;
        }
    }
}
