using System;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace Toci.PlotTwist.ChatGPT
{
    public class FileGenerator
    {
        private DTE2 _dte;

        public FileGenerator(DTE2 dte)
        {
            _dte = dte;
        }

        public void CreateClassFile(string className)
        {
            // Get the current project
            var project = _dte.Solution.Projects.OfType<Project>().FirstOrDefault();
            if (project == null) return;

            // Create a new class file
            string filePath = $@"{project.FullName}\{className}.cs";
            string classContent = $@"
using System;

namespace {project.Name}
{{
    public class {className}
    {{
        // Your properties and methods here
    }}
}}";

            // Save the file
            System.IO.File.WriteAllText(filePath, classContent);
            _dte.ItemOperations.OpenFile(filePath);
        }
    }
}