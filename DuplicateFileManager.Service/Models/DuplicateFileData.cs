namespace DuplicateFileManager.Service.Models;

public record DuplicateFileData(string CommonFileName, List<string> FilePaths);