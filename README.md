# commander
C# Command Line Program Execution Helper Classes

This library wraps up the .NET framework [Process](https://msdn.microsoft.com/en-us/library/system.diagnostics.process%28v=vs.110%29.aspx)
class in such a way that it becomes easier to monitor output, feed the running program input, and do this in an async manner.

Basic example:

    var psi = new ProcessStartInfo("hg", "clone http://some.domain.com/repo d:\temp");
    var result = await ProcessEx.ExecuteAsync(psi);
    if (result.ExitCode == 0)
    {
        // success
    }

More complex example:

    var psi = new ProcessStartInfo("hg", "clone http://some.domain.com/repo d:\temp");
    var evts = new ProcessEventMonitor();
    evts.StandardOutput += (s, e) =>
    {
        if (e.Line == "Enter username:")
            e.Process.WriteLine("username");
        else if (e.Line == "Enter password:")
            e.Process.WriteLine("pa$$w0rd");
    };
    var result = await ProcessEx.ExecuteAsync(psi, evts);
    if (result.ExitCode == 0)
    {
        // success
    }


Add timeout support:

    ...
    using (var timeout = new ProcessTimeoutMonitor(TimeSpan.FromSeconds(15)))
    {
        var result = await ProcessEx.ExecuteAsync(psi, evts, timeout);
        ...
    }

And log to Debug output:

    var debug = new DebugProcessMonitor();
    var result = await ProcessEx.ExecuteAsync(psi, ..., debug);
