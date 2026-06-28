# ZeroGC

public void PrepareForHotPath()
{
    // 1. 告诉 GC：下次回收时，请务必压缩大对象堆 (LOH) 以消除内存碎片
    GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;

    // 2. 强制执行最高代 (Gen 2)、阻塞式、且压缩堆的 GC 回收
    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true, compacting: true);
    
    // 3. 等待所有带有析构函数（Finalizer）的对象执行完毕
    GC.WaitForPendingFinalizers();
    
    // 4. 可选：再执行一次，因为上一步的终结器可能会释放新的引用，导致产生新的垃圾
    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true, compacting: true);
    
    Console.WriteLine("堆内存清理与压缩完毕，准备进入热路径！");
}













#### 第三阶段：热路径 (The Hot Path)

进入这个阶段后，你的目标是“Gen 0 回收次数无限趋近于 0”。 在这个闭环里，你必须遵守严苛的纪律：

- 用 `ArrayPool<T>` 或自定义对象池代替 `new`。
- 用 `struct` 代替 `class`（分配在栈上）。
- 用 `Span<T>` / `Memory<T>` 处理字符串和数组切片。
- 绝对禁止使用 LINQ、装箱（Boxing）和无意识的闭包（Lambda 捕获外部变量）。















如果你知道你的热路径无论如何优化，还是会产生少量的临时对象，但你**绝对不允许 GC 在这段时间内发生卡顿（STW）**，你可以提前向系统“申请”一块专属内存：

```c#
// 假设热路径大概需要 50MB 内存
long totalSize = 50 * 1024 * 1024; 

// 1. 启动无 GC 区域（调用此方法底层会自动触发一次完整的 GC 以腾出空间）
bool success = GC.TryStartNoGCRegion(totalSize);

if (success)
{
    try
    {
        // 2. 进入极致热路径！
        // 在这里，只要你分配的总内存不超过 50MB，.NET 保证绝对不会触发任何 GC！
        RunExtremeLowLatencyTask(); 
    }
    finally
    {
        // 3. 任务结束，恢复正常的 GC 机制
        GC.EndNoGCRegion();
    }
}
```

注意：`NoGCRegion` 适用于短期极度敏感的任务。如果是长年累月运行的服务器热循环，依然需要依靠对象池来做到物理上的零分配。

# HFSM

状态A，接收到消息a，执行一个函数，函数返回true，则迁移到下一状态，返回false，则仍旧保持当前状态。

消息a分成两部分,一部分是command,单一状态可以接收多个不同的command转移到不同的下一状态,另一部分是提供给函数的实参.

状态的迁移只能靠消息驱动.我们期望稳定在某个状态时,能够一直执行一些任务,直到状态被切换走.所以,如果无状态迁移的消息时,我们就假设有个消息叫FsmCmd.Timer,此消息会执行一个函数,但是这个函数的返回值永远是无参的且永远返回false,其不会引起状态的迁移.

这个设计有很大的作用,只要灵活的从不同角度理解它,用各种方式扩展它,能够实现惊喜强大的功能.



第一种实现,状态切换后,立刻执行在该状态下应当执行的任务,比如倒计时,或者定时抛出消息,状态切换走后,要立刻停止,保证这一点很重要.这很难做到.但是把它做成状态机里面,就能保证,因为消息优先,延迟时间只是单次函数的执行时间.

perfect!



妙用:

1. 红绿灯
2. 长耗时任务





状态机内部维护一个消息队列，它监视队列，一旦有新的消息，则立刻取出，然后查询状态迁移表，如果当前状态可以接收此消息，则执行函数，函数的实参来自消息，最后根据函数的返回值决定如何迁移状态。

有一个巧妙之处：函数可以在自己返回false的前一步，往状态机的消息队列推入别的消息，那样可以驱动当前状态往另一状态过渡。举例：





Timer是状态的自循环,它的Action永远返回false,只靠消息驱动,条件满足驱动力也要转成消息驱动.





二开有两种扩展:扩展一,长耗时操作  扩展二 红绿灯自驱动条件满足向前驱动.





complete

error

abort





如果函数是一个长耗时操作



Action return true，则切换状态，Action return false，则什么也不做，除非在Action return false前一步，PostErrorMsg to StateMachine，这样状态机的下一次周期，会取出Error Msg将当前状态驱动到Error状态。







**状态迁移表**



| Current State | Next State | Message | Action |
| ------------- | ---------- | ------- | ------ |
| S1            | S2         | msg1-2  |        |
| S2            | S3         | msg2-3  |        |
| S3            | S1         | msg3-1  |        |



> 当前状态下，收到消息命令和消息参数，状态可以迁移到多种另一状态，消息命令决定是往哪个状态迁移，收到消息后，会执行一个函数，函数的实参就是消息参数，若函数返回TRUE，则顺利迁移到下一状态，若返回FALSE，则不迁移。
>
> 函数返回FALSE，并不是表示函数执行失败，它只是表示本次虽收到消息且执行了Action但是不迁移到下一状态。我们可以为FALSE赋予多种意义：1.如果认为返回FALSE是表示Action异常，想要迁移到ERROR状态，可以在函数返回FALSE前一步，往消息队列中POST一条ERROR MESSAGE即可。2.如果返回FALSE表示Action是长耗时操作，只是yield return false，那么不要在Action返回false时插入消息就行。





> 长耗时的Action怎么办？





### todo

- 导出状态图图片
- 统计每个函数的耗时的最大最小值，平均值，执行次数
- Routine增加功能：若Routine的执行总时长超过阈值，直接以错误结束Routine。



# UART & Modbus

# PLC Framework





# ObjectModel



