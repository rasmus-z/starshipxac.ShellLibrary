using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace starshipxac.Shell.Components.Internal
{
    /// <summary>
    ///     Manage shell change event.
    /// </summary>
    internal class ShellChangeEventManager
    {
        private readonly IEnumerable<uint> eventTypes;
        private readonly ConcurrentDictionary<uint, List<Delegate>> events;

        /// <summary>
        ///     Initialize a instance of the <see cref="ShellChangeEventManager"/> class.
        /// </summary>
        public ShellChangeEventManager()
        {
            this.eventTypes = Enum.GetValues(typeof(ShellChangeTypes)).Cast<object>().Select(Convert.ToUInt32);
            this.events = new ConcurrentDictionary<uint, List<Delegate>>();
        }

        public uint RegisteredTypes
        {
            get
            {
                return this.events.Keys.Aggregate(0U, (a, c) => (Convert.ToUInt32(c) | a));
            }
        }

        //TODO: Delegateを変更する。

        public void AddHandler(ShellChangeTypes eventType, Delegate handler)
        {
            var et = Convert.ToUInt32(eventType);

            this.events.AddOrUpdate(et,
                value => new List<Delegate> {handler},
                (value, list) =>
                {
                    list.Add(handler);
                    return list;
                });
        }

        public void RemoveHandler(ShellChangeTypes eventType, Delegate handler)
        {
            var et = Convert.ToUInt32(eventType);

            List<Delegate> handlerList;
            if (this.events.TryGetValue(et, out handlerList))
            {
                handlerList.Remove(handler);
            }
        }

        public void RemoveAll()
        {
            this.events.Clear();
        }

        public void Invoke(object sender, uint eventType, EventArgs args)
        {
            foreach (var et in this.eventTypes.Where(x => (x & eventType) != 0))
            {
                InvokeEventHandlers(sender, et, args);
            }
        }

        private void InvokeEventHandlers(object sender, uint eventType, EventArgs args)
        {
            List<Delegate> handerList;
            if (this.events.TryGetValue(eventType, out handerList))
            {
                foreach (var handler in handerList)
                {
                    handler.DynamicInvoke(sender, args);
                }
            }
        }
    }
}