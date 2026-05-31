### 所有的Device都必须配置到DeviceModel.cfg

永远不要在程序中使用new xxxDevice()的硬编码方式随意实例化Device,而是把Equipment涉及的任何Device都要配置到设备列表文件中,程序通过读取此文件来实例化所有的Device.

这样做的优点如下:

- 不需要某个Device时,直接从配置文件中删除

- 需要增加一些Device时,直接向配置文件中添加

- 需要更换Device的品牌时,直接在配置文件中修改Device对应的类名(这种方式比较geek,往往需要程序员亲自动手修改,操作员修改则显得心有余而力不足,所以适合不会轻易变更品牌的Device.

- 如果需要支持操作员配置当前使用的某些Device品牌,以检测工艺腔室压力的真空计为例,可能是MKS,Inficon,Fujikin等;可以开发一个万能真空计类OmniVacuumGauge,它的内部读取配置文件来决定使用哪个品牌,操作员可以通过可视化配置界面自行修改Device品牌,无需去DeviceModel.cfg中修改类名. 如果后续又引入了新的品牌,打开OmniVacuumGauge类继续添加 if else就行了!

  ```c#
  public class OmniVacuumGauge : IVacuumGauge
  {
      IVacuumGauge _vacuumGauge;
      public OmniVacuumGauge()
      {
          if(PM.ChamberVacuumGauge.Type == "MKS")
              _vacuumGauge = new MksVacuumGauge(GetConfig("PM.ChamberVacuumGauge.MKS.Port"),GetConfig("PM.ChamberVacuumGauge.MKS.BaudRate"));
          else if(PM.ChamberVacuumGauge.Type == "Inficon")
              _vacuumGauge = new InficonVacuumGauge(GetConfig("PM.ChamberVacuumGauge.Inficon.Port"),GetConfig("PM.ChamberVacuumGauge.Inficon.BaudRate"));
          else if(PM.ChamberVacuumGauge.Type == "Fujikin")
              _vacuumGauge = new FujikinVacuumGauge(GetConfig("PM.ChamberVacuumGauge.Fujikin.Port"),GetConfig("PM.ChamberVacuumGauge.Fujikin.BaudRate"));
          else
              _vacuumGauge = null;
      }
  }
  
  public class MksVacuumGauge : IVacuumGauge {    }
  public class InficonVacuumGauge : IVacuumGauge {    }
  public class FujikinVacuumGauge : IVacuumGauge {    }
  ```

  

### Device Class 的Update和Monitor的作用

Device实例就表示设备,它应当缓存设备的所有状态,做到立等可取,所以必须有个定时器高频率的询问物理设备的状态并缓存.

在一个统一的定时器的回调函数中,最优先遍历执行所有Device的Update方法,这样就能在极短且最短的时间内刷新一个模块包含的所有Device的状态,假设PLC寄存器值缓存区有20个,缓存过期被覆盖冲掉的周期大约是200ms,只要保证foreach devices update()在200ms内执行完毕,就可以保证用于刷新Device的PLC值来自同一个PLC周期,如果每个Device的Update耗时是1ms,100个Device加上时间片因素导致的线程放弃CPU3次,也就150ms左右,通过长期观察可以做到打时间差的方式做到PLC同一周期的原子性.



Monitor的目的是实现自监督机制.如气缸要监测自己有没有超时还没到位,Tolerance,Stable,自预警,自报警等机制,刷新一次新的配置信息等.





### DataCenter的作用



DataCenter是数据项的值缓冲区,

















