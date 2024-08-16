using Azure.Data.Tables;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Class1
    {
        // C# record type for items in the table
        public record Product : ITableEntity
        {
            public string RowKey { get; set; } = default!;

            public string PartitionKey { get; set; } = default!;

            public string Name { get; init; } = default!;

            public int Quantity { get; init; }

            public bool Sale { get; init; }

            public ETag ETag { get; set; } = default!;

            public DateTimeOffset? Timestamp { get; set; } = default!;
        }
    }
}
