using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace testCallBackApiBot.Controllers
{
    [Route("api/[controller]")]
    public class CallbackController
    {
        private readonly IConfiguration _configuration;

        public CallbackController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Callback([FromBody] Updates updates)
        {
            // Проверяем, что находится в поле "type" 
            switch (updates.Type)
            {
                // Если это уведомление для подтверждения адреса
                case "confirmation":
                    // Отправляем строку для подтверждения 
                    return Ok(_configuration["Config:Confirmation"]);
                case "message_new":
                    {
                        // Десериализация
                        var msg = Message.FromJson(new VkResponse(updates.Object));

                        // Отправим в ответ полученный от пользователя текст
                        _vkApi.Messages.Send(new MessagesSendParams
                        {
                            RandomId = new DateTime().Millisecond,
                            PeerId = msg.PeerId.Value,
                            Message = msg.Text
                        });
                        break;
                    }
            }
            // Возвращаем "ok" серверу Callback API
            return Ok("ok");
        }

        private readonly IVkApi _vkApi;


        public CallbackController(IVkApi vkApi, IConfiguration configuration)
        {
            _vkApi = vkApi;
            _configuration = configuration;
     
        }
        private IActionResult Ok(string v)
        {
            throw new NotImplementedException();
        }
    }
}
