using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace DSA.Interfaces
{
    public interface IHash
    {
        string GetHash(string rawText);
    }

    interface IInternalHash
    {
        string AppendPadding(string unPaddedBinaries);
    }
}
