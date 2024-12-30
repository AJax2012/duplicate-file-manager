namespace DuplicateFileManager.Serivce.Models;

public class DeleteFilesRequest
{
    public List<string> FilePaths { get; set; } = [];
}