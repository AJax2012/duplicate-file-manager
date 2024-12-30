using DuplicateFileManager.Serivce.Models;

namespace DuplicateFileManager.Serivce.Services;

public interface IFileManagerService
{
    Task DeleteFiles(DeleteFilesRequest request, CancellationToken cancellationToken);
    Task<List<DuplicateFileData>> GetDuplicateFiles(GetDuplicateFilesRequest? request, CancellationToken cancellationToken);
}