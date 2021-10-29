using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace WebApplication3
    {
        public class researcher
        {


            public DataTable GetResearcher(string connectionString, int id)
            {
                string queryString = "SELECT *, CONCAT(CONCAT(name, ' ' ), familyname) as NomeCognome from germschema_viola.researcher   ";
                if (id > 0)
                {
                queryString += " WHERE researcher_id = :id ";
                }

                DataSet ds_a = new DataSet();

                using (NpgsqlConnection connection =
                    new NpgsqlConnection(connectionString))
                {
                    NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                    try
                    {
                        connection.Open();

                        NpgsqlDataAdapter myDataAdapter = new NpgsqlDataAdapter(command);
                        myDataAdapter.Fill(ds_a, "researcher");

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