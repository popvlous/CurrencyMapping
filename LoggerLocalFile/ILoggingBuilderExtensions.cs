using LoggerLocalFile.Models;
using LoggerLocalFile.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LoggerLocalFile
{
    public static class ILoggingBuilderExtensions
    {

        public static void AddLocalFileLogger(this ILoggingBuilder builder, Action<LoggerSetting> action)
        {
            builder.Services.Configure(action);
            builder.Services.AddSingleton<ILoggerProvider, LocalFileLoggerProvider>();
            builder.Services.AddSingleton<IHostedService, LogClearTask>();
        }
    }
}
