using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeD.Application.Abstractions
{
    public interface ISerializerProvider
    {
        string Serialize(object additionalData);

        T Deserialize<T>(string data);
    }
}
