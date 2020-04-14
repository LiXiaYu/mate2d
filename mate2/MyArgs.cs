using PowerArgs;
using System;
using System.Collections.Generic;
using System.Text;

namespace mate2
{
    // A class that describes the command line arguments for this program
    public class MyArgs
    {
        // This argument is required and if not specified the user will 
        // be prompted.
        [ArgRequired(PromptIfMissing = true), ArgShortcut("-mate")]
        public string MateFilePath { get; set; }

        [ArgRequired(PromptIfMissing = true), ArgShortcut("-matelib")]
        public string[] MateLibFilePaths { get; set; }


        [HelpHook, ArgShortcut("-?"), ArgDescription("Show this help")]
        public bool Help { get; set; }


        [ArgShortcut("-cpp"), ArgDescription("Set cpp files' path")]
        public string CppFilePath { get; set; }

    }
}
