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
            private const string filepath = "C:/Users/Owner/Desktop/Revature/Sean-Letts/Project1_Sean_Letts/User/UserDatabase/";
            private readonly string connectionString;
            public ReviewLogic(string connectionString)
            {
                this.connectionString = connectionString;
            }
            /// <summary>
            /// Gather and return a list of all reviews.
            /// </summary>
            /// <returns>List of all Reviews</returns>
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
            /// <summary>
            /// Displays all reviews w/ assigned resturants.
            /// </summary>
            public void LookAtAllReviews()
            {
                ResturantLogic tempLogic = new ResturantLogic(connectionString);
                var reviews = GetAllReviews();
                var resturants = tempLogic.GetAllResturants();
                foreach (var review in reviews)
                {
                    Console.WriteLine("==========");
                    Console.WriteLine($"Resturant Name: {resturants[review.ResturantId - 1].name}");
                    Console.WriteLine($"Rating: {review.rating} / 5 Stars");
                    Console.WriteLine($"Review: {review.reviewtext}");
                }
                Console.WriteLine("Select Enter to continue.");
                Console.ReadLine();
            }
            /// <summary>
            /// Add a new review to the database
            /// </summary>
            public void AddNewReview()
            {
                ResturantLogic tempLogic = new ResturantLogic(connectionString);
                var resturants = tempLogic.GetAllResturants();
                bool inloop = true;
                while (inloop)
                {
                    Console.WriteLine("Please enter the name of the resturant you wish to review.");
                    string name = Console.ReadLine();
                    int count = 1;
                    foreach (ResturantInfo res in resturants)
                    {
                        if (res.name == name)
                        {
                            bool inloop2 = true;
                            while (inloop2)
                            {
                                Console.WriteLine("How many stars would you give your expierence?");
                                Console.WriteLine("Select between 1 and 5, decimals are allowed.");
                                string rating = Console.ReadLine();
                                decimal ratingNumber;
                                if(Decimal.TryParse(rating, out ratingNumber))
                                {
                                    if(ratingNumber >= 1.00m && ratingNumber <= 5.00m)
                                    {
                                        Console.WriteLine("Please enter the contents of your review.");
                                        string reviewTxt = Console.ReadLine();

                                        string commandString = "INSERT INTO reviews (ResturantID, Rating, Review) " +
                                        "VALUES (@idval, @rate, @reviewtxt);";
                                        using SqlConnection connection = new(connectionString);
                                        using SqlCommand command = new(commandString, connection);

                                        command.Parameters.AddWithValue("@idval", count);
                                        command.Parameters.AddWithValue("@rate", ratingNumber);
                                        command.Parameters.AddWithValue("@reviewtxt", reviewTxt);
                                        connection.Open();
                                        command.ExecuteNonQuery();
                                        connection.Close();
                                        Console.WriteLine("Review has been added! Press enter to continue back to the previous page.");
                                        Console.ReadLine();
                                        return;
                                    }
                                    else
                                    {
                                        Console.WriteLine("You entered a value that is out of bounds between 1 and 5.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("You entered a value that is not a decimal.");
                                }
                            }
                        }
                        count++;
                    }
                    Console.WriteLine("You entered an invalid name. Please try again.");
                }
            }
            /// <summary>
            /// Gets all reviews from the DB async
            /// </summary>
            /// <returns></returns>
            public async Task<List<ReviewsInfo>> GetAllReviewsAsync()
            {
                string commandString = "SELECT * FROM reviews;";

                using SqlConnection connection = new(connectionString);
                using SqlCommand command = new(commandString, connection);
                IDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new();
                try
                {
                    await connection.OpenAsync();
                    adapter.Fill(dataSet); // this sends the query. DataAdapter uses a DataReader to read.
                    await connection.CloseAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }

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
            /// <summary>
            /// Async adding a review to the db
            /// </summary>
            /// <param name="name"></param>
            /// <param name="rating"></param>
            /// <param name="reviewtext"></param>
            /// <returns>The new Review to be added</returns>
            /// <exception cref="Exception">throws exception when there's an issue w/ user input</exception>
            public async Task<ReviewsInfo> AddNewReviewAPI(string name, decimal rating, string reviewtext)
            {
                if(name.Length == 0 || rating == 0 )
                    throw new Exception("Empty Values in name or rating");
                ResturantLogic tempLogic = new ResturantLogic(connectionString);
                var resturants = tempLogic.GetAllResturants();
                int count = 1;
                foreach (ResturantInfo res in resturants)
                {
                    if (res.name == name)
                    {
                        bool inloop2 = true;
                        while (inloop2)
                        {

                            if (rating >= 1.00m && rating <= 5.00m)
                            {

                                string commandString = "INSERT INTO reviews (ResturantID, Rating, Review) " +
                                "VALUES (@idval, @rate, @reviewtxt);";
                                using SqlConnection connection = new(connectionString);
                                using SqlCommand command = new(commandString, connection);

                                ReviewsInfo temp = new ReviewsInfo{
                                    ResturantId = count,
                                    rating = rating,
                                    reviewtext = reviewtext };
                                command.Parameters.AddWithValue("@idval", count);
                                command.Parameters.AddWithValue("@rate", rating);
                                command.Parameters.AddWithValue("@reviewtxt", reviewtext);
                                try
                                {
                                    await connection.OpenAsync();
                                    await command.ExecuteNonQueryAsync();
                                    await connection.CloseAsync();
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                return temp;
                            }
                            else
                            {
                                throw new Exception("Rating is either below 1.0, or above 5.0");
                            }
                        }
                    }
                    count++;
                }
                throw new Exception($"Cannot find a resturant named {name} in DB"); 
            }
        }
    }

}
