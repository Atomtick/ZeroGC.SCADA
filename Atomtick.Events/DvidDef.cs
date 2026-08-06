// 事件抛出时,会同时抛出一些值,这些值是有含义的,把值和含义打包起来构成对象,然后抽象出一个类,
// 这个类就是用于实例化出一个指明含义的值!
// 支持的含义是有限的,可以把支持的含义全部预先定义好,然后在事件抛出时,根据需要选择使用哪一个含义,并且把值和含义打包成对象,然后抛出这个对象!
// 相当于支持的字典KEY.
// CEID Link 的 DVID,相当于函数参数, 指明了携带的值的数量,顺序,魔法数值的类型和含义, 这些都是在定义时就已经确定好的,所以在事件抛出时,只需要根据定义好的 DVID 来携带值即可,不需要再去考虑值的含义和类型,因为这些都是在定义时就已经确定好的!
// CEID + LinkID, 相当于: 函数名是事件名称, DVID是形参列表.  抛到EAP的数值必须数量,类型一致.
// DVID数字 相当于 类型与含义的乘积的映射, 比如类型有 int double, bool, string, 含义有 温度,压力,流量,电压,电流,频率,时间戳,状态码,错误码,等等. 这些类型与含义的组合是有限的,所以可以预先定义好这些组合的映射关系,然后在事件抛出时,根据需要选择使用哪一个组合,并且把值和含义打包成对象,然后抛出这个对象!
// 一个DVID数字,包含两个意思: 1. 类型, 2. 含义. 这两个意思是通过一个数字来表示的,所以在事件抛出时,只需要根据定义好的 DVID 来携带值即可,不需要再去考虑值的含义和类型,因为这些都是在定义时就已经确定好的!
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
