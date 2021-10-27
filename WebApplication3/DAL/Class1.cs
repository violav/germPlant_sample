using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Data;
using System.Data.SqlClient;
using Npgsql;

namespace WebApplication3
{

    public class Class1
    {

        public DataTable GetData(string connectionString)
        {
            return GetData(0, connectionString);
        }
        public DataTable GetData(int id, string connectionString)
        {

            // Provide the query string with a parameter placeholder.
            string queryString = "";
            if (id > 0)
            {
                queryString =
   "SELECT A.* from (select * FROM germschema_viola.accession   ";

                queryString += " WHERE accession_id = :id ) AS A";
            }
            else
            {

                queryString = "SELECT A.* from germschema_viola.accession as A  ";

            }

            queryString += " INNER JOIN germschema_viola.COUNTRIES as B ";
            queryString += "  ON A.COUNTRY_ID = B.COUNTRY_ID ";
            queryString += " INNER JOIN germschema_viola.INSTITUTE as C ";
            queryString += "  ON C.INSTITUTE_ID = A.INSTITUTE_ID ";
            queryString += " LEFT JOIN germschema_viola.TAXONOMY as D ";
            queryString += "  ON D.TAXONOMY_ID = A.TAXONOMY_ID ";


            DataSet ds_a = new DataSet();

            // Specify the parameter value.
            int paramValue = 5;

            // Create and open the connection in a using block. This
            // ensures that all resources will be closed and disposed
            // when the code exits.
            using (NpgsqlConnection connection =
                new NpgsqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                command.Parameters.AddWithValue("id", id);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();

                    NpgsqlDataAdapter myDataAdapter = new NpgsqlDataAdapter(command);
                    myDataAdapter.Fill(ds_a, "Library");

                    if (ds_a != null)
                    {
                        if (ds_a.Tables.Count > 0)
                        {
                            return ds_a.Tables[0];
                        }

                    }

                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
                Console.ReadLine();
            }
        }

        public void putAccession(string connectionString, int opId, int siteId)
        {
            // Provide the query string with a parameter placeholder.
            string queryString = "INSERT INTO  germschema_viola.conservation (OPERATION_ID, SITE_ID) VALUES (:OPERATION_ID, :SITE_ID)  ";
            
            DataSet ds_a = new DataSet();

            // Specify the parameter value.
            int paramValue = 5;

            // Create and open the connection in a using block. This
            // ensures that all resources will be closed and disposed
            // when the code exits.
            using (NpgsqlConnection connection =
                new NpgsqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                command.Parameters.AddWithValue("OPERATION_ID", opId);
                command.Parameters.AddWithValue("SITE_ID", siteId);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();               
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }
        }
    }
}