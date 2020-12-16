using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HttpClient
{
    public class WebApiClient
    {
        private System.Net.Http.HttpClient _client;
        public WebApiClient(string address)
        {
            _client = new System.Net.Http.HttpClient();
            _client.BaseAddress = new Uri(address);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
        }

        public async Task RefreshTokenAsync<T>(string requestUri, T entity)
        {
            var response = await _client.PostAsJsonAsync(requestUri, entity);
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadAsStringAsync();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<int> PostAsync<T>(string requestUri, T entity)
        {
            var response = await _client.PostAsJsonAsync(requestUri, entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<int>();
        }

        public async Task<T> GetAsync<T>(string requestUri, int id)
        {
            var response = await _client.GetAsync($"{requestUri}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<IEnumerable<T>> GetAsync<T>(string requestUri)
        {
            var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IEnumerable<T>>();
        }

        public async Task PutAsync<T>(string requestUri, int id, T entity)
        {
            var response = await _client.PutAsJsonAsync($"{requestUri}/{id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string requestUri, int id)
        {
            var response = await _client.DeleteAsync($"{requestUri}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}