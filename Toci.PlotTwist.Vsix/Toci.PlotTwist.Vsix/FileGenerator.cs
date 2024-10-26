using EnvDTE;
using EnvDTE80;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

public class FileGenerator
{
    private readonly DTE2 _dte;

    public FileGenerator(DTE2 dte)
    {
        _dte = dte;
    }

    public void GenerateFiles(string directoryPath, string[] fileNames)
    {
        try
        {
            // Ensure the directory exists
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Get the current project
            Project project = _dte.ActiveSolutionProjects as Project;

            if (project == null)
            {
                MessageBox.Show("No active project found.");
                return;
            }

            foreach (var fileName in fileNames)
            {
                string filePath = Path.Combine(directoryPath, fileName);

                // Create a new file
                File.WriteAllText(filePath, "// This is a generated file: " + fileName);

                // Add the file to the project
                project.ProjectItems.AddFromFile(filePath);
            }

            MessageBox.Show("Files created and added to the project successfully.");
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: " + ex.Message);
        }
    }

    public void GenerateFilesFromChatGptResponse(string response)
    {
        try
        {
            // Assume the response is in the format:
            // FileName1.cs: // Content of File 1
            // FileName2.cs: // Content of File 2
            var lines = response.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Project project = _dte.ActiveSolutionProjects as Project;

            if (project == null)
            {
                MessageBox.Show("No active project found.");
                return;
            }

            foreach (var line in lines)
            {
                var split = line.Split(new[] { ":" }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length != 2) continue; // Skip if the format is incorrect

                string fileName = split[0].Trim();
                string fileContent = split[1].Trim();

                string filePath = Path.Combine(project.FullName, fileName);

                // Create a new file
                File.WriteAllText(filePath, fileContent);

                // Add the file to the project
                project.ProjectItems.AddFromFile(filePath);
            }

            MessageBox.Show("Files created and added to the project successfully.");
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: " + ex.Message);
        }
    }
}