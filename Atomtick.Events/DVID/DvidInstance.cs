using System.Threading;

namespace Atomtick.Events.DVID
{
    public class DvidInstance
    {
        private bool _boolCurrentValue;
        private long _longCurrentValue;
        private double _doubleCurrentValue;
        private string _stringCurrentValue;

        public DvidInstance(DvidDef dvidDef)
        {
            DvidDef = dvidDef;
        }

        public DvidDef DvidDef { get; }
        public bool BoolCurrentValue
        {
            get { return Volatile.Read(ref _boolCurrentValue); }
            set { Volatile.Write(ref _boolCurrentValue, value); }
        }
        public long LongCurrentValue
        {
            get { return Volatile.Read(ref _longCurrentValue); }
            set { Volatile.Write(ref _longCurrentValue, value); }
        }
        public double DoubleCurrentValue
        {
            get { return Volatile.Read(ref _doubleCurrentValue); }
            set { Volatile.Write(ref _doubleCurrentValue, value); }
        }
        public string StringCurrentValue
        {
            get { return Volatile.Read(ref _stringCurrentValue); }
            set { Volatile.Write(ref _stringCurrentValue, value); }
        }
    }
}
