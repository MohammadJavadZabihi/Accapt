using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Accapt.Core.Servies
{
    public class ApiCallServies : IApiCallServies
    {
        public async Task<ApiResponse<T>> SendRequest<T>(HttpMethod method, string url, object? data, string jwt)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(method, url);
            if (data != null)
            {
                string content = JsonConvert.SerializeObject(data);
                httpRequestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }

            if (!string.IsNullOrEmpty(jwt))
            {
                httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }

            try
            {
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string text = await httpResponseMessage.Content.ReadAsStringAsync();
                    T val = JsonConvert.DeserializeObject<T>(text);
                    if (val != null)
                    {
                        return new ApiResponse<T>
                        {
                            Data = val,
                            IsSuccess = true,
                            Message = "Request Is Successfully"
                        };
                    }

                    return new ApiResponse<T>
                    {
                        Data = default(T),
                        IsSuccess = false,
                        Message = "Error Message : " + text
                    };
                }

                ApiResponse<T> apiResponse = new ApiResponse<T>
                {
                    Data = default(T),
                    IsSuccess = false
                };
                ApiResponse<T> apiResponse2 = apiResponse;
                apiResponse2.Message = "Error Message : " + await httpResponseMessage.Content.ReadAsStringAsync();
                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponse<T>
                {
                    Data = default(T),
                    IsSuccess = false,
                    Message = "Error Message : " + ex.Message
                };
            }
        }
    }
}
