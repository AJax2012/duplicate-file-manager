namespace DuplicateFileManager.Service.Models;

public class DeleteFilesRequest
{
    public List<string> FilePaths { get; set; } = [];
}