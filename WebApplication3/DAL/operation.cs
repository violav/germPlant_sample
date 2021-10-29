using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication3
{
    public class operation
    {


        public DataTable GetGeneticProgram(string connectionString)
        {
            string queryString = "SELECT A.* from germschema_viola.genetic_program as A  ";
         

            DataSet ds_a = new DataSet();

            using (NpgsqlConnection connection =
                new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
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

        //public DataTable GetOperations(string connectionString, int tipeId)
        //{
        //    return GetOperations(connectionString, tipeId, 0);
        //}

        public DataTable GetFatherOperation(string connectionString, int accessionId)
        {
            //string queryString = "SELECT A.* ";
            //queryString += "from ((SELECT * from germschema_viola.operation_history   ";
            //queryString += " WHERE op_type_id =  " + ((int)operations.Genica) ;
            //queryString += " ) as A   ";
            //queryString += " INNER JOIN (SELECT * FROM germschema_viola.operation_history  ";
            //queryString += " WHERE accession_id =  " + accessionId;
            //queryString += " ) as C  ";
            //queryString += " ON C.operation_id = A.operation_id )  ";

            string queryString = " SELECT * from ( ";
            queryString += " select A.* From (";
            queryString += " 	SELECT * from germschema_viola.operation_history    WHERE op_type_id =  3  ";
            queryString += " ) as A     ";
            queryString += " INNER JOIN  ";
            queryString += " ( ";
            queryString += " 	SELECT * FROM germschema_viola.operation_history   WHERE accession_id =  9  ";
            queryString += " ) as C    ";
            queryString += " ON C.operation_id = A.operation_id ) as TAB ";
            queryString += " INNER JOIN germschema_viola.genetic_operation AS Gen ";
            queryString += " ON Gen.operation_id = TAB.operation_id ";
            queryString += " INNER JOIN germschema_viola.genetic_operation_detail AS GenDet ";
            queryString += " ON Gen.gen_op_id = GenDet.genetic_operation_id ";
            queryString += " INNER JOIN germschema_viola.operation_registry AS Reg ";
            queryString += " ON GenDet.op_registry_id = Reg.operation_id ";

            DataSet ds_a = new DataSet();

            using (NpgsqlConnection connection =
                new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
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
        /// <summary>
        /// All available operations
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public DataTable GetOperations(string connectionString, int tipeId )
        {
            string queryString = "SELECT A.* from germschema_viola.operation_registry as A  ";
            if (tipeId > 0)
            {
                 queryString += " INNER JOIN germschema_viola.operation_type_registry as B  ";
                queryString += " ON B.op_type_id = A.type_id  ";
                queryString += " WHERE A.type_id =  " + tipeId;
            }
            //if (accessionId > 0)
            //{
            //    //queryString += " INNER JOIN germschema_viola.operation_history as C  ";
            //    //queryString += " ON C.operation_reg_id = A.operation_reg_id  ";
            //    //queryString += " WHERE C.accession_id =  " + accessionId;


            //    queryString += " INNER JOIN (SELECT * FROM germschema_viola.operation_history  ";
            //    queryString += " WHERE C.accession_id =  " + accessionId;
            //    queryString += " ) as C  ";
            //    queryString += " ON C.operation_reg_id = A.operation_reg_id  ";
            //}

            DataSet ds_a = new DataSet();

            using (NpgsqlConnection connection =
                new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
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

        public DataTable GetOperations(string connectionString)
        {
            return GetOperations(connectionString, 0);
        }

        public DataTable GetGeneticOperations(string connectionString)
        {
            return GetOperations(connectionString, ((int)operations.Genica));
        }

        public DataTable GetGeneticPopulation(string connectionString)
        {
            return GetOperations(connectionString, ((int)operations.Genetic_constitution_of_the_population));
        }

        public DataTable GetGeneticOperationByAccessioId(string connectionString, int accessionId)
        {
            return GetOperations(connectionString, ((int)operations.Genica));
        }
        /// <summary>
        /// All available operations
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public DataTable GetOperationtypes(string connectionString, string tipo)
        {
            string queryString = "";

            if (!string.IsNullOrEmpty(tipo))
            {
                queryString = " SELECT * from germschema_viola.operation_type_registry where description= :tipo  ";

            }
            else 
            {
                queryString = " SELECT * from germschema_viola.operation_type_registry  ";
            }

            DataSet ds_a = new DataSet();

            using (NpgsqlConnection connection =
                new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                command.Parameters.AddWithValue("tipo", tipo);

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
        #region "GENETICOPERATION"

        public void putgeneticoperation(string connectionString, int accession_id, int researcher_id, DateTime operation_date, int user_ins, string opHistoryNote, string opPathFile, int operation_id,  int gen_program_id, int gen_population_id
                    , string gen_population_text, string gen_population_note, int access_father_id, int access_mother_id, string fieldnumber, string subfieldnumber
        , int op_father_id, string genealogy, int date_ins,  List<int> op_registry_id)
        {
          
            int headerGeneticOperationId = 0;
            int headeroperationId = 0;
            int op_type_genica_id = int.Parse( GetOperationtypes(connectionString, "Genica").Rows[0]["op_type_id"].ToString());


            // OPERATION HEADER.
            string queryString = @"INSERT INTO  germschema_viola.operation_history ( accession_id, op_type_id, operation_date, researcher_id,
                         inserting_user_id, notes, pathFile) VALUES 
                        ( :accession_id, :op_type_id, :operation_date, :researcher_id,
                         :inserting_user_id, :notes, :pathFile) RETURNING operation_id ";


            using (NpgsqlConnection connection =
              new NpgsqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);

                command.Parameters.AddWithValue("accession_id", accession_id);
                command.Parameters.AddWithValue("op_type_id", op_type_genica_id);
                command.Parameters.AddWithValue("operation_date", operation_date);
                command.Parameters.AddWithValue("researcher_id", researcher_id);

                command.Parameters.AddWithValue("inserting_user_id", user_ins);
                command.Parameters.AddWithValue("notes", opHistoryNote);
                command.Parameters.AddWithValue("pathFile", opPathFile);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    headeroperationId = int.Parse(command.ExecuteScalar().ToString());
                }
                catch (Exception ex)
                {
                    return;
                }
                finally 
                {
                    connection.Close();
                }

                if (headeroperationId == 0)
                    return;

                //// GENETIC OPERATION HEADER.
                queryString = @"INSERT INTO  germschema_viola.genetic_operation ( operation_id, op_type_id,  gen_program_id, gen_population_id,
                             gen_population_text, gen_population_note, access_father_id, access_mother_id, fieldnumber, subfieldnumber, op_father_id, genealogy,  user_ins) VALUES 
                        ( :operation_id, :op_type_id, :gen_program_id, :gen_population_id, :gen_population_text, :gen_population_note, :access_father_id, :access_mother_id, :fieldnumber, :subfieldnumber, :op_father_id, :genealogy, :user_ins ) RETURNING gen_op_id ";

  
                command = new NpgsqlCommand(queryString, connection);
                command.Parameters.AddWithValue("operation_id", headeroperationId);
                command.Parameters.AddWithValue("op_type_id", op_type_genica_id);
                command.Parameters.AddWithValue("gen_program_id", gen_program_id);
                command.Parameters.AddWithValue("gen_population_id", gen_population_id);
                command.Parameters.AddWithValue("gen_population_text", gen_population_text);
                command.Parameters.AddWithValue("gen_population_note", gen_population_note);
                command.Parameters.AddWithValue("access_father_id", access_father_id);

                command.Parameters.AddWithValue("access_mother_id", access_mother_id);
                command.Parameters.AddWithValue("fieldnumber", fieldnumber);
                command.Parameters.AddWithValue("subfieldnumber", subfieldnumber);
                command.Parameters.AddWithValue("op_father_id", op_father_id);
                command.Parameters.AddWithValue("genealogy", genealogy);
                command.Parameters.AddWithValue("user_ins", user_ins);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    headerGeneticOperationId = int.Parse(command.ExecuteScalar().ToString());
                }
                catch (Exception ex)
                {
                    return;
                }
                finally
                {
                    connection.Close();
                }

                if (headerGeneticOperationId == 0)
                    return;

                foreach (int reg in op_registry_id)
                {
                    // GENETIC OPERATION HEADER.
                    queryString = @"INSERT INTO  germschema_viola.genetic_operation_detail ( genetic_operation_id, op_registry_id,  user_ins) VALUES 
                        ( :genetic_operation_id, :op_registry_id,  :user_ins
                        ) RETURNING detail_id ";

                    command = new NpgsqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("genetic_operation_id", headerGeneticOperationId);
                    command.Parameters.AddWithValue("op_registry_id", reg);
                    command.Parameters.AddWithValue("user_ins", user_ins);
                    try
                    {
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }
                        headerGeneticOperationId = int.Parse(command.ExecuteScalar().ToString());
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        #endregion


        #region "SAMPLING"

        private void pusamplingoperation(string connectionString, int accession_id, int operation_id, int site_id, int user_ins, string notes, DateTime operation_date, int researcher_id)
        {

            int headerGeneticOperationId = 0;
            int headeroperationId = 0;
            int op_type_genica_id = int.Parse(GetOperationtypes(connectionString, "Campionamento").Rows[0]["op_type_id"].ToString());


            // OPERATION HEADER.
            string queryString = @"INSERT INTO  germschema_viola.genetic_operation_history ( accession_id, op_type_id, operation_date, researcher_id,
                         inserting_user_id, notes, pathFile) VALUES 
                        ( :accession_id, :op_type_id, :operation_date, :researcher_id,
                         inserting_user_id, notes, pathFile) RETURNING operation_id ";


            using (NpgsqlConnection connection =
              new NpgsqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);

                command.Parameters.AddWithValue("accession_id", accession_id);
                command.Parameters.AddWithValue("op_type_id", op_type_genica_id);
                command.Parameters.AddWithValue("operation_date", operation_date);
                command.Parameters.AddWithValue("researcher_id", researcher_id);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    headeroperationId = int.Parse(command.ExecuteScalar().ToString());
                }
                catch (Exception ex)
                {
                    return;
                }
                if (headeroperationId == 0)
                    return;

                // GENETIC OPERATION HEADER.
                queryString = @"INSERT INTO  germschema_viola.sampling (operation_id, site_id, date, accession_id, researcher_id, notes,  inserting_user_id) VALUES 
                        ( operation_id, site_id, date, accession_id, researcher_id, notes, inserting_date, inserting_user_id
                        ) RETURNING sampling_id ";

                command = new NpgsqlCommand(queryString, connection);
                command.Parameters.AddWithValue("operation_id", operation_id);
                command.Parameters.AddWithValue("site_id", site_id);
                command.Parameters.AddWithValue("accession_id", accession_id);
                command.Parameters.AddWithValue("researcher_id", researcher_id);
                command.Parameters.AddWithValue("notes", notes);
                command.Parameters.AddWithValue("inserting_user_id", user_ins);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    headerGeneticOperationId = int.Parse(command.ExecuteScalar().ToString());
                }
                catch (Exception ex)
                {
                    return;
                }

             
            }
        }
        #endregion



        #region "CONSERVATION"

        private void conservationoperation(string connectionString, int accession_id, int operation_id, int site_id, int user_ins, string notes, DateTime operation_date, int researcher_id)
        {

            int cionservationoperationId = 0;
            int op_type_genica_id = int.Parse(GetOperationtypes(connectionString, "Campionamento").Rows[0]["op_type_id"].ToString());


            // OPERATION HEADER.
            string queryString = @"INSERT INTO  germschema_viola.genetic_operation_history ( accession_id, op_type_id, operation_date, researcher_id,
                         inserting_user_id, notes, pathFile) VALUES 
                        ( :accession_id, :op_type_id, :operation_date, :researcher_id,
                         inserting_user_id, notes, pathFile) RETURNING operation_id ";


            using (NpgsqlConnection connection =
              new NpgsqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);

                command.Parameters.AddWithValue("accession_id", accession_id);
                command.Parameters.AddWithValue("op_type_id", op_type_genica_id);
                command.Parameters.AddWithValue("operation_date", operation_date);
                command.Parameters.AddWithValue("researcher_id", researcher_id);
                command.Parameters.AddWithValue("inserting_user_id", user_ins);
                command.Parameters.AddWithValue("notes", notes);
                
                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    cionservationoperationId = int.Parse(command.ExecuteScalar().ToString());
                }
                catch (Exception ex)
                {
                    return;
                }
                if (cionservationoperationId == 0)
                    return;

                // GENETIC OPERATION HEADER.
                queryString = @"INSERT INTO  germschema_viola.conservation (operation_id, site_id) VALUES 
                        ( operation_id, site_id) ";

                command = new NpgsqlCommand(queryString, connection);
                command.Parameters.AddWithValue("operation_id", operation_id);
                command.Parameters.AddWithValue("site_id", site_id);
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
                    return;
                }                
            }
        }
        #endregion

        public enum operations
        {
            none,
            Campionamento,
            Conservazione,
            Genica,
            Genetic_constitution_of_the_population
        }
    }
}