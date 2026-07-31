namespace Atomtick.Events
{
    public class DvidDef
    {
        public DvidDef(long dvid, string name, string description, SecsDataType dataType, object initialValue, string unit)
        {
            Dvid = dvid;
            Name = name;
            Description = description;
            DataType = dataType;
            InitialValue = initialValue;
            Unit = unit;
        }

        public long Dvid { get; }
        public string Name { get; }
        public string Description { get; }
        public SecsDataType DataType { get; }
        public object InitialValue { get; }
        public string Unit { get; }
    }
}
