using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Atomtick.Events.CEID
{
    public interface IEventManager
    {
        event EventHandler<EventInstance> OnEventAsync;
        event EventHandler<EventInstance> OnEventSync;

        void ClearAllAlarmEvents();
        void ClearAlarmEvent(long instanceId);
        void ClearAlarmEvent(string source);
        bool HasAlarmEvent(out IList<EventInstance> events);
        bool HasAlarmEvent(string source, out IList<EventInstance> events);
        void PostAlramEvent(string source, string description);
        void PostAlramEvent(string source, string description, ListDictionary DvidValues);
        void PostEvent(string name, string source);
        void PostEvent(string name, string source, ListDictionary DvidValues);
        void PostInfoEvent(string source, string description);
        void PostInfoEvent(string source, string description, ListDictionary DvidValues);
        void PostWarningEvent(string source, string description);
        void PostWarningEvent(string source, string description, ListDictionary DvidValues);
        void Register(EventDef @event);
        void Register(long id, string name, EventLevel eventLevel, string description);
    }
}
