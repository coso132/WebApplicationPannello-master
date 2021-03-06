using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
 
namespace WebApplicationPannello.Models
{
    public class PannelloFV
    {
        public PannelloFV(string Marca, string Modello, double PeakPower)
        {
            SqliteConnection myConnection = new SqliteConnection("Data Source=fotovoltaico.db");
            //command: manda in esecuzione una query sql
            string sqlInsert = @"INSERT INTO tblPannelli(PeakPower,Marca,Modello) VALUES(@par1,@par2,@par3);";

            //command: manda in esecuzione una query sql
            SqliteCommand myCommand = new SqliteCommand(sqlInsert);
            SqliteParameter myPar1 = new SqliteParameter("@par1", PeakPower);
            SqliteParameter myPar2 = new SqliteParameter("@par2", Marca);
            SqliteParameter myPar3 = new SqliteParameter("@par3", Modello);
            myCommand.Parameters.Add(myPar1);
            myCommand.Parameters.Add(myPar2);
            myCommand.Parameters.Add(myPar3);

            myCommand.Connection = myConnection;
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
        
        public PannelloFV() { }


        public int IdPannello { get; set; }
        [Range (1, 500, ErrorMessage = "Il valore della potenza non è valido.")]
        public double PeakPower { get; set; }
        [Required (ErrorMessage = "La marca deve essere inserita.")]
        public string Marca { get; set; }
        [Required (ErrorMessage = "Il modello deve essere inserito.")]
        public string Modello { get; set; }
        private static List<PannelloFV> listaPannelli;
        public static List<PannelloFV> ListaPannelli {
            get 
            {
                listaPannelli = new List<PannelloFV>();
                //creo gli oggetti che mi servono per manipolare il database: 
                //connection: collega il db a c#
                SqliteConnection myConnection = new SqliteConnection("Data Source=fotovoltaico.db");
                //command: manda in esecuzione una query sql
                SqliteCommand myCommand = new SqliteCommand("SELECT * FROM tblPannelli");
                //ospita la tabella che risulta dall'esecuzione del command
                SqliteDataReader myDatareader;

                myCommand.Connection = myConnection;
                myConnection.Open();
                myDatareader = myCommand.ExecuteReader();
                while (myDatareader.Read())
                {
                    PannelloFV mioPannello = new PannelloFV();
                    mioPannello.IdPannello = Convert.ToInt32(myDatareader["IdPannello"]);
                    mioPannello.PeakPower = (double)myDatareader["PeakPower"];
                    mioPannello.Marca = (string)myDatareader["Marca"];
                    mioPannello.Modello = (string)myDatareader["Modello"];
                    listaPannelli.Add(mioPannello);
                }
                myConnection.Close();
                return listaPannelli;
            } 
             
        }
    }
}
