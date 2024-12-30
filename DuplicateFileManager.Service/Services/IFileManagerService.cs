using DuplicateFileManager.Service.Models;

namespace DuplicateFileManager.Service.Services;

public interface IFileManagerService
{
    Task DeleteFiles(DeleteFilesRequest request, CancellationToken cancellationToken);
    Task<List<DuplicateFileData>> GetDuplicateFiles(GetDuplicateFilesRequest? request, CancellationToken cancellationToken);
}