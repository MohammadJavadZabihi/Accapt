using Accapt.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IApiCallServies
    {
        Task<ApiResponse<T>> SendRequest<T>(HttpMethod method, string url, object? data, string jwt);
    }
}
