using SCADA.Common;
using System;
using System.Collections.Specialized;

namespace SCADA.Events
{
    public interface IEventManager
    {
        #region General Core Features

        event Action<EventItem> EventOcurred;

        void ClearAlarmEvent();

        void ClearAlarmEventByName(string eventName);

        void ClearAlarmEventBySource(string eventSource);

        void PostEvent(string eventName);

        void PostEvent(string eventName, params object[] args);

        void PostEvent(string eventName, string source);

        void PostEvent(string eventName, string source, params object[] args);

        #region Unregistered Events

        void PostAlarmEvent(string message, ModuleName source, NotifyType notifyType = NotifyType.UILog | NotifyType.Dialog);

        void PostInfoEvent(string message, ModuleName source, NotifyType notifyType = NotifyType.UILog);

        void PostWarnEvent(string message, ModuleName source, NotifyType notifyType = NotifyType.UILog);

        #endregion Unregistered Events

        #endregion General Core Features

        #region SCMS/GEM

        void PostEventWithDVVALs(string eventName, ListDictionary dvvals);

        void PostEventWithDVVALs(string eventName, ListDictionary dvvals, params object[] args);

        void PostEventWithDVVALs(string eventName, string source, ListDictionary dvvals);

        void PostEventWithDVVALs(string eventName, string source, ListDictionary dvvals, params object[] args);

        #region Unregistered Events

        void PostAlarmEventWithDVVALs(string message, ModuleName source, ListDictionary dvvals, NotifyType notifyType = NotifyType.UILog | NotifyType.Dialog);

        void PostInfoEventWithDVVALs(string message, ModuleName source, ListDictionary dvvals, NotifyType notifyType = NotifyType.UILog);

        void PostWarnEventWithDVVALs(string message, ModuleName source, ListDictionary dvvals, NotifyType notifyType = NotifyType.UILog);

        #endregion Unregistered Events

        #endregion SCMS/GEM
    }
}