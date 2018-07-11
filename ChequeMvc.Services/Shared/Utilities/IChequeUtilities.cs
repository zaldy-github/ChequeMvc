using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeMvc.Services.Shared.Utilities
{
    /// <summary>
    /// This is the interface to the actual implementation of ChequeUtilities.
    /// The interface-segregation principle (ISP) states that no client should be forced to depend on methods it does not use.
    /// ISP splits interfaces that are very large into smaller and more specific ones so that clients will only have to know
    /// about the methods that are of interest to them. Such shrunken interfaces are also called role interfaces.
    /// ISP is intended to keep a system decoupled and thus easier to refactor, change, and redeploy. ISP is one of the five
    /// SOLID principles of object-oriented design. Source: Wikipedia
    /// </summary>
    public interface IChequeUtilities
    {
        string TranslateDollarAmountToWords(decimal? amount);
    }
}
