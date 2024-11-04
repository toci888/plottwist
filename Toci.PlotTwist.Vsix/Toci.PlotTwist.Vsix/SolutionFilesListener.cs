using EnvDTE;
using EnvDTE80;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EnvDTE;
using EnvDTE80;
using System;
using System.Collections.Generic;


namespace Toci.PlotTwist.Vsix
{




    public class SolutionFilesLister
    {
        public static void ListAllFilesInSolution(DTE2 dte)
        {
            if (dte.Solution == null || !dte.Solution.IsOpen)
            {
                Console.WriteLine("No solution is open.");
                return;
            }

            List<string> allFiles = new List<string>();
            Projects projects = dte.Solution.Projects;

            foreach (Project project in projects)
            {
                GetProjectFiles(project.ProjectItems, allFiles);
            }

            // Output the list of files
            foreach (string file in allFiles)
            {
                Console.WriteLine(file);
            }
        }

        private static void GetProjectFiles(ProjectItems projectItems, List<string> fileList)
        {
            if (projectItems == null)
                return;

            foreach (ProjectItem item in projectItems)
            {
                // Add the file to the list
                if (item.Properties != null && item.Properties.Item("FullPath") != null)
                {
                    string filePath = item.Properties.Item("FullPath").Value.ToString();
                    fileList.Add(filePath);
                }

                // Recursively get files from sub-items
                if (item.ProjectItems != null)
                {
                    GetProjectFiles(item.ProjectItems, fileList);
                }
            }
        }
    }

}