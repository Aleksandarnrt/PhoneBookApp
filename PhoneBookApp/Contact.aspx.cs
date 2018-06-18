using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhoneBookApp
{
    public partial class Contact : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=JOXY-PC\SQLEXPRESS; Initial Catalog=HubRiverTest; Integrated Security=true");
        
        protected void Page_Load(object sender, EventArgs e)
        {
            /*check if it's initial load*/
            if (!IsPostBack)
            {
                btnDelete.Enabled = false;
                fillGrid();
                message.Text = "";
            }
            gvContact.UseAccessibleHeader = true;



            //adds <thead> and <tbody> table element

            gvContact.HeaderRow.TableSection = TableRowSection.TableHeader;
            gvContact.HeaderRow.CssClass = "tableHeader";

        }
        /*method for clearing form*/
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            message.Text = "";
        }

        public void Clear()
        {
            hfContactID.Value = "";
            txtFirstame.Text = txtLastname.Text = txtPhoneNumber.Text = "";
            btnAdd.Text = "Add Contact";
            btnDelete.Enabled = false;
        }



        /*method for adding new contact to database*/

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            if (txtFirstame.Text == "")
                message.Text = "Please eneter valid contact name.";
            else if(txtPhoneNumber.Text.Length<10 || txtPhoneNumber.Text.Length > 11 || txtPhoneNumber.Text.Any(char.IsLetter))
                message.Text = "Please eneter valid contact phone number.";

            else
            {
                SqlDataAdapter adp = new SqlDataAdapter("select phoneNumber from dbo.contacts where phoneNumber='" + txtPhoneNumber.Text + "'", conn);
                DataSet ds = new DataSet();
                adp.Fill(ds, "users");
                if (ds.Tables["users"].Rows.Count > 0)
                {
                    message.Text = "Contact with this phone number already exist. Please try another phone number.";
                }
                else { 
                    SqlCommand cmd = new SqlCommand("CreateOrUpdateContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ContactID", hfContactID.Value == "" ? 0 : Convert.ToInt32(hfContactID.Value));
                    cmd.Parameters.AddWithValue("@firstName", txtFirstame.Text.Trim());
                    cmd.Parameters.AddWithValue("@lastName", txtLastname.Text.Trim());
                    cmd.Parameters.AddWithValue("@phoneNumber", txtPhoneNumber.Text.Trim());
                    cmd.ExecuteNonQuery();
                    Clear();

                    conn.Close();
                    fillGrid();
                    message.Text = "";
                }
            }
        }

        /*filling table with contacts from database*/

        void fillGrid()
        {
            if(conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataAdapter adp = new SqlDataAdapter("ContactViewAll", conn);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adp.Fill(dt);
            conn.Close();
            gvContact.DataSource = dt;
            gvContact.DataBind();

        }

        /*method for populating form with existng contact for eventual contact update*/

        protected void lnkView_OnClick(object sender, EventArgs e)
        {
            int contactID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataAdapter adp = new SqlDataAdapter("ViewContactsById", conn);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@contactID", contactID);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            conn.Close();
            hfContactID.Value = contactID.ToString();
            txtFirstame.Text = dt.Rows[0]["ContactFirstname"].ToString();
            txtLastname.Text = dt.Rows[0]["ContactLastName"].ToString();
            txtPhoneNumber.Text = dt.Rows[0]["phoneNumber"].ToString();
            btnAdd.Text = "Update";
            btnDelete.Enabled = true;
            message.Text = "";
        }

        /*method for deleting contact*/

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("DeleteById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ContactID", Convert.ToInt32(hfContactID.Value));
            cmd.ExecuteNonQuery();
            conn.Close();
            Clear();
            fillGrid();
            
        }
    }
}