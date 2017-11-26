using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyBodyApp.Abstractions
{
    public interface ICloudService
    {
        ICloudTable<T> GetTable<T>() where T : TableData;
    }
}
