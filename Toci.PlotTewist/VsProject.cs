using EnvDTE;
using EnvDTE80;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toci.PlotTwist.Vs
{
    public class VsProject
    {
        public async Task AddNewProjectAsync(DTE2 dte, string solutionPath, string projectName)
        {
            // Ścieżka do szablonu projektu
            string templatePath = ""; //dte.Solution.GetProjectTemplate("ClassLibrary.zip", "CSharp");

            // Ścieżka do nowego projektu
            string projectPath = Path.Combine(solutionPath, projectName);
            Directory.CreateDirectory(projectPath);

            // Dodanie projektu do rozwiązania
            dte.Solution.AddFromTemplate(templatePath, projectPath, projectName);
            await Task.Delay(500); // Drobne opóźnienie na synchronizację
        }

        public void AddClassToProject(Project project, string className, string classContent)
        {
            string classPath = Path.Combine(project.FullName, $"{className}.cs");
            File.WriteAllText(classPath, classContent);

            // Odświeżenie projektu w IDE
            project.ProjectItems.AddFromFile(classPath);
        }

       

    }
}
