namespace Commander.TestExecutable
{
    public static class Constants
    {
        public static class Inputs
        {
            public const string Request1 = "REQUEST";
            public const string Request2 = "OTHER REQUEST";
        }

        public static class Outputs
        {
            public const string StandardOutput = "<standard output>";
            public const string StandardError = "<standard error>";
            public const int ExitCode = 42;
            public const string Prompt1 = "PROMPT1";
            public const string Prompt2 = "PROMPT2";
            public const string ResponsePrefix = "RESPONSE: ";
            public const string ExpectedResponse1 = ResponsePrefix + Inputs.Request1;
            public const string ExpectedResponse2 = ResponsePrefix + Inputs.Request2;
        }

        public static class Arguments
        {
            public const string Output = "OUTPUT";
            public const string Error = "ERROR";
            public const string ExitCode = "EXITCODE";
            public const string Input = "INPUT";
            public const string MultiInput = "MULTIINPUT";
        }
    }
}