using Quiz.Contracts.Constants;
using Quiz.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Quiz.Contracts.Constants.Common;

namespace Quiz.Contracts.Services
{
    public class FileStorageService : IFileStorage
    {
        private readonly IConfiguration _configuration;
        private readonly StorageType _storageType;
        private readonly IUnitOfWork _repository;
        public FileStorageService(IConfiguration configuration, IUnitOfWork repository)
        {
            _configuration = configuration;
            _repository = repository;
            _storageType = (StorageType)Enum.Parse(typeof(StorageType), _configuration["FileStorage:StorageType"], true);
        }

        public void Delete(Guid id, string container)
        {
            switch (_storageType)
            {
                case Common.StorageType.Local:
                    //var file = _repository.CTRFiles.GetQueryable().Where(x => x.ID == id).SingleOrDefault();
                    //if (file != null)
                    //{
                    //    string path = Path.Combine(_configuration["FileStorage:LocalPath"], file.FILE_PATH);
                    //    File.Delete(path);
                    //}
                    break;
                case Common.StorageType.Blob:

                    break;
                case Common.StorageType.Remote:
                    break;
                default: throw new NotImplementedException();
            }
        }

        public async Task<byte[]> Download(Guid id, string container)
        {
            byte[] content = null;

            switch (_storageType)
            {
                case Common.StorageType.Local:

                    var directory = Path.Combine(_configuration["FileStorage:LocalPath"], container);
                    var dir = Directory.GetFiles(directory).ToList();
                    var file = dir.Where(x => x.Contains(id.ToString())).FirstOrDefault();

                    if (!string.IsNullOrEmpty(file))
                    {
                        string path = Path.Combine(directory, file);
                        content = await File.ReadAllBytesAsync(file);
                    }
                    break;
                case Common.StorageType.Blob:

                    break;
                case Common.StorageType.Remote:
                    break;
                default: throw new NotImplementedException();
            }

            return content;
        }
        public async Task<string> Upload(Guid filename, string extension, string container, byte[] content, FileTypeEnum type, string category, string? extend = "")
        {
            string subFolder = type == FileTypeEnum.Image ? Common.ImageRoot : Common.FileRoot;
            subFolder = subFolder.Replace("{Comcode}", container).Replace("{Category}", category);
            switch (_storageType)
            {
                case Common.StorageType.Local:

                    var directory = _configuration["FileStorage:LocalPath"] + subFolder;

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    await File.WriteAllBytesAsync(Path.Combine(directory, extend + filename.ToString() + extension), content);
                    break;
                case Common.StorageType.Blob:

                    break;
                case Common.StorageType.Remote:
                    break;
                default: throw new NotImplementedException();
            }

            return subFolder + extend + filename + extension;
        }

        public string GetLocalEnvironment()
        {
            return _configuration["FileStorage:LocalEnvironment"];
        }

        public string GetLocalPath()
        {
            return _configuration["FileStorage:LocalPath"];
        }
    }
}
