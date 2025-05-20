using Elastic.Clients.Elasticsearch;

namespace FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.ElasticSearch
{
    public interface IElasticClient<T>
    {
        Task<bool> Create(T log, IndexName index);

        Task<bool> Replace(string id, T document, IndexName index);
    }
}
