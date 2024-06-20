using Microsoft.Azure.Cosmos;
using QuestionApp.Api.Models;

namespace QuestionApp.Api.Services;
public class QuestionRepository : IQuestionRepository
{
    private readonly Container _container;
    public QuestionRepository(
        string connection,
        string key,
        string databaseName,
        string containerName
    ){
        var cosmosClient = new CosmosClient(connection, key, new CosmosClientOptions(){});
        _container = cosmosClient.GetContainer(databaseName, containerName);

    }

    public async Task<IEnumerable<Question>> GetQuestionsAsync(){
        var query = _container
        .GetItemQueryIterator<Question>(new QueryDefinition("SELECT * FROM c"));
        
        var results = new List<Question>();
        while (query.HasMoreResults){
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }
    public async Task<Question> GetQuestionAsync(string id, QuestionType type){
        try{
            var response = await _container.ReadItemAsync<Question>(id, new PartitionKey(type.ToString()));
            return response;
        }catch(Exception e){
            Console.WriteLine(e);
            return null;
        }
    }
    public async Task<Question> CreateQuestionAsync(Question question){
        var response = await _container.CreateItemAsync(question, new PartitionKey(question.Type.ToString()));
        return response.Resource;

    }
    public async Task<Question> UpdateQuestionAsync(string id, Question question){
        var response = await _container.UpsertItemAsync(question, new PartitionKey(question.Type.ToString()));
        return response.Resource;
    }
    public async Task<bool> DeleteQuestion(string id, QuestionType type){
        var response = await _container.DeleteItemAsync<Question>(id, new PartitionKey(type.ToString()));
        if(response.Resource != null)
            return false;
        
        return true;
    }
}