using System;

namespace Commander.TestExecutable
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string request;
            switch (args[0])
            {
                case Constants.Arguments.Output:
                    Console.Out.WriteLine(Constants.Outputs.StandardOutput);
                    break;

                case Constants.Arguments.Error:
                    Console.Error.WriteLine(Constants.Outputs.StandardError);
                    break;

                case Constants.Arguments.ExitCode:
                    Environment.Exit(Constants.Outputs.ExitCode);
                    break;

                case Constants.Arguments.Input:
                    Console.Out.WriteLine(Constants.Outputs.Prompt1);
                    request = Console.In.ReadLine();
                    Console.Out.WriteLine(Constants.Outputs.ResponsePrefix + request);
                    break;

                case Constants.Arguments.MultiInput:
                    Console.Out.WriteLine(Constants.Outputs.Prompt1);
                    request = Console.In.ReadLine();
                    Console.Out.WriteLine(Constants.Outputs.ResponsePrefix + request);

                    Console.Out.WriteLine(Constants.Outputs.Prompt2);
                    request = Console.In.ReadLine();
                    Console.Out.WriteLine(Constants.Outputs.ResponsePrefix + request);
                    break;
            }
        }
    }
}
