using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;

namespace CosmosDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CosmosTableController : Controller
    {
       private CloudStorageAccount storageAccount;
        private readonly string connectionString ;

        public CosmosTableController()
        {
            connectionString = "";
            storageAccount = CloudStorageAccount.Parse(connectionString);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCustomer(string tableName, string partitionKey, string rowKey)
        {
            CloudTableClient tableClinet = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClinet.GetTableReference(tableName);

            TableOperation tableOperation = TableOperation.Retrieve<TableCustomer>(partitionKey, rowKey);
            TableResult result = await table.ExecuteAsync(tableOperation);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateCustomer(string tableName, [FromBody] TableCustomer customer)
        {
            CloudTableClient tableClinet = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClinet.GetTableReference(tableName);

            TableOperation tableOperation = TableOperation.Insert(customer);
            TableResult result = await table.ExecuteAsync(tableOperation);
            return Ok(result);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteCustomer(string tableName, [FromBody] TableCustomer customer)
        {
            CloudTableClient tableClinet = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClinet.GetTableReference(tableName);

            TableOperation tableOperation = TableOperation.Delete(customer);
            TableResult result = await table.ExecuteAsync(tableOperation);
            return Ok(result);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateCustomer(string tableName, [FromBody] TableCustomer customer)
        {
            CloudTableClient tableClinet = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClinet.GetTableReference(tableName);

            TableOperation tableOperation = TableOperation.InsertOrReplace(customer);
            TableResult result = await table.ExecuteAsync(tableOperation);
            return Ok(result);
        }
    }
}
    