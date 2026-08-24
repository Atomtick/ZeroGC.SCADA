# TODO

- IConfigReader

# 解决了什么问题

Atomtick.Configuration的核心价值是

1. 支持原子批量读写 
2. 读操作零GC 
3. 纳秒级性能

下面是BenchmarkDotNet的测试报告, 该报告说明: 原子性批量读取16个配置项, 仅耗时`47.966 ns`, 相当于执行两次将一个字符串转换成数字的时间, 速度极快, 同时`满足零GC`!

```

BenchmarkDotNet v0.15.8, Windows 10 (10.0.19045.6466/22H2/2022Update)
12th Gen Intel Core i7-1260P 2.10GHz, 1 CPU, 16 logical and 12 physical cores
.NET SDK 10.0.301
  [Host]     : .NET 10.0.9 (10.0.9, 10.0.926.27113), X64 RyuJIT x86-64-v3
  Job-YGNLVQ : .NET 10.0.9 (10.0.9, 10.0.926.27113), X64 RyuJIT x86-64-v3

Server=False  WarmupCount=5  

```
| Method              |        Mean |     Error |    StdDev | Ratio | RatioSD | Allocated | Alloc Ratio |
| ------------------- | ----------: | --------: | --------: | ----: | ------: | --------: | ----------: |
| ParseStringToDouble | `27.128 ns` | 0.3929 ns | 0.3675 ns |  1.00 |    0.02 |       `-` |          NA |
| HashSearch          | `23.654 ns` | 0.4555 ns | 0.4260 ns |  0.87 |    0.02 |       `-` |          NA |
| Read16Items         | `47.966 ns` | 0.1554 ns | 0.1378 ns |  1.77 |    0.02 |       `-` |          NA |
| ReadOneItem         |  `4.467 ns` | 0.0167 ns | 0.0148 ns |  0.16 |    0.00 |       `-` |          NA |





- **跨平台**
  - 支持 Windows, Linux, MacOS
  - 支持 .NET Framework 4.6.2 & .NET 12.

- **支持修改**
  - .NET Framework 和 .NET Core 内置的配置系统, 仅对读取操作友好, 但是对程序运行中修改配置操作支持差劲, 不适合在工业软件中, 用户需要频繁修改配置以完成装机调试, 工艺验证, 流程管控等场景.


- **支持原子批量读写**

  保证单次批量读取的所有配置来自同一个快照, 批量写入的配置要么同时成功要么全部失败, `严格满足一致性, 无数据撕裂`. 

  在工业软件中, 配置的批量操作原子性如同PLC在一个扫描周期内IO来自同一快照, 一起写入物理地址那样重要, 否则就会导致诡异和难以复现的偶发宕机.

  举例: 

  1. MFC的流量在Tolerance徘徊Time则报警,如果调试时, Tolerance的修改先生效,程序内部可能出现在一个监控周期使用Tolerance新值Time旧值的情况,导致设备报警!
  2. 矛盾的多个配置关系

- **零GC & 高性能**

  1. 采用SeqLock机制实现读无锁, 软件不会因为高频读取配置导致性能抖动或下降.

  2. 自研Object转数字类型时, 在检查溢出和精度损失的同时, 不发生任何装箱, 速度接近C#类型强转.
  3. 提前查好Value,在热路径避免哈希查找, 极其高频调用连哈希计算的时间都省去了.

  3. 使用结构体和栈内存, 避免堆内存压力导致GC Stop-World.
  4. 纳秒级抖动和耗时

- **引入值校验机制, 防御性编程**

  regex, options, type

- **结构文档, 便于组织配置, Sqlite, 写无损.**

- 字符串性配置,无需编译, 灵活配置, 完全符合半导体行业设备和软件逐步迭代的需求.

- 支持导出GEM模型中的ECID集合.

- **存储修改历史**

  此功能可选择是否开启.

  若开启, 每一次修改都会被严格记录, 可用于追溯和审计, 帮助定位客户现场事故原因.

​		

# Design Idea

## ConfigValue

IConfigValue和IConfigItem的目的是配合IConfigSource暴露给用户,用户在使用时,可以拿到接口进而读写配置,但是接口内无具体实现,既保密了代码,也能让用户自己实现IConfigSource,也能使用SCADA提供的实现.

ConfigItem包含一个配置的所有信息,包括当前值,是单例的.

ConfigValue只是暂存的,拿到后拷贝ConfigItem,能够临时变量.

ConfigValue是值类型, 疯狂返回时, 不会有GC压力.



## 校验流程

**校验的相关元素**

​	min, max, regex, options, CustomizeOptions, AppendValidationRule.

**校验顺序**

1. 值字符串是否可以转换成相应的类型
2. 如果是数字类型, 是否超出min和max范围
3. 是否是options集合中的某一元素
4. 值字符串是否满足regex正则表达式
5. 是否满足AppendValidationRule

**检查位置**

	- initial_value, 加载XML后校验初始值
	- options, 加载XML后校验options的每一个元素
	- current_value, 校验从数据表读取到的当前值
	- new_value, 修改配置项的值时检验新值



> 不需要对min和max校验, 因为它并不是配置项的值, 只是指定极值范围.

# User Manual

1. 支持读取和修改配置
2. 支持原子性批量读取或修改配置
3. 顺序锁机制保证原子操作杜绝撕裂读，读性能极高
4. 支持UI





## Motivation and Function

**.net framework app.config**

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
    </startup>
    <appSettings>
        <add key="CycleCount" value="365" />
        <add key="IsSimulatorMode" value="false" />
        <add key="DiskFreeSpaceAlarmTolerance" value="6.18" />
        <add key="RemoteIpAddress" value="127.0.0.1" />
    </appSettings>
</configuration>
```

**缺点**

- 只支持读配置，不支持修改。.NET Framework 程序**在技术上是支持**修改自身的 `app.config` 文件的，但这通常几乎不推荐，`app.config` 文件被设计为存储相对静态的、随应用程序部署的配置信息，例如数据库连接字符串、服务终结点等，而不是用来存储频繁变化的用户数据或运行时状态。

- 结构简单，容易Key重复。如果配置项数量太多，多达几百甚至上千项，很容易导致Key重复。

  ```xml
  <?xml version="1.0" encoding="utf-8" ?>
  <configuration>
      <startup> 
          <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
      </startup>
      <appSettings>
          <add key="System.CycleCount" value="365" />
          <add key="System.IsSimulatorMode" value="false" />
          <add key="System.Setup.DiskFreeSpaceAlarmTolerance" value="6.18" />
          <add key="System.Setup.RemoteIpAddress" value="127.0.0.1" />
      </appSettings>
  </configuration>
  ```

  以`.`延长Key长，虽然在一定程度上避免了Key重复问题，但是结构不易调整。

**PrimitiveConfigSource XML**

```xml
<?xml version="1.0" encoding="utf-8"?>
<root>
	<config name="System" >
		<config name="CycleCount" value="3" type="Integer" />
		<config name="IsSimulatorMode" value="false" type="Boolean" />
		<config name="SetUp" >
			<config name="DiskFreeSpaceAlarmTolerance" value="5" type="Decimal" />
			<config name="RemoteIpAddress" value="127.0.0.1"  type="String" />
		</config>
	</config>
</root>
```

- 不仅支持读配置，也支持`修改配置`
- 高性能写操作。SetValue只要修改完内存中的值立刻返回即刻生效，后台的生产者消费者线程‘默默’写磁盘(.NetFramework4.6.2和.NET6.0，使用Channel`不空占线程`,开销几乎忽略不计)
- 树状结构，很容易避免Key重复，且`容易调整config的位置和Key的索引路径`
- 可以在XML中添加额外的数据类型限定，在程序中自动进行类型转换，能够在程序员使用错误的期望类型读写配置时，抛出异常，满足`防御性编程`。
- 可以添加`校验规则`，如最大值最小值限制，正则表达式校验，限制到可允许的取值集合
- 支持跨平台: Windows, MacOS, Linux
- 适合配置的结构、内容或键名在编码时无法预知，或者需要被程序动态处理，尤其是自动化行业上位机，需要同一个软件兼容多种机型的场景，`避免维护多个分支和软件版本`.

## Quick Start

### XML File Example

**system.xml**

```xml
<?xml version="1.0" encoding="utf-8"?>
<root>
	<config name="System" >
		<config name="CycleCount" value="3" type="Integer" />
		<config name="IsSimulatorMode" value="false" type="Boolean" />
		<config name="SetUp" >
			<config name="DiskFreeSpaceAlarmTolerance" value="5" type="Decimal" />
			<config name="RemoteIpAddress" value="127.0.0.1"  type="String" />
            <config name="LogsFolder" value="C:\Logs"  type="Folder" />
            <config name="DataReport" value="C:\data.csv"  type="File" />
            <config name="AlarmLight" value="#FFFFFF"  type="Color" />
            <config name="ResetDate" value="2025-05-06 08:00:00"  type="DateTime" />
		</config>
	</config>
</root>
```

### Supported Types

- Boolean
- Integer
- Decimal
- String
- Folder
- File
- Color
- DateTime

> Folder,File,Color,DateTime属于非核心type，算是额外拓展的边缘类型，虽然完全可以用String替代，但是这样做的好处是将来做控件来修改XML配置项的值，Folder可以标记弹出文件夹选择对话框，DateTime可以弹出日期选择器，但如果全是String，只能采用简陋的文本框输入路径，颜色，日期，既麻烦也易输入错误，此外，标记成Folder,File,Color,DateTime，PrimitiveConfigSource内部会对Value字符串的格式进行校验检查，避免流入非法字符串。

`当然，如果你明确将来不会通过可视化界面修改某个XML文件的值，那么完全可以不适用Folder，File，Color，DateTime这四种类型，只用String平替即可。`

### Load XML File

```c#
var source = new PrimitiveConfigSource("system.xml",Encoding.UTF8);
```

PrimitiveConfigSource构造函数传入XML文件路径和文件编码。

> 需要保证一个XML文件同时只能有一个PrimitiveConfigSource对象持有，因为单个对象能保证改配置项值时写操作是线程安全的，但多个对象同时写时不是线程安全的。

### How to read multiple configs atomically

#### 读取单个配置

```C#
// 第一步: 使用配置名称字符串索引到配置项
var isSimulatorMode_i = _configSource.Select("System.IsSimulatorMode");
// 第二步: 拿到配置的值快照
var simulatorMode_v = _configSource.Read(isSimulatorMode_i);
// 第三步: 解析快照拿到基元值
var simulatorMode = simulatorMode_v.ToBool(true);
// 再次读取配置最新的值(不需要重新用字符串Select ConfigItem)
simulatorMode_v = _configSource.Read(isSimulatorMode_i);
simulatorMode = simulatorMode_v.ToBool(true);
```

#### 读取多个配置

`System.IsSimulatorMode 和 System.CycleCount 来自同一快照, 满足一致性和原子性`

```C#
// 第一步: 使用配置名称字符串索引到配置项
var isSimulatorMode_i = _configSource.Select("System.IsSimulatorMode");
var cycleCount_i = _configSource.Select("System.CycleCount");
// 第二步: 拿到配置的值快照
(var simulatorMode_v, var cycleCount_v) = _configSource.Read(isSimulatorMode_i, cycleCount_i);
// 第三步: 解析快照拿到基元值
var simulatorMode = simulatorMode_v.ToBool(true);
var cycleCount = cycleCount_v.ToInt32();
// 再次读取配置最新的值(不需要重新用字符串Select ConfigItem)
(simulatorMode_v,cycleCount_v) = _configSource.Read(isSimulatorMode_i, cycleCount_i);
simulatorMode = simulatorMode_v.ToBool(true);
cycleCount = cycleCount_v.ToInt32();
```

#### 读取配置的最佳实践

```C#
public class TransferModule
{
    private readonly IConfigReader _configReader;
    private readonly ConfigItem _homeTimeout;
    private readonly ConfigItem _maxPressureDiffOpenSlitValve;
    private readonly ConfigItem _atmPressureBase;
    private readonly ConfigItem _vacuumPressureBase;
    private readonly ConfigItem _robotIp;
    private readonly ConfigItem _robotPort;

    public TransferModule(IConfigReader configReader)
    {
        _configReader = configReader;
        _homeTimeout = _configReader.Select("TM.HomeTimeout");
        _maxPressureDiffOpenSlitValve = _configReader.Select("TM.MaxPressureDiffOpenSlitValve");
        _atmPressureBase = _configReader.Select("TM.AtmPressureBase");
        _vacuumPressureBase = _configReader.Select("TM.VacuumPressureBase");
        _robotIp = _configReader.Select("TM.RobotIP");
        _robotPort = _configReader.Select("TM.RobotPort");
    }

    public void Init()
    {
        var configValues = _configReader.Read(_robotIp, _robotPort);
        var ip = configValues.Item1.ToString();
        var port = configValues.Item2.ToInt32();
        Connect(ip, port);
    }

    public void Home()
    {
        var configValues = _configReader.Read(_homeTimeout, _atmPressureBase, _vacuumPressureBase, _maxPressureDiffOpenSlitValve);
        int homeTimeout = configValues.Item1.ToInt32();
        var atmPressureBase = configValues.Item2.ToDouble();
        var vacuumPressureBase = configValues.Item3.ToDouble();
        var maxPressureDiffOpenSlitValve = configValues.Item4.ToDouble();
    }

    private void Connect(string ip, int port)
    {
        // ......
    }
}
```

- 类的构造函数注入IConfigReader，此接口只暴露读取配置的方法。PrimitiveConfigSource只适合读极多写极少的场景，IConfigReader可以约束用户在类中滥用写操作。
- 类要用到的所有配置的ConfigItem全部定义成字段，在构造函数中通过Select完成字段的赋值。这样做有两大优点：1. 代码可读性好 2. 类中读取配置最新值时访问ConfigItem引用即可，无需借助字符串，可以避免用户重复的进行不必要的字符串拼接导致GC。
- 可以把程序启动后不会再被修改的配置项的基元值直接存储成字段，在构造函数中完成初始化，后续直接访问字段即可，这种静态读取比透过顺序锁的冷读取快的多。

#### ConfigValue转换成基元值的方法

**ToXXX()   ToXXX(xxx)   TryToXXX(out xxx)**

```c#
// 如果未配置此项，抛出异常
bool isEFEMInstalled = _isEFEMInstalled.ToBool();
// 如果未配置此项，不会抛出异常，而是返回false。函数参数是默认值
bool isEFEMInstalled = _isEFEMInstalled.ToBool("false");
// 返回值表示是否含有此配置项，函数的out参数是配置项的值
bool isPresent = _isEFEMInstalled.TryToBool(out bool isEFEMInstalled);
```



**Type Mapping Table** 

| Config Type |                     .NET Type                      |
| :---------: | :------------------------------------------------: |
|    Bool     |                        bool                        |
|   Integer   | long  ulong  int  uint  short  ushort  byte  sbyte |
|   Decimal   |                   double  float                    |
|   String    |                       string                       |
|    Color    |                System.Drawing.Color                |
|  DateTime   |                      DateTime                      |
|   Folder    |                   DirectoryInfo                    |
|    File     |                      FileInfo                      |



> PrimitiveConfigSource后台用long存储`Integer`类型的配置项的值，ToXXX()的本质是将long类型转换成XXX类型。框架的ToXXX()允许XXX是任意整数类型，它的内部自动进行类型转换，但需要注意，若转换发生溢出，会抛出异常。



> PrimitiveConfigSource后台用double存储`Decimal`类型的配置项的值，ToDouble()没什么需要说的，ToSingle()的本质是将double类型转换成float类型, 若转换发生溢出或精度损失，会抛出异常。举例：对于"3.1415926535897"， ToSingle()时会因为精度损失而抛出异常。



> ConifgValue.ToXXX()，XXX必须满足mapping table，否则发生异常！举例：`<config name="DiskFreeSpaceAlarmTolerance" value="5" type="double" />` ，如果 configValue.ToDouble(), configValue.ToFloat()是OK的，但是configValue.ToInt()，configValue.ToBool()会抛出异常。



> 任何ConfigType都允许ToString() !  `<config name="Timeout" value="0x0A" type="Integer" />` , ToInt32()得到的结果是10，ToString()得到的结果是"0X0A".



> 复杂数据可以全部配置成String，程序使用时先转换成string，再把string二次转换成复杂数据。

### 批量修改单个或多个配置项(原子操作)

```c#
var configSource = new PrimitiveConfigSource("configs.db");

configSource.BeginTransaction(out long transactionId);

configSource
    .Write(transactionId, "Cylinder.Timeout", 5 * 1000)
    .Write(transactionId, "EAP.IP", "192.168.1.29")
    .Write(transactionId, "Log.Enable", true)
    .Write(transactionId, "FlowRate.Tolerance", 1.5)
    .Write(transactionId, "Alarm.Color", System.Drawing.Color.Red)
    .Write(transactionId, "Data.Folder", "C:\\Logs");

configSource.CommitTransaction(transactionId);
```

- 多个配置项要么都被成功修改, 要么都维持不变, 满足原子操作
- 只有新值与旧值不相等才会触发修改动作, 所以如果多次Write相同值几乎没有开销
- 新值和旧值判断相等的规则是比较字符串形式的值, 如 旧值是 "16", 新值是 "0x10" , 虽然都表示十进制的数字16, 但仍旧被判定为新值与旧值不等.

#### type="Bool"

```c#
configSource.Write(transactionId,"System.IsSimulatorMode", true);
```

```c#
configSource.Write(transactionId,"System.IsSimulatorMode", false);
```

```c#
configSource.Write(transactionId,"System.IsSimulatorMode", "fAlsE");
```

```c#
configSource.Write(transactionId,"System.IsSimulatorMode", "TRUE");
```

#### type="Integer"

```c#
configSource.Write(transactionId,"System.CycleCount", 13);
```

```c#
configSource.Write(transactionId,"System.CycleCount", "-13");
```

```c#
configSource.Write(transactionId,"System.CycleCount", 13.0);
```

```c#
configSource.Write(transactionId,"System.CycleCount", "13.0");
```

```c#
configSource.Write(transactionId,"System.CycleCount", 0xA2);
```

```c#
configSource.Write(transactionId,"System.CycleCount", "0xA2");
```

```c#
configSource.Write(transactionId,"System.CycleCount", "0XA01");
```

```c#
configSource.Write(transactionId,"System.CycleCount", 123,456);
```

```c#
configSource.Write(transactionId,"System.CycleCount", 12,34,56);
```

```c#
configSource.Write(transactionId,"System.CycleCount", "123,456");
```

#### type="Decimal"

```c#
configSource.Write(transactionId,"System.SetUp.DiskFreeSpaceAlarmTolerance", -23.01);
```

```c#
configSource.Write(transactionId,"System.SetUp.DiskFreeSpaceAlarmTolerance", -1,234.61);
```

```c#
 configSource.Write(transactionId,"System.SetUp.DiskFreeSpaceAlarmTolerance", "34.55");
```

```c#
configSource.Write(transactionId,"System.SetUp.DiskFreeSpaceAlarmTolerance", -69.8e3);
```

```c#
configSource.Write(transactionId,"System.SetUp.DiskFreeSpaceAlarmTolerance", "-69.8e3");
```

```c#
configSource.Write(transactionId,"System.SetUp.DiskFreeSpaceAlarmTolerance", 23);
```

```c#
configSource.Write(transactionId,"System.SetUp.DiskFreeSpaceAlarmTolerance", "0XA01");
```

```c#
configSource.Write(transactionId,"System.SetUp.DiskFreeSpaceAlarmTolerance", 0xA2);
```

```c#
configSource.Write(transactionId,"System.SetUp.DiskFreeSpaceAlarmTolerance", "0xA2");
```

#### type="String"

可以是任意字符串，包括空字符串，也可以是任意数据类型，比如下面的23，会自动调用其ToString()。只要不是null都可以。

```c#
configSource.Write(transactionId,"System.SetUp.RemoteIpAddress", "");
```

```c#
configSource.Write(transactionId,"System.SetUp.RemoteIpAddress", "hello");
```

```c#
configSource.Write(transactionId,"System.SetUp.RemoteIpAddress", 23);
```

#### type="DateTime"

```c#
configSource.Write(transactionId,"System.ResetDate", DateTime.Now);
configSource.Write(transactionId,"System.ResetDate", "2025-8-4");
```

#### type="Color"

```c#
configSource.Write(transactionId,"System.AlarmLight", "#000000CC");
configSource.Write(transactionId,"System.AlarmLight", "#0000CC");
configSource.Write(transactionId,"System.AlarmLight", System.Drawing.Color.Red);
```

#### type="Folder"

*支持绝对路径和相对路径*

*相对路径时, 当前目录是程序根目录*

```c#
configSource.Write(transactionId,"System.LogsFolder", "D:\\");
configSource.Write(transactionId,"System.LogsFolder", "D:\\Logs");
configSource.Write(transactionId,"System.LogsFolder", "../Logs");
```

#### type="File"

*支持绝对路径和相对路径*

*相对路径时, 当前目录强制设置成程序根目录*

```c#
configSource.Write(transactionId,"System.DataReport", "D:\\data.xlsx");
configSource.Write(transactionId,"System.DataReport", "../../data.xlsx");
```

> 每次调用CommitTransaction都会写一次数据库。如果有多个修改，单次批量提交性能更高开销更小，且可以保证只要有一项校验失败，则全部的设置项都不会被修改，即原子操作。

## 校验

### 校验代码

```c#
IConfigValidator configSource = new PrimitiveConfigSource("configs.db");

// 方式一: 校验失败会抛出异常
configSource.ValidateValue("FA.LocalPortNumber", "1000");

// 方式二: ok是true表示校验通过,false表示校验失败,errorMessage是失败原因.
var ok = configSource.ValidateValue("FA.LocalPortNumber", "1000", out string errorMessage);
```

> 高频校验场景请使用方式二, 因为方式一频繁抛出异常会严重影响性能.

### 校验流程

- 类型校验
  - 值都是字符串类型. 值字符串必须满足可以转换成配置项的type指定的类型. 如"3.14"肯定无法转换成Integer, "#AABBCC"肯定无法转换成DateTime.

- 集合校验
  - String, Integer, Decimal, Color才有此项校验
  - Integer 和 Decimal 比较特殊.首先,options内的所有元素和待校验的值都是字符串,它会先统一的把Options里面的元素以及待校验的值全部转换成数字类型(long或double),然后再检查转换后的集合是否包含转换后的待校验值. 举例: 假设string[] options=["1", "0x02", "3.14E2"], 待校验值是"2", 则校验过程是[1,2,314].Contains(2),结果是包含! 这样做更智能,避免了同一个数字因为不同的字符串表示被判断成不相等的情况.
- 最值校验
  - 如果大于最大值或小于最小值, 则校验失败.
  - Integer和Decimal才有此项校验, 其他类型无.
- 正则校验
  - Bool无此校验,其他类型有.
  - String, Folder, File, DateTime, Color的字符串形式直接进行正则表达.
  - Integer和Decimal先统一转换成十进制字符串形式再进行正则表达. (举例: 如果是十六进制如'0X0A', 那么进行正则表达校验的实际字符串是'10').

- AppendedValidationRule. 

### 校验位置

Atomtick.Configuration Library 内部在3个位置调用校验函数进行校验.

1. 初始化时,对intial_value校验.
2. 初始化时,对持久化的current_value校验.
3. 初始化时,对options所有子元素校验.
4. Write函数修改配置项的值时对新值校验.

## ValueSet

调用SetValue( )会触发ValueSet事件，事件参数是被修改的配置项，旧值，新值。

主要作用是日志追溯。

```c#

source.ValueSet += Source_ValueSet;

// oldValue和newValue两个字符串肯定不同。因为SetValue()检测到新值和旧值相等的情况下会提前返回，不会真的去修改值，更不会触发ValueSet事件。
// by the way,old value可能是10，new value是0x0A,虽然都表示十进制的值，但仍旧会触发事件，因为比较规则是比较数据项的值的文本形式。
private void Source_ValueSet((string configItem, string oldValue, string newValue)[] obj)
{
    foreach (var item in obj)
    {
        Console.WriteLine($"{item.configItem} changed from {item.oldValue} to {item.newValue}.");
    }
}
```

## Valid XML Schema Specification

### How to edit xml

xml文件固定初始模板如下，必须要有根节点root。

```xml
<?xml version="1.0" encoding="utf-8"?>
<root>

</root>
```

可以添加配置项`System.CycleCount`

```c#
<?xml version="1.0" encoding="utf-8"?>
<root>
	<config name="System">
		<config name="CycleCount" value="3" type="Integer"/>
	</config>
</root>
```

下面的配置是==无效==的。root节点下必须是分类节点！

```xml
<?xml version="1.0" encoding="utf-8"?>
<root>
	<config name="CycleCount" value="3" type="Integer"/>
</root>
```

root下可以有多个分类节点。下面有两个配置项`System.CycleCount`和`SetUp.RemoteIpAddress`

```xml
<?xml version="1.0" encoding="utf-8"?>
<root>
	<config name="System">
		<config name="CycleCount" value="3" type="Integer"/>
	</config>
    <config name="SetUp">
		<config name="RemoteIpAddress" value="127.0.0.1" type="String"/>
	</config>
</root>
```

可以自由调整数据节点的路径。数据项RemoteIpAddress的路径被调整成`SetUp.Address.RemoteIpAddress`

```xml
<?xml version="1.0" encoding="utf-8"?>
<root>
	<config name="System">
		<config name="CycleCount" value="3" type="Integer"/>
	</config>
    <config name="SetUp">
        <config name="Address">
            <config name="RemoteIpAddress" value="127.0.0.1" type="String"/>
        </config>
	</config>
</root>
```

同一分类节点下的分类节点的name不能相同，必须保证唯一；数据节点之间的name也不能相同。



错误范例1：root下有2个System

```xml
<?xml version="1.0" encoding="utf-8"?>
<root>
	<config name="System">
		<config name="CycleCount" value="3" type="Integer"/>
	</config>
    <config name="System">
		<config name="RemoteIpAddress" value="127.0.0.1" type="String"/>
	</config>
</root>
```



错误范例2：System下有2个CycleCount

```xml
<?xml version="1.0" encoding="utf-8"?>
<root>
	<config name="System">
		<config name="CycleCount" value="3" type="Integer"/>
        <config name="CycleCount" value="127.0.0.1" type="String"/>
	</config>
</root>
```



正确范例3：System下有2个CycleCount

```c#
<?xml version="1.0" encoding="utf-8"?>
<root>
	<config name="System">
		<config name="CycleCount" value="3" type="Integer"/>
        <config name="CycleCount">
            <config name="RemoteIpAddress" value="127.0.0.1" type="String"/>
	    </config>
	</config>
</root>
```



正确范例4：CycleCount节点下有一个CycleCount，这是允许的。

```xml
<?xml version="1.0" encoding="utf-8"?>
<root>
	<config name="System">
        <config name="CycleCount">
            <config name="RemoteIpAddress" value="127.0.0.1" type="String"/>
            <config name="CycleCount" value="3" type="Integer"/>
	    </config>
	</config>
</root>
```

==终极原则是：同一个分类节点下的直接子分类节点的Name不能重复，子数据节点的Name不能重复，进而保证每一个数据项的路径在全部数据项中都是唯一的。==

## Node

> 红色标记的属性是必须配置的意思，必须在xml中config指定该属性且值不能是空白。可选配置的意思是可以在xml的config不写该属性或值是空白字符。
>
> 橙色标记的属性是可选配置，用于校验和限定配置项的取值，根据需求决定是否配置。
>
> 绿色标记的属性是可选配置，用于做可视化修改值的UI时使用的，如果只是在后台使用简单的读写功能，则无需理会这些属性。
>

### Category node

`<config name="System" display="系统" visible="true" enable="true" />`

| Attribute                         | Description                                    | Default Value                       |
| --------------------------------- | ---------------------------------------------- | ----------------------------------- |
| <font color=red>name</font>       | 必须配置，分类节点的ID                         |                                     |
| <font color=green> display</font> | 可选配置，默认值与name相同。For UI             | same as <font color=red>name</font> |
| <font color=green> visible</font> | 可选配置，节点下的数据项是否在UI可见。For UI   | true                                |
| <font color=green> enable</font>  | 可选配置，节点下的数据项是否可在UI更改。For UI | true                                |

### Data node

`<config name="RemoteIpAddress" value="127.0.0.1" type="String" max="" min="" regex="^((2((5[0-5])|([0-4]\d)))|([0-1]?\d{1,2}))(\.((2((5[0-5])|([0-4]\d)))|([0-1]?\d{1,2}))){3}$" regexnote="必须是正确的IP地址格式" options="127.0.0.1;192.168.2.22;172.176.1.1" desc="服务器的IP地址" unit=""  visible="true" enable="true" restart="true" />`

| Attribute                            | Description                                                  | Default Value                       |
| ------------------------------------ | ------------------------------------------------------------ | ----------------------------------- |
| <font color=red>name</font>          | 必须配置，数据节点的ID                                       |                                     |
| <font color=red>initial_value</font> | 必须配置，数据项的值                                         |                                     |
| <font color=red>type</font>          | 必须配置，数据项的值类型。                                   |                                     |
| <font color=orange>max</font>        | 可选配置，数据项的允许值的最大边界。此属性只有`type`是`Integer` `Decimal`才有效。 | decimal.MaxValue                    |
| <font color=orange>min</font>        | 可选配置，数据项的允许值的最小边界。此属性只有`type`是`Integer` `Decimal`才有效。 | decimal.MinValue                    |
| <font color=orange>regex</font>      | 可选配置，正则表达式。SetValue的新值实参最终转换成的字符串，必须匹配此正则表达式，否则拒绝本次修改。 | string.Empty                        |
| <font color=orange>regex_note</font> | 可选配置，regex的注解。正则表达式难以理解，可以用此项做注解，不是必须的。在程序中设置值时，如果正则表达式校验失败，抛出的异常信息是regexNote,如果未配置regexNote，异常信息是regex. | string.Empty                        |
| <font color=orange>options</font>    | 允许的取值集合。用; 隔开，如 options="COM1;COM2;COM3" .      | empty array                         |
| <font color=green>display</font>     | 可选配置，在UI显示的文字。For UI                             | same as <font color=red>name</font> |
| <font color=green>desc</font>        | 可选配置，数据项的描述。For UI                               | string.Empty                        |
| <font color=green>unit</font>        | 可选配置，数据项的单位，如 kg，Torr，mm，℃ ...... For UI     | string.Empty                        |
| <font color=green>visible</font>     | 可选配置，数据项是否在UI可见。For UI                         | true                                |
| <font color=green>enable</font>      | 可选配置，数据项是否可在UI更改。For UI                       | true                                |
| <font color=green>restart</font>     | 可选配置，修改数据项的值后是否需要重启App。For UI            | false                               |

### options

> options只对Boolean，Integer，Decimal，String有效，其他类型会绕过options机制。

example1: 限定String类型的串口号配置值为COM1，COM2，COM3之一。

< name="Port" value="COM1" type="String" options="COM1;COM2;COM3" />

example2：限定Integer类型的重试次数配置值为1，10，100之一。

< name="RetryTimes" value="1" type="Integer" options="1;10;100" />

SetValue(string config，object newValue)，会先把newValue转换成type指定的类型，再将options的每一项转换成type指定的类型，这时候才开始检查options中是否有元素等于newValue，如果没有，会拒绝本次修改。举例：newValue是字符串"0xA"，Options字符串列表是["1","10","100"],设置值操作会成功，字符串"0xA"转换成整数是10，字符串"10"转换成整数是10，所以Options包含"0xA"。

type是ValueType.Integer时，匹配规则是（`ValueType.Decimal与Integer类似`）

```c#
var longOptions = new List<long>();
foreach (var option in options)
{
    if (TryParse2Long(option, out long longValue))
    {
        longOptions.Add(longValue);
    }
    else
    {
        throw new ConfigException($"option '{option}' can't convert to a integer for '{configItem}'.");
    }
}
TryParse2Long(strValue, out long @long);
if (!longOptions.Contains(@long))
{
    throw new ArgumentOutOfRangeException(nameof(value), $"The value '{strValue}' is not in the options for config item '{configItem}'.");
}
```

type是ValueType.Boolean时，options决定true和false时在UI显示的文本。如options="on;off"，true显示on，false显示off。如果不需要UI界面，无需为ValueType.Boolean配置options。

### regex

> regex只对String，Decimal，Integer，File，Folder校验，其他类型Boolean，Color，DateTime绕过正则表达式校验。

```c#
private string Convert2String(object value)
{
    var valueType = value.GetType();
    if (valueType == typeof(DateTime))
    {
        return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
    }
    else if (valueType == typeof(System.Drawing.Color))
    {
        return "#" + ((System.Drawing.Color)value).ToArgb().ToString("X8", CultureInfo.InvariantCulture);
    }
    else if (valueType == typeof(FileInfo))
    {
        return ((FileInfo)value).FullName;
    }
    else if (valueType == typeof(DirectoryInfo))
    {
        return ((DirectoryInfo)value).FullName;
    }
    else
    {
        return value.ToString();
    }
}
```

- type是ValueType.String时，匹配规则是 `Regex.IsMatch(Convert2String(newValue), regex)`。

example: IPV4

`^((2((5[0-5])|([0-4]\d)))|([0-1]?\d{1,2}))(\.((2((5[0-5])|([0-4]\d)))|([0-1]?\d{1,2}))){3}$`

- type是ValueType.Integer或ValueType.Decimal时，匹配规则是 `Regex.IsMatch(decimal.Parse(Convert2String(newValue)).ToString(), regex)`，也就是说，数字的字符串形式有多种，比如十六进制，科学计数法等，但是在进行正则表达式匹配时，总是先转换成最简单的十进制的字符串再去匹配。

example: 

`^([02468]|[1-9]\d*[02468])$`,限制是偶数。

## CustomizeValidationRule & CustomizeOptionsSource

### Derived from PrimitiveConfigSource

PrimitiveConfigSource有两个虚方法

`CustomizeValidationRule` 可以定制数据项的校验规则。\<config>节点只能用min,max,regex来校验，当这3个手段无法满足校验需求时，可以通过重写此方法再额外添加一些校验规则。

`CustomizeOptionsSource` 可以定制数据项的限定值选项。\<config>节点可以用options来限定数据项允许的取值集合，当options无法很好的指定集合时，可以通过重写此方法指定允许的取值集合。注意：此方法返回的集合会覆盖掉\<config>的options，即导致xml中配置的options无效。



用法示例如下。

```c#
public class AppConfigSource : PrimitiveConfigSource
{
    public AppConfigSource(string xmlString) : base(xmlString)
    {
    }

    public AppConfigSource(string xmlDocumentPath, Encoding encoding) : base(xmlDocumentPath, encoding)
    {
    }

    protected override string[] CustomizeOptionsSource(string configItem)
    {
        switch (configItem)
        {
            case "System.CycleCount":
                return ["1","2","3","4","5","6"];
            case "System.RemoteIpAddress":
                return ["127.0.0.1","192.168.2.22"];
            default:
                return null;
        }
    }

    protected override Func<string, bool> CustomizeValidationRule(string configItem)
    {
        switch (configItem)
        {
            case "System.CycleCount":
                return (textValue) =>
                {
                    if (textValue.Any(x => !char.IsDigit(x)))
                    {
                        return false;
                    }
                    return true;
                };
            case "System.RemoteIpAddress":
                return (textValue) =>
                {
                    if(!Regex.IsMatch(textValue, "^((2(5[0-5]|[0-4]\\d))|[0-1]?\\d{1,2})(\\.((2(5[0-5]|[0-4]\\d))|[0-1]?\\d{1,2})){3}$"))
                    {
                        return false;
                    }
                    return true;
                };
            default:
                return null;
        }
    }
}
```

## Restarting app will restore the default value

PrimitiveConfigSource第2个构造方法如下

`public PrimitiveConfigSource(string xmlString)`

可以在内存中提供一个作为默认配置的xml文本，在软件启动后可以正常读取和修改配置项的值，但是软件重启后，所有配置又恢复默认配置，上一次软件运行修改的值被丢弃。也就是说，每次软件启动，使用的都是相同的初始配置。

```c#
string xmlString = """
    <?xml version="1.0" encoding="utf-8"?>
    <root>
    	<config name="System">
    		<config value="3" name="CycleCount" type="Integer"/>
    		<config value="false" name="IsSimulatorMode" type="Boolean"/>
    	</config>
    </root>
    """;

using PrimitiveConfigSource source = new PrimitiveConfigSource(xmlString);

Console.WriteLine(source.GetValue<int>("System.CycleCount"));
Console.WriteLine(source.GetValue<bool>("System.IsSimulatorMode"));

source.SetValue("System.CycleCount", 111);
source.SetValue("System.IsSimulatorMode", true);

Console.WriteLine(source.GetValue<int>("System.CycleCount"));
Console.WriteLine(source.GetValue<bool>("System.IsSimulatorMode"));

// PrimitiveConfigSource source = new PrimitiveConfigSource(File.ReadAllText("SetUp.xml")); // SetUp.xml作为软件初启动的默认配置，存放在应用程序目录下，可根据需要编辑修改。
```

## Awesome Example

## TODO

- DateTime 的字符串形式的值,要支持 仅有日期, 仅有时间, 日期+时间 三种形式, 这样UI控件可以根据字符串形式决定是否支持日期编辑或时间编辑!
- File和Folder要同时支持绝对路径和相对路径!
- File和Folder目前仅支持Windows路径，后续应当补充支持Linux和Mac。
- 加载XML字符串时，检查它不能含有不允许出现的属性和节点。
- 字典操作全部替换成ConcurrentDictionary的API。
- 作为返回值的字典全部替换成ReadonlyDictionary。
- 备份写文件抽象成Common公共API，然后替换。
- 批量写使用流写，避免产生导致产生Gen2 GC的80kb字符串大对象。
