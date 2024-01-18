using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ApiService
{
	private readonly HttpClient _httpClient;
	private readonly string _baseApiUrl;

	public ApiService(HttpClient httpClient)
	{
		_httpClient = httpClient;
		_baseApiUrl = "http://103.77.214.148/api";
	}

	private string GetApiUrl(string endpoint) => $"{_baseApiUrl}/{endpoint}";


	private async Task<T> HandleResponse<T>(Task<HttpResponseMessage> task)
	{
		var response = await task;
		if (!response.IsSuccessStatusCode)
		{
			string errorMessage = await response.Content.ReadAsStringAsync();
			return default;
		}
		var data = await response.Content.ReadAsStringAsync();
		if (data.Equals("") && response.IsSuccessStatusCode)
		{
			return (T)(object)true;
		}

		return JsonConvert.DeserializeObject<T>(data);
	}


	public async Task<T> GetAsync<T>(string endpoint) =>
		await HandleResponse<T>(_httpClient.GetAsync(GetApiUrl(endpoint)));

	public async Task<bool> PutAsync<T>(string endpoint, T data) =>
		await HandleResponse<bool>(_httpClient.PutAsync(GetApiUrl(endpoint), CreateJsonContent(data)));

	public async Task<T> PostAsync<T>(string endpoint, T data) =>
		await HandleResponse<T>(_httpClient.PostAsync(GetApiUrl(endpoint), CreateJsonContent(data)));

	public async Task<bool> DeleteAsync(string endpoint) =>
		await HandleResponse<bool>(_httpClient.DeleteAsync(GetApiUrl(endpoint)));

	private StringContent CreateJsonContent<T>(T data)
	{
		string jsonData = JsonConvert.SerializeObject(data);
		return new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
	}
}
