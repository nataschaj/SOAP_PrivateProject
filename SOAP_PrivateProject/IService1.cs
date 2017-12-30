using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel.Web;
using System.Text;

namespace SOAP_PrivateProject
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        IList<Movie> GetAllMovies();

        //[OperationContract]
        //Movie GetMovieById(int id);

        //[OperationContract]
        //IList<Movie> GetMovieById(string titel);

        [OperationContract]
        int AddMovie(string titel, byte rating);

    }

    [DataContract]
    public class Movie
    {
        [DataMember]
        public int Id;

        [DataMember]
        public DateTime TimeStamp;

        [DataMember]
        public string Titel;

        [DataMember]
        public int Rating; // SQL Server tinyint = C# byte
    }


}
