# Commander

This library wraps up the .NET framework [Process](https://msdn.microsoft.com/en-us/library/system.diagnostics.process%28v=vs.110%29.aspx)
class in such a way that it becomes easier to monitor output, feed the running program input, and do this in an async manner.

## Monitors

The principle is that you give a [ProcessStartInfo](https://msdn.microsoft.com/en-us/library/system.diagnostics.processstartinfo%28v=vs.110%29.aspx) to
[ProcessEx](Commander/ProcessEx.cs) and get back a `Task<int>` that represents the running program. The result of the task is the exitcode once the
program exits.

To monitor the execution, one or more objects that implement [IProcessMonitor](Commander/IProcessMonitor.cs) can be provided that are called when
various events occur during the execution of the program:

* The program is started
* The program has exited
* A line of standard output has been captured
* A line of error output has been captured

The monitor object can do "the right thing" with these events, such as log them, collect them, or even act upon them. Alongside the data for
the event that occured, a [IProcess](Commander/IProcess.cs) object is provided, where the monitor can control the running program, such
as feed it text on standard input or even forcibly terminate it.

See the [Events folder](Commander/Events) for some monitors that come with the library. Since the rest of the library only references these monitors
through the [IProcessMonitor](Commander/IProcessMonitor.cs) interface, new monitors can easily be created.

## Basic example

Here we're going to simply clone a Mercurial repository into a local folder, wait until mercurial has exited, and inspect the exitcode.

    var psi = new ProcessStartInfo("hg", "clone http://some.domain.com/repo d:\temp");
    var exitcode = await ProcessEx.ExecuteAsync(psi);
    if (exitcode == 0)
    {
        // success
    }

## More complex example

Here we assume that Mercurial will ask for username and password (the strings below may be incorrect, don't use this as an actual example of how
to interact with Mercurial), and then we'll feed the correct username and password back to the process.

    var psi = new ProcessStartInfo("hg", "clone http://some.domain.com/repo d:\temp");
    var evts = new ProcessEventMonitor();
    evts.StandardOutput += (s, e) =>
    {
        if (e.Line == "Enter username:")
            e.Process.WriteLine("username");
        else if (e.Line == "Enter password:")
            e.Process.WriteLine("pa$$w0rd");
    };
    var exitcode = await ProcessEx.ExecuteAsync(psi, evts);
    if (exitcode == 0)
    {
        // success
    }


## Add timeout support

There is also a timeout monitor that will wait a given duration of time before it forcibly terminates the program. The
monitor can optionally be configured to reset the clock every time output is captured from the program, in effect
turning the timeout monitor into a "timeout since last output" monitor.

    ...
    using (var timeout = new ProcessTimeoutMonitor(TimeSpan.FromSeconds(15)))
    {
        var exitcode = await ProcessEx.ExecuteAsync(psi, evts, timeout);
        ...
    }

## And log to Debug output

There is also a simple monitor implementation that just outputs every event it captures to the Debug output
of the program.

    var debug = new DebugProcessMonitor();
    var exitcode = await ProcessEx.ExecuteAsync(psi, ..., debug);

## Collect all the output from the program

To collect all the events, so that we can analyze it afterwards, scan the text the program output, etc. you
can use the [ProcessCollectOutputMonitor](Commander/Monitors/ProcessCollectOutputMonitor.cs). After the
program has terminated you can read the `Events` property to get a list of event objects detailing the
events that were captured during program execution.