using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Renci.SshNet;

namespace TestWeb1
{
    public partial class _Default : Page
    {
        DBIgor con = new DBIgor();
        MySqlCommand command;
        MySqlDataAdapter da;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            con.Connect();
            try
            {
                con.cn.Open();
                command = new MySqlCommand("Select * from information_sensors", con.cn);
                command.ExecuteNonQuery();
                dt = new DataTable();
                da = new MySqlDataAdapter(command);
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                con.cn.Close();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e) //кнопка добавления нового устройства
        {
            con.Connect();
            try
            {
                con.cn.Open();
                command = new MySqlCommand($"INSERT INTO information_sensors(MAC_adres,Location)" +
                                           $" VALUES ('{TextBox1.Text}', '{TextBox2.Text}')", con.cn);
                command.ExecuteNonQuery();
                command = new MySqlCommand($"CREATE TABLE `{TextBox1.Text}` (`ID_measurement` INT NOT NULL AUTO_INCREMENT , `MAC_adres` VARCHAR(12) NOT NULL ," +
                                           $" `Temperature` FLOAT NOT NULL , `Humidity` FLOAT NOT NULL , `Battaty_charge` INT NOT NULL ," +
                                           $" `Time_measurement` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ," +
                                           $" PRIMARY KEY (`ID_measurement`)) ENGINE = InnoDB CHARSET=utf8 COLLATE utf8_general_ci;", con.cn);
                command.ExecuteNonQuery();
                command = new MySqlCommand("Select * from information_sensors", con.cn);
                command.ExecuteNonQuery();
                dt = new DataTable();
                da = new MySqlDataAdapter(command);
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                con.cn.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)//кнопка удаления устройства
        {
            con.Connect();
            try
            {
                con.cn.Open();
                command = new MySqlCommand($"DELETE FROM information_sensors WHERE MAC_adres='{TextBox3.Text}'", con.cn);
                command.ExecuteNonQuery();
                command = new MySqlCommand($"DROP TABLE `{TextBox3.Text}`", con.cn);
                command.ExecuteNonQuery();
                command = new MySqlCommand("Select * from information_sensors", con.cn);
                command.ExecuteNonQuery();
                dt = new DataTable();
                da = new MySqlDataAdapter(command);
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                con.cn.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void TextBox3_TextChanged1(object sender, EventArgs e)
        {

        }
    }
}