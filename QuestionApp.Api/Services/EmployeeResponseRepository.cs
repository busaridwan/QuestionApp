public class EmployeeResponseRepository : IEmployeeResponseRepository
{
    private readonly Container _container;

    public EmployeeResponseRepository(CosmosClient cosmosClient, string databaseName, string containerName)
    {
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task AddEmployeeResponseAsync(EmployeeResponse employeeResponse)
    {
        await _container.CreateItemAsync(employeeResponse, new PartitionKey(employeeResponse.Id));
    }

    public async Task<EmployeeResponse> GetEmployeeResponseAsync(string id)
    {
        try
        {
            ItemResponse<EmployeeResponse> response = await _container.ReadItemAsync<EmployeeResponse>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException)
        {
            return null;
        }
    }

    public async Task<IEnumerable<EmployeeResponse>> GetAllEmployeeResponsesAsync()
    {
        var query = _container.GetItemQueryIterator<EmployeeResponse>();
        List<EmployeeResponse> results = new List<EmployeeResponse>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }
}
