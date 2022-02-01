using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDB
{
    public class TableCustomer : TableEntity
    {
        public string customername { get; set; }

        public TableCustomer()
        {

        }
        public TableCustomer(string city, string id, string name)
        {
            PartitionKey = city;
            RowKey = id;
            customername = name;
        }
    }
}
