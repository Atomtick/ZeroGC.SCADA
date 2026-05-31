using System;
using System.Collections.Generic;
using System.Text;

namespace SCADA.PLCFramework
{
    public interface IInterlockChecker
    {
        IReadOnlyDictionary<InterlockAction, InterlockLimit[]> GetAllActions();
        IEnumerable<InterlockAction> GetAllInterLockActions();
        IEnumerable<InterlockAction> GetAllBypassActions();
        IEnumerable<InterlockLimit> GetAllLimits(InterlockAction action);
        IEnumerable<InterlockLimit> GetAllInterLockLimits(InterlockAction action);
        IEnumerable<InterlockLimit> GetAllBypassLimits(InterlockAction action);

        void BypassInterLock(InterlockAction action);
        void BypassInterLock(InterlockAction action, InterlockLimit limit);
        void RestoreInterlock(InterlockAction action);
        void RestoreInterlock(InterlockAction action, InterlockLimit limit);

        IEnumerable<string> Monitor();
        bool CanDO(InterlockAction action, out InterlockLimit interlockLimit);
    }
}
