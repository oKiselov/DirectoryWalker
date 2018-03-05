using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectoryWalker.Database.Repositories.Interfaces
{
    public interface ITreeReadRepository
    {
        int Val { get; set; }
        Task<bool> CheckIfPathExists(IEnumerable<string> combinedPath);
    }
}
