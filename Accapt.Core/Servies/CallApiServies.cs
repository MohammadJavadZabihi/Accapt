using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class CallApiServies : ICallApiServies
    {
        private IApiCallServies _callServies;

        public CallApiServies(IApiCallServies apiCallServies)
        {
            _callServies = apiCallServies ?? throw new ArgumentException("apiCallServies");
        }

        public async Task<ApiResponse<T>> SendDeletRequest<T>(string urlt, object? data = null, string jwt = "")
        {
            ApiResponse<T> apiResponse = await _callServies.SendRequest<T>(HttpMethod.Delete, urlt, data, jwt);
            return new ApiResponse<T>
            {
                Data = apiResponse.Data,
                IsSuccess = apiResponse.IsSuccess,
                Message = apiResponse.Message
            };
        }

        public async Task<ApiResponse<T>> SendGetRequest<T>(string urlt, object? data = null, string jwt = "")
        {
            return await _callServies.SendRequest<T>(HttpMethod.Get, urlt, data, jwt);
        }

        public async Task<ApiResponse<T>> SendPatchRequest<T>(string urlt, object? data = null, string jwt = "")
        {
            ApiResponse<T> apiResponse = await _callServies.SendRequest<T>(HttpMethod.Patch, urlt, data, jwt);
            return new ApiResponse<T>
            {
                Data = apiResponse.Data,
                IsSuccess = apiResponse.IsSuccess,
                Message = apiResponse.Message
            };
        }

        public async Task<ApiResponse<T>> SendPostRequest<T>(string urlt, object? data = null, string jwt = "")
        {
            ApiResponse<T> apiResponse = await _callServies.SendRequest<T>(HttpMethod.Post, urlt, data, jwt);
            return new ApiResponse<T>
            {
                Data = apiResponse.Data,
                IsSuccess = apiResponse.IsSuccess,
                Message = apiResponse.Message
            };
        }

        public async Task<ApiResponse<T>> SendPutRequest<T>(string urlt, object? data = null, string jwt = "")
        {
            ApiResponse<T> apiResponse = await _callServies.SendRequest<T>(HttpMethod.Put, urlt, data, jwt);
            return new ApiResponse<T>
            {
                Data = apiResponse.Data,
                IsSuccess = apiResponse.IsSuccess,
                Message = apiResponse.Message
            };
        }
    }
}
