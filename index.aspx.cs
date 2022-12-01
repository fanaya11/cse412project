using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;
using System.Text;

namespace Spotify_App
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Label1.Text = "PostgreSQL Connection Success...";
                //fish records from album table and display in gridview
               /* NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres; User Id=postgres;Password=Ishreya;");
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "SELECT * FROM \"SpotifyProjectupdated\".\"Album\"";
                NpgsqlDataAdapter nda = new NpgsqlDataAdapter(comm);
                DataTable dt = new DataTable();
                nda.Fill(dt);
                comm.Dispose();
                conn.Close(); 
                GridView1.DataSource = dt;
                GridView1.DataBind(); */
            }
            catch (Exception)
            {
                Label1.Text = "PostgreSQL Connection Failed...";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "Your selected item is " + RadioButtonList1.SelectedItem.ToString();
            string srchstr = txtsearch.Text;
            string query; 
            string paramName;
            DataTable dt;
            if (!string.IsNullOrEmpty(srchstr))
            {
                srchstr = srchstr.Trim();
            }
            switch(RadioButtonList1.SelectedIndex)
            {
                case 0: //select by artist name 
                     query = "SELECT \"SpotifyProjectupdated\".\"Artist\".\"artistName\", \"SpotifyProjectupdated\".\"Song\".\"songName\", \"SpotifyProjectupdated\".\"Song\".\"numPlays\" FROM \"SpotifyProjectupdated\".\"Artist\", \"SpotifyProjectupdated\".\"Song\" WHERE \"SpotifyProjectupdated\".\"Song\".\"artistId\" = \"SpotifyProjectupdated\".\"Artist\".\"artistId\" AND \"SpotifyProjectupdated\".\"Artist\".\"artistName\" = @artname;";
                     paramName = "artname";
                    dt = SelectData(query, paramName, srchstr);
                    break;
                case 1: //select by album 
                    query = "SELECT \"SpotifyProjectupdated\".\"Album\".\"albumName\", \"SpotifyProjectupdated\".\"Song\".\"songName\", \"SpotifyProjectupdated\".\"Song\".\"numPlays\" FROM \"SpotifyProjectupdated\".\"Album\", \"SpotifyProjectupdated\".\"Song\" WHERE \"SpotifyProjectupdated\".\"Song\".\"albumId\" = \"SpotifyProjectupdated\".\"Album\".\"albumId\" AND \"SpotifyProjectupdated\".\"Album\".\"albumName\" = @albname;";
                    paramName = "albname";
                    dt = SelectData(query, paramName, srchstr);
                    break;
                case 2: //select
                    query = "SELECT \"SpotifyProjectupdated\".\"Artist\".\"artistName\", \"SpotifyProjectupdated\".\"Album\".\"albumName\", \"SpotifyProjectupdated\".\"Song\".\"songName\", \"SpotifyProjectupdated\".\"Song\".\"numPlays\" FROM \"SpotifyProjectupdated\".\"Album\", \"SpotifyProjectupdated\".\"Artist\", \"SpotifyProjectupdated\".\"Song\" WHERE \"SpotifyProjectupdated\".\"Song\".\"artistId\" = \"SpotifyProjectupdated\".\"Artist\".\"artistId\" AND \"SpotifyProjectupdated\".\"Song\".\"albumId\" = \"SpotifyProjectupdated\".\"Album\".\"albumId\" AND \"SpotifyProjectupdated\".\"Song\".\"songName\" = @sngname;";
                    paramName = "sngname";
                    dt = SelectData(query, paramName, srchstr);
                    break;
                case 3:
                    query = "SELECT * FROM \"SpotifyProjectupdated\".\"Artist\"";
                    dt = SelectData(query);
                    break;
                case 4:
                    query = "SELECT * FROM \"SpotifyProjectupdated\".\"Album\"";
                    dt = SelectData(query);
                    break;
                case 5:
                    query = "SELECT * FROM \"SpotifyProjectupdated\".\"Song\"";
                    dt = SelectData(query);
                    break;
                default:
                    query = "SELECT \"SpotifyProjectupdated\".\"Song\".\"songName\", \"SpotifyProjectupdated\".\"Artist\".\"artistName\" FROM \"SpotifyProjectupdated\".\"Artist\", \"SpotifyProjectupdated\".\"Song\" WHERE \"SpotifyProjectupdated\".\"Song\".\"artistId\" = \"SpotifyProjectupdated\".\"Artist\".\"artistId\" AND \"SpotifyProjectupdated\".\"Artist\".\"artistName\" = @artname;";
                    paramName = "artname";
                    dt = SelectData(query, paramName, srchstr);
                    break;
            }
            
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        /// <summary>
        /// Get data from a simple query. No params needed.
        /// </summary>
        /// <param name="query">Query to execute. Example: select * from sales</param>
        /// <returns></returns>
        private DataTable SelectData(string query)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres; User Id=postgres;Password=Ishreya;");
            conn.Open();
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Prepare();

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);

                DataSet _ds = new DataSet();
                DataTable _dt = new DataTable();

                da.Fill(_ds);

                try
                {
                    _dt = _ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Label1.Text = "PostgreSQL Connection Failed...";
                }

                conn.Close();
                return _dt;
            }

        }

        /// <summary>
		/// Get data a DataTable from a query with params.
		/// </summary>
		/// <param name="query">Query to execute. Example: select * from sales where product = @prodId</param>
		/// <param name="paramName">Param name. Example: "prodId"</param>
		/// <param name="paramValue">Param value. Example: (int)15</param>
		/// <returns></returns>
		private DataTable SelectData(string query, string paramName, object paramValue)
        {

            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres; User Id=postgres;Password=Ishreya;");
            conn.Open();
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue(paramName, paramValue);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);

                DataSet _ds = new DataSet();
                DataTable _dt = new DataTable();

                da.Fill(_ds);

                try
                {
                    _dt = _ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Label1.Text = "PostgreSQL Connection Failed...";
                }

                conn.Close();
                return _dt;
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblsearch.Visible = true;
            txtsearch.Visible = true;
            if (RadioButtonList1.SelectedIndex == 0)
            {
                lblsearch.Text = "Enter artist name: ";

            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                lblsearch.Text = "Enter album name: ";

            }
            else if (RadioButtonList1.SelectedIndex == 2)
            {
                lblsearch.Text = "Enter song name: ";

            }
            else if ((RadioButtonList1.SelectedIndex == 3) || (RadioButtonList1.SelectedIndex == 4) || (RadioButtonList1.SelectedIndex == 5))
            {
                lblsearch.Visible = false;
                txtsearch.Visible = false;
            }
        }
    }
}