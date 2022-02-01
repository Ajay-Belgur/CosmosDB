using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDB.Services
{
    public class CosmosDBClient
    {
        private string accountKey = "";
        private string endPoint = "";
        
        public CosmosClient GetCosmosClient()
        {
            CosmosClient client = new CosmosClient(endPoint, accountKey);
            return client;

        }

    }
}
