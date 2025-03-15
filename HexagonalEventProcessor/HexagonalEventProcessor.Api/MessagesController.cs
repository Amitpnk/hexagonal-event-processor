using HexagonalEventProcessor.Application;
using HexagonalEventProcessor.Domain.Entities;

namespace HexagonalEventProcessor.Api
{
    // todo: this is for demo purposes only
    [ApiController]
    [Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly MessageProcessorService _messageProcessor;

        public MessagesController(MessageProcessorService messageProcessor)
        {
            _messageProcessor = messageProcessor;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessMessage([FromBody] Message message)
        {
            await _messageProcessor.HandleMessageAsync(message);
            return Ok();
        }
    }

}
