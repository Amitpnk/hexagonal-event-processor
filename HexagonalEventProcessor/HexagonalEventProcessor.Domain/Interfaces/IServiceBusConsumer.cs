using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalEventProcessor.Domain.Interfaces
{
    public interface IServiceBusConsumer
    {
        Task ConsumeMessageAsync();
    }
}
