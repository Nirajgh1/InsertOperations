using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CsharpToDB
{
    internal class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect() {
            Initialize();
        }
        //Initialize values
        private void Initialize() {
            server = "localhost";
            database = "connectcsharptomysql";
            uid = "root";
            password = "nirajlapi";

            string connectionstring;

            connectionstring = "SERVER=" + server + ";" + "DATABASE=" +
        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionstring);
        }

        private bool Openconnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.Write("Cannot connect to server. Contact administrator");
                        break;
                    case 1045:
                        Console.Write("Invalid username/password, please try again");
                        break;

                }
                return false;
            }
        }
        private bool Closeconnection()
        {
            try
            {
                connection.Close();
                return true;
            } catch (MySqlException ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
   
        public void InsertShop()
        {
            string query = "INSERT INTO shops(ShopID,Fname,TaxID,OwnerID)VALUES('1','textiles','3','1'),('2','rawMaterials','3','2'),('3','goods','3','3'),('4','fruits','3','4')";
            //open connection
            if (this.Openconnection() == true)
            {
                //create command and assign query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //execute command
                cmd.ExecuteNonQuery();
                //close connection
                this.Closeconnection();
            }
        }
        public void InsertOwner()
        {
            string query = "INSERT INTO Owners(OwnerID,Fname,email)VALUES('1','Karan','karan@gmail.com'),('2','jarin','jarin@gmail.com'),('3','shankar','shankar@gmail.com'),('4','martin','martin@gmail.com')";
            //open connection
            if (this.Openconnection() == true)
            {
                //create command and assign query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //execute command
                cmd.ExecuteNonQuery();
                //close connection
                this.Closeconnection();
            }
        }
        public void InsertShopLocation()
        {
            string query = "INSERT INTO shoplocations(LocID,shopID,Sname,address,lat,lng)VALUES('1','1','textiles','durgamCheruvu','60','43'),('2','2','rawMaterials','hitech','50','33'),('3','3','goods','balaji','70','23'),('4','4','fruits','sitafalman','30','29')";
            //open connection
            if (this.Openconnection() == true)
            {
                //create command and assign query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //execute command
                cmd.ExecuteNonQuery();
                //close connection
                this.Closeconnection();
            }
        }
        public void Update()
        {
            string query = "UPDATE student SET name='Jarin',age='24' WHERE name='Suraj'";
            if (this.Openconnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                this.Closeconnection();
            }
        }
        public void Delete()
        {
            string query = "DELETE FROM student WHERE name='Suraj'";

            if (this.Openconnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.Closeconnection();
            }

        }
        public void Read() {


            string query = "SELECT *,(6371*acos(cos(radians(37))*cos(radians(lat))*cos(radians(lng)-radians(48))+sin(radians(37))*sin(radians(lat))) ) AS distance FROM shoplocations order by distance asc limit 1;";
            if (this.Openconnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query,connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["id"]);
                    Console.WriteLine(reader["shopID"]);
                    Console.WriteLine(reader["name"]);
                    Console.WriteLine(reader["address"]);
                    Console.WriteLine(reader["distance"]);
                }
                reader.Close();
                this.Closeconnection();

            }
        }
    }
}

