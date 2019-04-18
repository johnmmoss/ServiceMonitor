using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tfs.BuildDto;
using Tfs.PullRequestDto;
using Tfs.ReleaseDto;
using TfsClient;

namespace Tfs.Client
{
    public class TfsRepository
    {
        private TfsUrl _tfsUrl;
        private string _accessToken;

        public TfsRepository(TfsClientOptions _tfsClientOptions)
        {
            _accessToken = _tfsClientOptions.AccessToken;
            _tfsUrl = new TfsUrl(_tfsClientOptions.Instance);            
        }

        public async Task<IList<TfsRelease>> GetTfsReleaseAsync(string projectName, int definitionId)
        {
            var url = _tfsUrl.GetReleaseUrl(projectName, definitionId);
            var dto = await Get<Tfs.ReleaseDto.RootObject>(url);
            return dto.value;
        }

        public async Task<IList<TfsBuild>> GetTfsBuildsAsync(string projectName, int defintionId)
        {
            var url = _tfsUrl.GetTfsBuildApi(projectName) + $"&definitions={defintionId}";
            var dto = await Get<Tfs.BuildDto.RootObject>(url);
            return dto.value;
        }

        public async Task<IList<TfsPullRequest>> GetTfsPullRequestAsync()
        {
            var url = _tfsUrl.PullRequestsApi;
            var dto = await Get<Tfs.PullRequestDto.RootObject>(url);
            return dto.value;
        }
        public async Task<T> Get<T>(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        System.Text.ASCIIEncoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", "", _accessToken))));

                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseBody);
                }
            }
        }
    }
}