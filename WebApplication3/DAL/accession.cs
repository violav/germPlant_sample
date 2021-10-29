using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication3
{
    public class accession
    {


        public DataTable GetAccessions(string connectionString, int accessionId)
        {
            string queryString = "SELECT A.*,CONCAT(CONCAT( A.UNIMI_NUMBER ,  CONCAT( ' - ', C.description)),  CONCAT( ' - ',  D.description)) as description  from germschema_viola.accession as A  ";
             queryString += "  JOIN germschema_viola.TAXONOMY as B  ";
            queryString += "  ON  B.taxonomy_id = A.taxonomy_id  ";
            queryString += "  JOIN germschema_viola.GENUS as C  ";
            queryString += "  ON  B.GENUS_id = C.GENUS_id  ";
            //queryString += "  JOIN germschema_viola.SPECIES as D  ";
            //queryString += "  ON  B.species_id = D.species_id  ";

            queryString += "  JOIN germschema_viola.ACCESSION as E  ";
            queryString += "  ON  E.TAXONOMY_ID = A.TAXONOMY_ID  ";
            queryString += "  WHERE E.ACCESSION_ID = :accession_id  " ;


            DataSet ds_a = new DataSet();

            using (NpgsqlConnection connection =
                new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                command.Parameters.AddWithValue("accession_id", accessionId);

                try
                {
                    connection.Open();

                    NpgsqlDataAdapter myDataAdapter = new NpgsqlDataAdapter(command);
                    myDataAdapter.Fill(ds_a, "accession");

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