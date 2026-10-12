using System.Text;
using Microsoft.Extensions.DependencyInjection;
using VyshKat.ConsoleApp;
using VyshKat.Core.Services;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var services = new ServiceCollection();

services.AddSingleton<Park>();
services.AddSingleton<IServiceCenter, ServiceCenter>();
services.AddSingleton<ParkService>();
services.AddSingleton<ParkReports>();
services.AddSingleton<FileImporter>();
services.AddSingleton<ConsoleMenu>();

using var provider = services.BuildServiceProvider();

provider.GetRequiredService<ConsoleMenu>().Run();Console.WriteLine("Hello, World!");
