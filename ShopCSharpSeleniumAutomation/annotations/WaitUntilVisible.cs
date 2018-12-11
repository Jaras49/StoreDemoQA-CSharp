using System;

namespace ShopCSharpSeleniumAutomation.annotations
{
    /**
 * This annotations marks fields which should be visible before
 * selenium starts manipulating the page.
 */

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class WaitUntilVisible : Attribute
    {
    }
}
