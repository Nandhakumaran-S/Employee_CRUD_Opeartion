using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        void BindGrid()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Select * from emp_table";
            //Since SqlDataAdapater is a Disconnected Data Access there is no need for SqlConnection to be open or close
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            //Fill Method will automatically open the connection, exceute the query, then stores in the data in DataTable or DataSet and closes the Connection
            sda.Fill(data);
            GV1.DataSource = data;
            GV1.DataBind();
        }

        void InitialPostion()
        {
            IDText.Text = NameText.Text = SalaryText.Text = CityText.Text = string.Empty;
            DDLGender.SelectedItem.Text = "Select Gender";
        }

        protected void GV1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GV1.SelectedRow;
            Label lblID = (Label)row.FindControl("IDLabel");
            Label lblName = (Label)row.FindControl("NameLabel");
            Label lblGender = (Label)row.FindControl("GenderLabel");
            Label lblSalary = (Label)row.FindControl("SalaryLabel");
            Label lblCity = (Label)row.FindControl("CityLabel");
            IDText.Text = lblID.Text;
            NameText.Text = lblName.Text;
            DDLGender.SelectedItem.Text = lblGender.Text;
            SalaryText.Text = lblSalary.Text;
            CityText.Text = lblCity.Text;
           
        }
        // Since ID is AutoIncremented in DataBase so for Insert and Update it will use the DataBase Identity
        //To Update or Delete No need to enter the values just need to select the particular record to be performed for opeartion
        protected void InsertBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "spCrud";
            string operation = "Insert";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@action", operation);
            cmd.Parameters.AddWithValue("@id", IDText.Text);
            cmd.Parameters.AddWithValue("@name", NameText.Text);
            cmd.Parameters.AddWithValue("@gender", DDLGender.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@salary", SalaryText.Text);
            cmd.Parameters.AddWithValue("@city", CityText.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            int RowAffected = cmd.ExecuteNonQuery();
            if (RowAffected > 0) {
                Response.Write("<script>alert('Inserted Succesfully')</script>");
                BindGrid();
                InitialPostion();
            }
            else
            {
                Response.Write("<script>alert('Not Inserted')</script>");
            }
            con.Close();
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(cs);
            string query = "spCrud";
            string operation = "Update";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@action", operation);
            cmd.Parameters.AddWithValue("@id", IDText.Text);
            cmd.Parameters.AddWithValue("@name", NameText.Text);
            cmd.Parameters.AddWithValue("@gender", DDLGender.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@salary", SalaryText.Text);
            cmd.Parameters.AddWithValue("@city", CityText.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            int RowAffected = cmd.ExecuteNonQuery();
            if (RowAffected > 0)
            {
                Response.Write("<script>alert('Updated Succesfully')</script>");
                BindGrid();
                InitialPostion();
            }
            else
            {
                Response.Write("<script>alert('Not Updated')</script>");
            }
            con.Close();
        }

        
        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "spCrud";
            string operation = "delete";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@action", operation);
            cmd.Parameters.AddWithValue("@id", IDText.Text);
            cmd.Parameters.AddWithValue("@name", NameText.Text);
            cmd.Parameters.AddWithValue("@gender", DDLGender.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@salary", SalaryText.Text);
            cmd.Parameters.AddWithValue("@city", CityText.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            int RowAffected = cmd.ExecuteNonQuery();
            if (RowAffected > 0)
            {
                Response.Write("<script>alert('Deleted Succesfully')</script>");
                BindGrid();
                InitialPostion();
            }
            else
            {
                Response.Write("<script>alert('Not Deleted')</script>");
            }
            con.Close();
        }
    }
}