using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace oop_lab_exam
{
    class DBAccess
    {
        public SqlConnection Connection
        {
            get { return connection; }
        }
        private static SqlConnection connection = new SqlConnection();
        private static SqlCommand command = new SqlCommand();
        private static SqlDataAdapter adapter = new SqlDataAdapter();
        public SqlTransaction? DbTran;
        private static string strConnString = "Data Source=DESKTOP-HQGOEFH;Initial Catalog=Lap4OOP;Integrated Security=True";

        public void createConn()
        {
            try
            {// if the connection is not open open it
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        // close connection
        public void closeConn()
        {
            connection.Close();
        }

        public int executeDataAdapter(DataTable tblName, string strSelectSql)
        {/*this method allows you to update a database table by providing a DataTable and 
          * an SQL SELECT statement. It takes care of generating the 
          * necessary SQL commands and applying the changes to the database.*/
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }

                adapter.SelectCommand = new SqlCommand(strSelectSql, connection);
                adapter.SelectCommand.CommandType = CommandType.Text;
                SqlCommandBuilder DbCommandBuilder = new SqlCommandBuilder(adapter);

                adapter.InsertCommand = DbCommandBuilder.GetInsertCommand();
                adapter.UpdateCommand = DbCommandBuilder.GetUpdateCommand();
                adapter.DeleteCommand = DbCommandBuilder.GetDeleteCommand();

                return adapter.Update(tblName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void readDatathroughAdapter(string query, DataTable tblName)
        {/*, this method executes the query against the database, retrieves the results, 
          * and stores them in the provided DataTable.*/
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                adapter = new SqlDataAdapter(command);
                adapter.Fill(tblName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public SqlDataReader readDatathroughReader(string query)
        {
            //DataReader used to sequentially read data from a data source
            SqlDataReader reader;

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                reader = command.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int executeQuery(SqlCommand dbCommand)
        {//this method executes a non-query SQL command against the database and returns the number of affected rows.
            try
            {
                if (connection.State == 0)
                {
                    createConn();
                }

                dbCommand.Connection = connection;
                dbCommand.CommandType = CommandType.Text;


                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        //this method executes a SQL query, retrieves the resulting data, and returns it as a DataTable.
        {
            string connectionString = "Data Source=DESKTOP-HQGOEFH;Initial Catalog=Library;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }


        public int UpdateTable(DataTable dataTable, string tableName)
        {//this method updates a database table based on changes made to
         //a DataTable using a SqlDataAdapter and a SqlCommandBuilder.
            int rowsAffected = 0;

            try
            {
                createConn();

                // Create a new SqlDataAdapter and SqlCommandBuilder
                adapter = new SqlDataAdapter();
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

                command.Connection = connection;
                command.CommandText = "SELECT * FROM " + tableName;
                command.CommandType = CommandType.Text;

                adapter.SelectCommand = command;

                // Set the primary key column(s) for the DataTable
                DataColumn[] primaryKeys = GetPrimaryKeys(tableName);
                dataTable.PrimaryKey = primaryKeys;

                // Update the database with the changes
                rowsAffected = adapter.Update(dataTable);

                closeConn();
            }
            catch (Exception ex)
            {
                throw;
            }

            return rowsAffected;
        }

        private DataColumn[] GetPrimaryKeys(string tableName)
        {//this method retrieves the primary key column(s) for a specified table from the database by querying t
         //he schema information of the indexes. It returns an array of DataColumn objects representing the primary key columns.
            DataTable schemaTable = connection.GetSchema("Indexes");
            DataRow[] indexRows = schemaTable.Select($"table_name = '{tableName}' AND primary_key = 1");
            List<DataColumn> primaryKeys = new List<DataColumn>();

            foreach (DataRow row in indexRows)
            {
                string columnName = row["column_name"].ToString();
                DataColumn column = new DataColumn(columnName);
                primaryKeys.Add(column);
            }

            return primaryKeys.ToArray();
        }


        public int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {//, the ExecuteNonQuery method executes a non-query SQL statement
         //on the database and returns the number of rows affected.
         //It is typically used for performing data manipulation operations.
            try
            {
                createConn();

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.Parameters.AddRange(parameters);

                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public object ExecuteScalar(string query, params SqlParameter[] parameters)
        {// method executes a query that returns a single value and returns that value as an object.
         // It is commonly used when you expect a single result from a query,
         // such as retrieving a count or an aggregated value.
            using (SqlConnection connection = new SqlConnection(strConnString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }



    }
}
