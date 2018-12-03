using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCSharpSeleniumAutomationTests.factory
{
    [Serializable]
    public class DriverNotFoundException : Exception
    {
        public static readonly string noDriver = "No driver found for specified name";
        public static readonly string noDriverType = "No driver found for specified DRIVER TYPE ";

        public DriverNotFoundException(string message) : base(message) { }
    }
}
