using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OnMed.Persistance.Common.Helpers;
using OnMed.Service.Interfaces.Common;

namespace OnMed.Service.Services.Common;

#pragma warning disable
public class FileService : IFileService
{
    private readonly string MEDIA = "media";
    private readonly string STORAGE = "storage";
    private readonly string IMAGES = "images";

    private readonly string ROOTPATH;

    public FileService(IWebHostEnvironment env)
    {
        ROOTPATH = env.WebRootPath;
    }

    public Task<bool> DeleteAvatarAsync(string subpath)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteImageAsync(string subpath)
    {
        if (subpath == "") return true;
        string path = Path.Combine(ROOTPATH, subpath);
        if (File.Exists(path))
        {
            await Task.Run(() =>
            {
                File.Delete(path);
            });
            return true;
        }
        else return false;
    }

    public Task<string> UploadAvatarAsync(IFormFile avatar, string folderName)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UploadImageAsync(IFormFile image, string folderName)
    {
        string newImageName = MediaHelper.MakeImageName(image.FileName);
        string subpath = Path.Combine(STORAGE, IMAGES, folderName, newImageName);
        string path = Path.Combine(ROOTPATH, subpath);

        var stream = new FileStream(path, FileMode.Create);
        await image.CopyToAsync(stream);
        stream.Close();

        return subpath;
    }
}
