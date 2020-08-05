// -----------------------------------------------------------------------
// <copyright file="NUnit3.cs" company="Ace Olszowka">
//  Copyright (c) Ace Olszowka 2020. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace NUnit.MSBuild
{
    using System.IO;
    using System.Text;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// An MSBuild Wrapper around the NUnit3 Console Runner.
    /// </summary>
    public class NUnit3 : ToolTask
    {
        /// <summary>
        /// Gets or sets a list of assemblies to pass to NUnit Console.
        /// </summary>
        [Required]
        public string[] Assemblies
        {
            get;
            set;
        }

        /// <summary>
        ///   Gets or sets the --result flag which is passed; verbatim to the
        /// NUnit3 Console Runner. Pay special attention to the Console command
        /// documentation as this takes a SPEC.
        /// </summary>
        /// <remarks>
        ///   The most common usage will be to indicate the file name; however
        /// you can also indicate the output format by passing it like this:
        ///     C:\TestResultOutput;format=nunit2
        /// </remarks>
        public string Result
        {
            get;
            set;
        }

        /// <summary>
        ///   Gets or sets a value which indicates if the --x86 flag should be
        /// set. Run tests in a 32-bit process on 64-bit systems.
        /// </summary>
        public bool Forcex86
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value to pass to the --framework flag.
        /// </summary>
        public string Framework
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value to pass to the --agents flag. This setting
        /// is used to control running your assemblies in parallel.
        /// </summary>
        public string Agents
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value to pass to the --where clause. An expression indicating which tests to run.
        /// </summary>
        public string Where
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the name of this Tool.
        /// </summary>
        protected override string ToolName
        {
            get
            {
                return "nunit3-console.exe";
            }
        }

        /// <summary>
        /// Generates the full path to the tool.
        /// </summary>
        /// <returns>The full path to the tool.</returns>
        protected override string GenerateFullPathToTool()
        {
            string fullPathToTool = this.ToolName;

            if (!string.IsNullOrEmpty(this.ToolPath))
            {
                fullPathToTool = Path.Combine(this.ToolPath, this.ToolName);
            }

            return fullPathToTool;
        }

        /// <summary>
        ///     Generates a Command File at the specified location
        /// based on the task inputs.
        /// </summary>
        /// <param name="commandFile">The location of the command file to generate.</param>
        protected override string GenerateResponseFileCommands()
        {
            StringBuilder sb = new StringBuilder();

            // All test assemblies should be first
            foreach (var assembly in this.Assemblies)
            {
                sb.AppendLine(assembly);
            }

            if (this.Forcex86)
            {
                sb.AppendLine("--x86");
            }

            AppendSwitchIfNotNull(sb, "--agents=", this.Agents);
            AppendSwitchIfNotNull(sb, "--framework=", this.Framework);
            AppendSwitchIfNotNull(sb, "--result=", this.Result);
            AppendSwitchIfNotNull(sb, "--where=", this.Where);

            return sb.ToString();
        }

        /// <summary>
        /// Appends a command to the string builder if its value is not null.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to append to.</param>
        /// <param name="switchName">The name of the switch to append to the command line. This value cannot be null.</param>
        /// <param name="switchParameter">The switch parameter to append to the command line. If this value is null, then this method has no effect.</param>
        private static void AppendSwitchIfNotNull(StringBuilder sb, string switchName, string switchParameter)
        {
            if (!string.IsNullOrWhiteSpace(switchParameter))
            {
                sb.AppendLine(switchName + switchParameter);
            }
        }
    }
}
