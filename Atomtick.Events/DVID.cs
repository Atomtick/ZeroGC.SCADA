using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atomtick.Events
{
    public class DvidDef
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SecsDataType DataType { get; set; }
        public object InitialValue { get; set; }
    }

    public class DvidInstance
    {
        public string Name { get; set; }
        public bool BoolValue { get; set; }
        public long LongValue { get; set; }
        public double DoubleValue { get; set; }
        public string StringValue { get; set; }
    }




    public class DvidManager
    {
        private IDictionary<string, DvidInstance> _dvidInstances;

        public void Register(DVID dvid)
        {

        }

        public void Register(long id, string name, string description, SecsDataType dataType, object initialValue)
        {

        }

        public void Update<T>(DVID dvid, T value)
        {

        }

        public void Update<T>(long dvid, T value)
        {

        }

        public void Update<T>(string name, T value)
        {

        }

        public T Read<T>(DVID dvid)
        {

        }

        public T Read<T>(long dvid)
        {
        }

        public T Read<T>(string name)
        {
        }
    }

}
