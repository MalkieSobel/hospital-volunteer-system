

using DalApi;
using System;

namespace Helpers
{
    internal class CallManager
    {
        private static IDal s_dal = Factory.Get; //stage 4
        internal static ObserverManager Observers = new(); //stage 5 

    }
}
