using System.ComponentModel.DataAnnotations;

namespace DuplicateFileManager.Service.Models;

public class GetDuplicateFilesRequest
{
    [Required(ErrorMessage = "Directory Path is required"), StringLength(260, MinimumLength = 3, ErrorMessage = "Directory Path must be between 3 and 260 characters")]
    public string? DirectoryPath { get; set; }
    
    [Required(ErrorMessage = "At least one file extension is required"), MinLength(1, ErrorMessage = "At least one file extension is required")]
    public List<string> FileExtensions { get; set; } = [];
}