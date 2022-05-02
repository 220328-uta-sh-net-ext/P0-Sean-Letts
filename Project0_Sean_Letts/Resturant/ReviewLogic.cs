using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Resturant
    {
        public class ReviewLogic
        {
            private const string filepath = "C:/Users/Owner/Desktop/Revature/Sean-Letts/Project0_Sean_Letts/User/UserDatabase/";
            private readonly string connectionString;
            public ReviewLogic(string connectionString)
            {
                this.connectionString = connectionString;
            }
            public List<ReviewsInfo> GetAllReviews()
            {
                string commandString = "SELECT * FROM reviews;";

                using SqlConnection connection = new(connectionString);
                using SqlCommand command = new(commandString, connection);
                IDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new();
                connection.Open();
                adapter.Fill(dataSet); // this sends the query. DataAdapter uses a DataReader to read.
                connection.Close();

                var reviews = new List<ReviewsInfo>();

                DataColumn levelColumn = dataSet.Tables[0].Columns[2];
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    reviews.Add(new ReviewsInfo
                    {
                        ResturantId = (int)row["ResturantID"],
                        rating = (decimal)row["Rating"],
                        reviewtext = (string)row["Review"]
                    });
                }
                return reviews;
            }
        }
    }

}
