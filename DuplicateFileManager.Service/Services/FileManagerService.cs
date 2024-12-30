using System.Collections.Concurrent;
using System.Text.RegularExpressions;

using DuplicateFileManager.Service.Models;

namespace DuplicateFileManager.Service.Services;

public class FileManagerService(IFileService fileService) : IFileManagerService
{
    private readonly Regex _duplicatedFileNameRegex = new(@"([\s-]+[cC]opy[\(\)\s0-9]*)+(?=\.[a-zA-Z0-9]+)", RegexOptions.Compiled);

    public async Task DeleteFiles(DeleteFilesRequest request, CancellationToken cancellationToken)
    {
        await Parallel.ForEachAsync(request.FilePaths, cancellationToken, (filePath, token) =>
        {
            token.ThrowIfCancellationRequested();
            fileService.DeleteFile(filePath);
            return ValueTask.CompletedTask;
        });
    }

    public async Task<List<DuplicateFileData>> GetDuplicateFiles(GetDuplicateFilesRequest? request, CancellationToken cancellationToken)
    {
        var filePaths = fileService.GetFilesFromDirectory(request.DirectoryPath, request.FileExtensions);
        var fileDictionary = new ConcurrentBag<FilePathHashPair>();

        await Parallel.ForEachAsync(filePaths, cancellationToken, (filePath, token) =>
        {
            token.ThrowIfCancellationRequested();

            var fileHash = fileService.GetFileHash(filePath);
            fileDictionary.Add(new(filePath, fileHash));

            return ValueTask.CompletedTask;
        });

        return fileDictionary.GroupBy(x => x.FileHash)
            .Where(x => x.Count() > 1)
            .Select(x => new DuplicateFileData(
                Path.GetFileName(x.MaxBy(y => _duplicatedFileNameRegex.Replace(y.FilePath, string.Empty, Int32.MaxValue))!.FilePath),
                x.Select(y => y.FilePath).OrderBy(y => y.Length).ToList()))
            .OrderBy(x => x.CommonFileName)
            .ThenBy(x => x.FilePaths.Count)
            .ToList();
    }
}