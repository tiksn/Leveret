using CommandLine;

namespace TIKSN.Leveret
{
    public class CommandLineOptions
    {
        [Option('f', "file", Required = false, HelpText = "File to open.")]
        public string File { get; set; }
    }
}