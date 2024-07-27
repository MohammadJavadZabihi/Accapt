using Accapt.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface ICallApiServies
    {
        Task<ApiResponse<T>> SendGetRequest<T>(string urlt, object? data = null, string jwt = "");

        Task<ApiResponse<T>> SendPostRequest<T>(string urlt, object? data = null, string jwt = "");

        Task<ApiResponse<T>> SendDeletRequest<T>(string urlt, object? data = null, string jwt = "");

        Task<ApiResponse<T>> SendPutRequest<T>(string urlt, object? data = null, string jwt = "");

        Task<ApiResponse<T>> SendPatchRequest<T>(string urlt, object? data = null, string jwt = "");
    }
}
