using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SOAP_PrivateProject
{
    //TIL EKSAMEN
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {


        #region CONNECTION STRING
        private string ConnectionString = "Server=tcp:natascha.database.windows.net,1433;Initial Catalog = School; Persist Security Info=False;User ID = nataschajakobsen; Password= Roskilde4000;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30";
        #endregion

        #region ADD MOVIE VIRKER (DB)
        public int AddMovie(string titel, byte rating)
        {
            const string insertMovie = "INSERT INTO Movies (Titel, Rating) values (@titel, @rating)";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertMovie, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@titel", titel);
                    insertCommand.Parameters.AddWithValue("@rating", rating);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }

            }
        }
        #endregion

        #region GET ALL MOVIES VIRKER (DB)
        public IList<Movie> GetAllMovies()
        {
            const string selectAllMovies = "SELECT * FROM Movies ORDER BY Titel";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectAllMovies, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Movie> MovieList = new List<Movie>();
                        while (reader.Read())
                        {
                            Movie movies = ReadMovie(reader);
                            MovieList.Add(movies);
                        }
                        return MovieList;
                    }
                }
            }
        }
        #endregion

        #region READ MOVIE 
        private static Movie ReadMovie(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string titel = reader.GetString(1);
            int rating = reader.GetInt32(2);
            //DateTime timeStamp = reader.GetDateTime(3);
            Movie movie = new Movie
            {
                Id = id,
                Titel = titel,
                Rating = rating,
                //TimeStamp = timeStamp
            };
            return movie;
        }
        #endregion

        //public Movie GetMovieById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public IList<Movie> GetMovieById(string titel)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
