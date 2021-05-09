using System.Collections.Generic;

namespace QuickHelpers.Models
{
    public class DotNetServiceSettings : IServiceSettings
    {
        public Dictionary<string, string> Modes { get; set; } = new Dictionary<string, string>
        {
            [DotNetServiceMode.ExecuteCode.ToString()] = "Выполниь код"
        };
        
        public string ServiceName { get; } = "DotNet";
    }
}