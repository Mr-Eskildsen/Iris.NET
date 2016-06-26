﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Iris.NET
{
    [Serializable]
    public class IrisSubscribe : IrisPacket, IUserSubmittedPacket
    {
        internal IrisSubscribe(Guid publisherId, string channel) : base(publisherId)
        {
            Channel = channel;
        }

        public string Channel { get; }
    }
}
