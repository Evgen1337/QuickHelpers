using System;
using System.Collections.Generic;

namespace QuickHelpers.Models
{
    public interface IServiceSettings
    {
        /// <summary>
        ///     Key - Url
        ///     Value - Слово на кнопке
        /// </summary>
        public Dictionary<string, string> Modes { get; }

        public string ServiceName { get; }
    }
}