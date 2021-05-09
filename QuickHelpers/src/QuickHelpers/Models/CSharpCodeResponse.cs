using System.Collections.Generic;

namespace QuickHelpers.Models
{
    public class CSharpCodeResponse
    {
        public IReadOnlyCollection<string> Values { get; }

        public CSharpCodeResponse(IReadOnlyCollection<string> values)
        {
            Values = values;
        }
    }
}