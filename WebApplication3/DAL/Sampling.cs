using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication3
{
    public class Sampling
    {


        public DataTable GetSamplingSites(string connectionString, int country_id)
        {
            string queryString = "SELECT *  FROM  germschema_viola.SAMPLINGSITE  ";


            if (country_id > 0)
            {
                 queryString += "   where country_id = :country_id  ";
            }

            DataSet ds_a = new DataSet();

            using (NpgsqlConnection connection =
                new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                command.Parameters.AddWithValue("country_id", country_id);

                try
                {
                    connection.Open();

                    NpgsqlDataAdapter myDataAdapter = new NpgsqlDataAdapter(command);
                    myDataAdapter.Fill(ds_a, "COUNTRIES");

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

    }
}