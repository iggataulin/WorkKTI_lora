using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace TestWeb1
{
    public partial class About : Page
    {
        DBIgor con = new DBIgor();
        MySqlCommand command;
        MySqlDataAdapter da;
        DataTable dt;
        private string mySQlTableMessege;
        private string mySQlChartTemperaturMessege1;
        private string mySQlChartHumidityMessege2;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)//кнопка для поиска данных по конкретному датчику
        {
            con.Connect();
            try
            {
            con.cn.Open();

            if (TextBox2.Text.Length == 0 && TextBox3.Text.Length == 0)
            {
                mySQlTableMessege = $"Select * from {TextBox1.Text}";
                mySQlChartTemperaturMessege1 = $"SELECT Temperature,Time_measurement FROM {TextBox1.Text}";
                mySQlChartHumidityMessege2 = $"SELECT Humidity,Time_measurement FROM {TextBox1.Text}";
            }

            if (TextBox2.Text.Length > 0 && TextBox3.Text.Length == 0)
            {
                string myDate = TextBox2.Text.ToString();
                DateTime dateValue = DateTime.Parse(myDate);
                string formatForMySql = dateValue.ToString("yyyy-MM-dd HH:mm:ss");
                mySQlTableMessege = $"Select * from {TextBox1.Text} where Time_measurement>='{formatForMySql}'";
                mySQlChartTemperaturMessege1 = $"SELECT Temperature,Time_measurement FROM {TextBox1.Text}" +
                                               $" where Time_measurement>='{formatForMySql}'";
                mySQlChartHumidityMessege2 = $"SELECT Humidity,Time_measurement FROM {TextBox1.Text} " +
                                             $"where Time_measurement>='{formatForMySql}'";
            }

            if (TextBox2.Text.Length == 0 && TextBox3.Text.Length > 0)
            {
                string myDate = TextBox3.Text.ToString();
                DateTime dateValue = DateTime.Parse(myDate);
                string formatForMySql = dateValue.ToString("yyyy-MM-dd HH:mm:ss");
                mySQlTableMessege = $"Select * from {TextBox1.Text} where Time_measurement<='{formatForMySql}'";
                mySQlChartTemperaturMessege1 = $"SELECT Temperature,Time_measurement FROM {TextBox1.Text}" +
                                               $" where Time_measurement<='{formatForMySql}'";
                mySQlChartHumidityMessege2 = $"SELECT Humidity,Time_measurement FROM {TextBox1.Text} " +
                                             $"where Time_measurement<='{formatForMySql}'";
            }

            if (TextBox2.Text.Length > 0 && TextBox3.Text.Length > 0)
            {
                string myDate = TextBox2.Text.ToString();
                DateTime dateValue = DateTime.Parse(myDate);
                string formatForMySql = dateValue.ToString("yyyy-MM-dd HH:mm:ss");
                string myDate2 = TextBox3.Text.ToString();
                DateTime dateValue2 = DateTime.Parse(myDate2);
                string formatForMySql2 = dateValue2.ToString("yyyy-MM-dd HH:mm:ss");
                mySQlTableMessege = $"Select * from {TextBox1.Text} where Time_measurement>='{formatForMySql}' AND Time_measurement<='{formatForMySql2}'";
                mySQlChartTemperaturMessege1 = $"SELECT Temperature,Time_measurement FROM {TextBox1.Text}" +
                                               $" where Time_measurement>='{formatForMySql}' AND Time_measurement<='{formatForMySql2}'";
                mySQlChartHumidityMessege2 = $"SELECT Humidity,Time_measurement FROM {TextBox1.Text} " +
                                             $"where Time_measurement>='{formatForMySql}' AND Time_measurement<='{formatForMySql2}'";
                }

            command = new MySqlCommand(mySQlTableMessege, con.cn);
            command.ExecuteNonQuery();
            dt = new DataTable();
            da = new MySqlDataAdapter(command);
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            command = new MySqlCommand(mySQlChartTemperaturMessege1, con.cn);
            command.ExecuteNonQuery();
            dt = new DataTable();
            da = new MySqlDataAdapter(command);
            da.Fill(dt);
            string[] x = new string[dt.Rows.Count];
            float[] y = new float[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i]["Time_measurement"].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i]["Temperature"]);
            }
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Series[0].ChartType = SeriesChartType.Line;

            command = new MySqlCommand(mySQlChartHumidityMessege2, con.cn);
            command.ExecuteNonQuery();
            dt = new DataTable();
            da = new MySqlDataAdapter(command);
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i]["Time_measurement"].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i]["Humidity"]);
            }
            Chart2.Series[0].Points.DataBindXY(x, y);
            Chart2.Series[0].ChartType = SeriesChartType.Line;
            con.cn.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Chart1_Load(object sender, EventArgs e)
        {

        }

        protected void Chart2_Load(object sender, EventArgs e)
        {

        }
    }
}