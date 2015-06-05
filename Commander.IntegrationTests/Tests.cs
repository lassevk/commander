using System;
using System.Diagnostics;
using Commander.Events;
using Commander.Monitors;
using Commander.TestExecutable;
using NUnit.Framework;

namespace Commander.IntegrationTests
{
    [TestFixture]
    public class Tests : ExecutionTest
    {
        [Test]
        public void StandardOutput()
        {
            var events = Execute(Constants.Arguments.Output);
            CollectionAssert.AreEqual(new ProcessEvent[]
            {
                new ProcessStartedEvent(TimeSpan.Zero, DateTime.Now),
                new ProcessStandardOutputEvent(TimeSpan.Zero, DateTime.Now, Constants.Outputs.StandardOutput),
                new ProcessExitedEvent(TimeSpan.Zero, DateTime.Now, 0),
            }, events, new ProcessEventsComparer());
        }

        [Test]
        public void StandardErrorOutput()
        {
            var events = Execute(Constants.Arguments.Error);
            CollectionAssert.AreEqual(new ProcessEvent[]
            {
                new ProcessStartedEvent(TimeSpan.Zero, DateTime.Now),
                new ProcessErrorOutputEvent(TimeSpan.Zero, DateTime.Now, Constants.Outputs.StandardError), 
                new ProcessExitedEvent(TimeSpan.Zero, DateTime.Now, 0),
            }, events, new ProcessEventsComparer());
        }

        [Test]
        public void ExitCode()
        {
            var events = Execute(Constants.Arguments.ExitCode);
            CollectionAssert.AreEqual(new ProcessEvent[]
            {
                new ProcessStartedEvent(TimeSpan.Zero, DateTime.Now),
                new ProcessExitedEvent(TimeSpan.Zero, DateTime.Now, Constants.Outputs.ExitCode),
            }, events, new ProcessEventsComparer());
        }

        [Test]
        public void Input()
        {
            var evts = new ProcessEventMonitor();
            evts.StandardOutput += (sender, args) =>
            {
                Debug.WriteLine(args.Line);
                switch (args.Line)
                {
                    case Constants.Outputs.Prompt1:
                        args.Process.WriteLine(Constants.Inputs.Request1);
                        break;

                    case Constants.Outputs.Prompt2:
                        args.Process.WriteLine(Constants.Inputs.Request2);
                        break;
                }
            };
            var events = Execute(Constants.Arguments.MultiInput, evts);
            CollectionAssert.AreEqual(new ProcessEvent[]
            {
                new ProcessStartedEvent(TimeSpan.Zero, DateTime.Now),
                new ProcessStandardOutputEvent(TimeSpan.Zero, DateTime.Now, Constants.Outputs.Prompt1),
                new ProcessStandardOutputEvent(TimeSpan.Zero, DateTime.Now, Constants.Outputs.ExpectedResponse1),
                new ProcessStandardOutputEvent(TimeSpan.Zero, DateTime.Now, Constants.Outputs.Prompt2),
                new ProcessStandardOutputEvent(TimeSpan.Zero, DateTime.Now, Constants.Outputs.ExpectedResponse2),
                new ProcessExitedEvent(TimeSpan.Zero, DateTime.Now, 0),
            }, events, new ProcessEventsComparer());
        }

        [Test]
        public void MultiInput()
        {
            var evts = new ProcessEventMonitor();
            evts.StandardOutput += (sender, args) =>
            {
                Debug.WriteLine(args.Line);
                if (args.Line == Constants.Outputs.Prompt1)
                    args.Process.WriteLine(Constants.Inputs.Request1);
            };
            var events = Execute(Constants.Arguments.Input, evts);
            CollectionAssert.AreEqual(new ProcessEvent[]
            {
                new ProcessStartedEvent(TimeSpan.Zero, DateTime.Now),
                new ProcessStandardOutputEvent(TimeSpan.Zero, DateTime.Now, Constants.Outputs.Prompt1),
                new ProcessStandardOutputEvent(TimeSpan.Zero, DateTime.Now, Constants.Outputs.ExpectedResponse1),
                new ProcessExitedEvent(TimeSpan.Zero, DateTime.Now, 0),
            }, events, new ProcessEventsComparer());
        }
    }
}
