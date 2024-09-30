using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace ProductService.Application.Common.Interfaces;
public interface IRabbitMqConsumerService
{
    void StartAsync();
    void StopAsync();
}
