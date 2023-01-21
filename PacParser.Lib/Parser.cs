using Jint;

namespace PacParser.Lib
{
    public class Parser
    {
        private readonly Engine engine;
        private readonly JsFunctions jsFunctions;

        public string JsConsole => jsFunctions.JsConsole;
        public string DebugOutput => jsFunctions.DebugOutput;

        public Parser()
        {
            jsFunctions = new();
            engine = new Engine()
                    .SetValue("isPlainHostName", new Func<string, bool>(jsFunctions.isPlainHostName))
                    .SetValue("dnsDomainIs", new Func<string, string, bool>(jsFunctions.dnsDomainIs))
                    .SetValue("dnsResolve", new Func<string, string>(jsFunctions.dnsResolve))
                    .SetValue("isInNet", new Func<string, string, string, bool>(jsFunctions.isInNet))
                    .SetValue("isResolvable", new Func<string, bool>(jsFunctions.isResolvable))
                    .SetValue("shExpMatch", new Func<string, string, bool>(jsFunctions.shExpMatch))
                    .SetValue("localHostOrDomainIs", new Func<string, string, bool>(jsFunctions.localHostOrDomainIs))
                    .SetValue("myIpAddress", new Func<string>(jsFunctions.myIpAddress))
                    .SetValue("convert_addr", new Func<string, uint>(jsFunctions.convert_addr))
                    .SetValue("dnsDomainLevels", new Func<string, int>(jsFunctions.dnsDomainLevels))
                    .SetValue("weekdayRange", new Func<string, string, string, bool>(jsFunctions.weekdayRange))
                    .SetValue("dateRange", new Func<string, string, string, string, string, string, string, bool>(jsFunctions.dateRange))
                    .SetValue("timeRange", new Func<string, string, string, string, string, string, string, bool>(jsFunctions.timeRange))
                    .SetValue("alert", new Action<string>(jsFunctions.alert));
        }

        public void Parse(string PAC, string Url, string Host)
        {
            jsFunctions.ClearConsole();
            jsFunctions.ClearDebug();
            jsFunctions.Host = Host;
            engine.Execute(PAC);
            engine.Execute($"alert(FindProxyForURL('{Url}', '{Host}'));");
        }
    }
}