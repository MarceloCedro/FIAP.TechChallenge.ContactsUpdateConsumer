using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.ElasticSearch;

namespace FIAP.TechChallenge.ContactsUpdateConsumer.Infrastructure.ElasticSearch
{
    public class ElasticClient<T>(IElasticSettings settings) : IElasticClient<T>
    {
        private readonly ElasticsearchClient _client = new ElasticsearchClient(settings.CloudId, new ApiKey(settings.ApiKey));

        public async Task<bool> Create(T log, IndexName index)
        {
            var response = await _client.IndexAsync(log, index);

            return response.IsValidResponse;
        }

        public async Task<bool> Replace(string id, T document, IndexName index)
        {
            var response = await _client.IndexAsync(document, i => i
                .Index(index)
                .Id(id)
            );

            return response.IsValidResponse;
        }
    }
}
