namespace Atomtick.Events
{
    public class DvidDef
    {
        public DvidDef(long dvid, string name, SecsDataType dataType, object initialValue, string unit, string description)
        {
            Dvid = dvid;
            Name = name;
            DataType = dataType;
            InitialValue = initialValue;
            Unit = unit;
            Description = description;
        }

        public long Dvid { get; }
        public string Name { get; }
        public SecsDataType DataType { get; }
        public object InitialValue { get; }
        public string Unit { get; }
        public string Description { get; }
    }
}
