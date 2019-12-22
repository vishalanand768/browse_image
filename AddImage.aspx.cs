using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace browse_image
{
    public partial class AddImage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["xyz"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        public void BindData()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_emp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            grd.DataSource = dt;
            grd.DataBind();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string FN = "";
            string Ext = "";
            if (btnsave.Text == "Save")
            {
                FN = DateTime.Now.Ticks.ToString() + Path.GetFileName(fui.PostedFile.FileName);
                Ext = Path.GetExtension(fui.PostedFile.FileName);
                if (Ext == ".jpeg" || Ext == ".jpg" || Ext == ".png")
                {
                    fui.SaveAs(Server.MapPath("Pics" + "\\" + FN));
                    con.Open();
                    SqlCommand cmd = new SqlCommand("usp_emp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", "insert");
                    cmd.Parameters.AddWithValue("@name", txtname.Text);
                    cmd.Parameters.AddWithValue("@img", FN);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    BindData();
                    lblmsg.Text = "Record saved!!!";
                }
                else
                {
                    lblmsg.Text = "Please upload only .jpeg,.jpg,.png files!!! ";
                }
            }
            else
            {
                FN = Path.GetFileName(fui.PostedFile.FileName);
               
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_emp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "update");
                cmd.Parameters.AddWithValue("@id", ViewState["PP"]);
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                if (FN != "")
                {
                    FN = DateTime.Now.Ticks.ToString() + FN;
                    cmd.Parameters.AddWithValue("@img", FN);
                    fui.SaveAs(Server.MapPath("Pics" + "\\" + FN));
                    File.Delete(Server.MapPath("Pics" + "\\" + ViewState["KK"]));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@img", ViewState["KK"]);
                }
                cmd.ExecuteNonQuery();
                con.Close();
                BindData();
            }
        }

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "A")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_emp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "delete");
                cmd.Parameters.AddWithValue("@id", e.CommandArgument);
                cmd.ExecuteNonQuery();
                con.Close();
                BindData();
            }
            else if (e.CommandName == "B")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_emp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "edit");
                cmd.Parameters.AddWithValue("@id", e.CommandArgument);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                txtname.Text = dt.Rows[0][1].ToString();
                ViewState["KK"] = dt.Rows[0][2].ToString();
                btnsave.Text = "Update";
                ViewState["PP"] = e.CommandArgument;
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_emp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@a", Convert.ToDateTime(txtStartDate.Text));
            cmd.Parameters.AddWithValue("@b", Convert.ToDateTime(txtEndDate.Text));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            grd.DataSource = dt;
            grd.DataBind();
        }
    }
}