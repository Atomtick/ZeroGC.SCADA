namespace Atomtick.Events
{
    public class DvidInstance
    {
        public DvidDef DvidDef { get; set; }
        public volatile bool BoolCurrentValue;
        public long LongCurrentValue;
        public double DoubleCurrentValue;
        public volatile string StringCurrentValue;
    }
}
