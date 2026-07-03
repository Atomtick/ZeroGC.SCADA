using SCADA.Common;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using PeriodicTimer = SCADA.Common.PeriodicTimer;

namespace SCADA.DataAcquisition
{
    public class DataCenter
    {
        private readonly ConcurrentDictionary<string, MulticastDelegate> _dataGetters;
        private readonly ConcurrentDictionary<string, MulticastDelegate> _dataGettersSaveDB;

        private readonly PeriodicTimer _periodicTimer;

        public DataCenter()
        {
            _dataGetters = new ConcurrentDictionary<string, MulticastDelegate>();
            _periodicTimer = new PeriodicTimer();
            _periodicTimer.Callback += (ct) =>
            {
                // 遍历所有注册的数据获取器，获取数据并记录到TimeScale中
                _periodicTimer.ChangePeriod((int)SCADABuildin.SBI.TryGetValue<double>("DataLoggerPeriodS", 1) * 1000);
            };
        }

        public void Start()
        {
            _periodicTimer.Start();
        }

        public void Stop()
        {
            _periodicTimer.Stop(1000);
        }


        public IEnumerable<string> GetDataNames()
        {
            return _dataGetters.Keys;
        }

        public IEnumerable<string> GetSaveDBDataNames()
        {
            return _dataGettersSaveDB.Keys;
        }

        #region Register & Unregister
        public void Register<TValue>(string dataName, DataGetter<TValue> dataGetter)
        {
            if (_dataGetters.TryAdd(dataName, dataGetter) == false)
            {
                throw new ArgumentException($"DataGetter with name '{dataName}' is already registered.");
            }
        }

        public void Register<TValue>(string dataName1, DataGetter<TValue> dataGetter1, string dataName2, DataGetter<TValue> dataGetter2)
        {
            if (_dataGetters.TryAdd(dataName1, dataGetter1) == false)
            {
                throw new ArgumentException($"DataGetter with name '{dataName1}' is already registered.");
            }
            if (_dataGetters.TryAdd(dataName2, dataGetter2) == false)
            {
                throw new ArgumentException($"DataGetter with name '{dataName2}' is already registered.");
            }
        }

        public void Register<TValue>(string dataName1, DataGetter<TValue> dataGetter1, string dataName2, DataGetter<TValue> dataGetter2, string dataName3, DataGetter<TValue> dataGetter3)
        {
            if (_dataGetters.TryAdd(dataName1, dataGetter1) == false)
            {
                throw new ArgumentException($"DataGetter with name '{dataName1}' is already registered.");
            }
            if (_dataGetters.TryAdd(dataName2, dataGetter2) == false)
            {
                throw new ArgumentException($"DataGetter with name '{dataName2}' is already registered.");
            }
            if (_dataGetters.TryAdd(dataName3, dataGetter3) == false)
            {
                throw new ArgumentException($"DataGetter with name '{dataName3}' is already registered.");
            }
        }

        public void Register<TValue>(params (string dataName, DataGetter<TValue> dataGetter)[] nameGetters)
        {
            foreach (var (dataName, dataGetter) in nameGetters)
            {
                if (_dataGetters.TryAdd(dataName, dataGetter) == false)
                {
                    throw new ArgumentException($"DataGetter with name '{dataName}' is already registered.");
                }
            }
        }

        public bool Unregister(string dataName)
        {
            return _dataGetters.TryRemove(dataName, out _);
        }
        #endregion
    }
}
