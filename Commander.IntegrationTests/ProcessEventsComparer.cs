using System;
using System.Collections;
using Commander.Events;

namespace Commander.IntegrationTests
{
    public class ProcessEventsComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
                return 0;
            if (x == null)
                return -1;
            if (y == null)
                return +1;

            if (x.GetType() != y.GetType())
                return StringComparer.InvariantCultureIgnoreCase.Compare(x.GetType().FullName, y.GetType().FullName);

            var exitedEvent = x as ProcessExitedEvent;
            if (exitedEvent != null)
                return exitedEvent.ExitCode.CompareTo(((ProcessExitedEvent)y).ExitCode);

            var lineEvent = x as ProcessOutputEvent;
            if (lineEvent != null)
                return StringComparer.InvariantCulture.Compare(lineEvent.Line, ((ProcessOutputEvent)y).Line);

            return 0;
        }
    }
}