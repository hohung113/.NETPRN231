using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Helper
    {
        public static string? GetString(string text)
        {

            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", true, true)
              .Build();
            string? connectionString = null;
            switch (text)
            {
                case "DataSource":
                    connectionString = config["ConnectionStrings:eStore"];
                    break;
                case "EmailAdmin":
                    connectionString = config["AdminAccount:Email"];
                    break;
                case "PassAdmin":
                    connectionString = config["AdminAccount:Password"];
                    break;
                default:
                    break;
            }
            return connectionString;
        }
    }
}
