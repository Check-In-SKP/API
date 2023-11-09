using CheckInSKP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Events.TokenEvents
{
    public class TokenRevokedEvent : DomainEvent
    {
        public int TokenId { get; }

        public TokenRevokedEvent(int tokenId)
        {
            TokenId = tokenId;
        }
    }
}
