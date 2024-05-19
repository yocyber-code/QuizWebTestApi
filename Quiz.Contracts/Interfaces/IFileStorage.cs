using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Quiz.Contracts.Constants.Common;

namespace Quiz.Contracts.Interfaces
{
    public interface IFileStorage
    {
        Task<string> Upload(Guid filename, string extension, string container, byte[] content, FileTypeEnum type, string category, string? extend = "");
        Task<byte[]> Download(Guid id, string container);
        void Delete(Guid id, string container);
        string GetLocalEnvironment();
        string GetLocalPath();
    }
}
