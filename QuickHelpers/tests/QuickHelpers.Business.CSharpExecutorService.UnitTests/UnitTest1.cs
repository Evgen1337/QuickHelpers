using System;
using System.Threading.Tasks;
using Xunit;

namespace QuickHelpers.Business.CSharpExecutorService.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var parseLogic = new CSharpRuntimeExecutor();

            var result1 = parseLogic.ExecuteAsync(@"
Return(2 + 2);
").Result;
            
            
            var result = parseLogic.ExecuteAsync(@"
while(true){}
").Result;
            
        }
    }
}