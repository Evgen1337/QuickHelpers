using System;
using System.Linq;

namespace SampleNamespace
{
    static class SampleClass
    {
        private static object _valueToReturn;
        
        public static void SampleMethod()
        {
            void Return<T>(T result)
            {
                _valueToReturn = result;
            }
            
            //CodeHere
        }
    }
}