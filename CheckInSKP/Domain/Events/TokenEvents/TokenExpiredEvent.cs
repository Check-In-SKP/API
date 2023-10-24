using CheckInSKP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Events.TokenEvents
{
    public class TokenExpiredEvent : DomainEvent
    {
        public int TokenId { get; }

        public TokenExpiredEvent(int tokenId)
        {
            TokenId = tokenId;
        }
    }
}
