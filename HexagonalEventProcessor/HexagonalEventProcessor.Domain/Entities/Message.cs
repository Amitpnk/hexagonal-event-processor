using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalEventProcessor.Domain.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public string Payload { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
