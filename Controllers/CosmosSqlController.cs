using CosmosDB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace CosmosDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CosmosSqlController : ControllerBase
    {
        private CosmosDBClient _cosmosClient;
        public CosmosSqlController(CosmosDBClient cosmosCLient)
        {
            _cosmosClient = cosmosCLient;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCustomer(string databseName, string containerName, string customerId, string partitionKey)
        {
            using (CosmosClient client = _cosmosClient.GetCosmosClient())
            {

                var databaseTask = await client.CreateDatabaseIfNotExistsAsync(databseName);
                Database database = databaseTask.Database;

                Container container = database.GetContainer(containerName);

                ItemResponse<Customer> response = await container.ReadItemAsync<Customer>(partitionKey : new PartitionKey(partitionKey), id :customerId);

                Customer customer = (Customer)response;

                return Ok(customer);

            }
                  
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateCustomer(string databseName, string containerName, [FromBody]Customer customer)
        {
            using (CosmosClient client = _cosmosClient.GetCosmosClient())
            {

                var databaseTask = await client.CreateDatabaseIfNotExistsAsync(databseName);
                Database database = databaseTask.Database;

                Container container = database.GetContainer(containerName);

                ItemResponse<Customer> response = await container.CreateItemAsync(customer);               

                return Ok();

            }

        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteCustomer(string databseName, string containerName, string partitionKey, string customerId)
        {
            using (CosmosClient client = _cosmosClient.GetCosmosClient())
            {

                var databaseTask = await client.CreateDatabaseIfNotExistsAsync(databseName);
                Database database = databaseTask.Database;

                Container container = database.GetContainer(containerName);

                ItemResponse<Customer> response = await container.DeleteItemAsync<Customer>(partitionKey : new PartitionKey(partitionKey), id : customerId);

                return Ok();

            }

        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> ReplaceCustomer(string databseName, string containerName, Customer customer)
        {
            using (CosmosClient client = _cosmosClient.GetCosmosClient())
            {

                var databaseTask = await client.CreateDatabaseIfNotExistsAsync(databseName);
                Database database = databaseTask.Database;
                Container container = database.GetContainer(containerName);
                ItemResponse<Customer> response = await container.ReplaceItemAsync<Customer>(id:customer.id, item: customer);

                return Ok();

            }

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> InvokeStoredProc(string databseName, string containerName, string storedProcedureName)
        {
            using (CosmosClient client = _cosmosClient.GetCosmosClient())
            {

                var databaseTask = await client.CreateDatabaseIfNotExistsAsync(databseName);
                Database database = databaseTask.Database;
                Container container = database.GetContainer(containerName);

                var procs = container.Scripts;
                var key = new PartitionKey("");
                var result = await procs.ExecuteStoredProcedureAsync<string>(storedProcedureName, key, null);

                return Ok();

            }

        }


    }
}
