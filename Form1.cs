﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Sit_In_Monitoring
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=LAB5-PC09\\ACTSTUDENT;Initial Catalog=monitoring;Integrated Security=True");
        readonly SeiyaMarx Design = new SeiyaMarx();
        Color notClicked;
        Color Clicked;
        bool exitApp;


        public Form1()
        {
            InitializeComponent();

            // Add design
            Design.RoundCorner(pnlConfirmExit, 18);
            Design.RoundCorner(pnlLoginFrame, 16);
            Design.RoundCorner(pnlStudsRec, 16);
            Design.RoundCorner(borderpass, 10);
            Design.RoundCorner(pnlDesign, 14);
            Design.RoundCorner(pass, 10);
            Design.RoundCorner(mrgn, 10);
            Design.RoundCorner(mrgl, 10);
            Design.RoundCorner(this, 25);
            Design.RoundCorner(tm1, 15);
            Design.RoundCorner(tm2, 16);
            Design.RoundCorner(l1, 9);
            Design.RoundCorner(l2, 9);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Clicked = Color.FromArgb(4, 146, 191);
            notClicked = Color.FromArgb(210, 242, 250);

            mrgl.BackColor = notClicked;
            mrgn.BackColor = notClicked;
            pnlConfirmExit.Hide();
            exitApp = false;
            Update_Data();
        }



        /// <summary>
        /// HEHE ORASAN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ORASAN(object sender, EventArgs e)
        {
            DateTime timedate = DateTime.Now;
            String time_of_day = timedate.ToString("hh:mm:ss tt");
            lblTime.Text = time_of_day;
        }



        /// <summary>
        /// Checks if the textbox is focused in the form
        /// </summary>
        private void TextBoxFocus(object sender, EventArgs e)
        {
            // Border highlight

            if (txtStudentID.Focused)
            {
                mrgl.BackColor = Clicked;
                placeholder1.Hide();
            }
            else
            {
                mrgl.BackColor = notClicked;
                placeholder1.Show();
            }

            // Border highlight

            if (txtStudentName.Focused)
            {
                mrgn.BackColor = Clicked;
                placeholder2.Hide();
            }
            else
            {
                mrgn.BackColor = notClicked;
                placeholder2.Show();
            }

            // Border highlight

            if (txtPass.Focused)
            {
                borderpass.BackColor = Clicked;
                placeholder3.Hide();
            }
            else
            {
                borderpass.BackColor = notClicked;
                placeholder3.Show();
            }

        }


        /// <summary>
        /// Checks for input in the given textboxes
        /// </summary>
        /// <param name="ctr"></param>
        /// <param name="placeholder"></param>
        void CheckForInput(Control ctr, Control placeholder)
        {
            if (ctr.Text == string.Empty || ctr.Text == null) { placeholder.BringToFront(); } else { placeholder.SendToBack(); }
        }
        private void FormWillBeClosed(object sender, FormClosingEventArgs e)
        {
            pnlConfirmExit.Show();
            if (exitApp == false) { e.Cancel = true; }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            pnlConfirmExit.Show();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // Check if user input pass is correct

            if (txtPass.Text == "hehe")
            {
                exitApp = true;
            }
            else
            {
                exitApp = false;
                MessageBox.Show("Please contact an assistant!", "Password Incorrect!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ENVI_EXIT(object sender, EventArgs e)
        {
            if (exitApp)
            {
                this.Close();
            }
        }

        private void btnCancelIn_Click(object sender, EventArgs e)
        {
            exitApp = false;
            pnlConfirmExit.Hide();
            txtPass.Text = string.Empty;
        }

        private void CalendarClick(object sender, EventArgs e)
        {
            dateToday.Checked = true;
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P && Control.ModifierKeys == Keys.Control)
            {
                MessageBox.Show("Input old password", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void Update_Data() // display the data register
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM sit_in_table", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DataGrid.Rows.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                int n = DataGrid.Rows.Add();
                for (int i = 0; i < 5; i++)
                {
                    DataGrid.Rows[n].Cells[i].Value = dr[i].ToString();

                    if (DataGrid.Rows[n].Cells[4].Value == null) {/**/}
                    else
                    {
                        DataGrid.Invalidate();
                    }

                }
            }


        }


        private void btnStart_Click(object sender, EventArgs e) //Update data during log in
        {
            DateTime val = DateTime.Now;
            string time1 = val.ToString("hh:mm:ss tt");
            string date = $"{val.Date: MM/dd/yyyy}";


            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO sit_in_table VALUES(@date_time, @student_id, @student_full_name, @time_in, @time_out)", conn);
                cmd.Parameters.AddWithValue("@date_time", date);
                cmd.Parameters.AddWithValue("@student_id", txtStudentID.Text.ToString());
                cmd.Parameters.AddWithValue("@student_full_name", txtStudentName.Text.ToString());
                cmd.Parameters.AddWithValue("@time_in", time1);
                cmd.Parameters.AddWithValue("@time_out", string.Empty);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Logged In!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            conn.Close();
            Update_Data();

            txtStudentID.Text = string.Empty;
            txtStudentName.Text = string.Empty;
        }
        private void DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) // Update data during log out
        {
            DateTime time = DateTime.Now;
            string time1 = time.ToString("hh:mm:ss tt");


            if (e.RowIndex >= 0 && e.ColumnIndex == DataGrid.Columns["LOG_OUT"].Index)
            {
                DataGridViewRow row = this.DataGrid.Rows[e.RowIndex];
                string studentId = row.Cells["STUDENT_ID"].Value.ToString();


                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE sit_in_table SET time_out = @time_out WHERE student_id = @student_id", conn);
                    cmd.Parameters.AddWithValue("@time_out", time1);
                    cmd.Parameters.AddWithValue("@student_id", studentId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Logged Out Successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                conn.Close();
                Update_Data();
            }
        }
        private void idNumberHasInput(object sender, EventArgs e) => CheckForInput(txtStudentID, placeholder1);
        private void fullNameHasInput(object sender, EventArgs e) => CheckForInput(txtStudentName, placeholder2);
        private void PassHasInput(object sender, EventArgs e) => CheckForInput(txtPass, placeholder3);
        private void idClick(object sender, EventArgs e) => txtStudentID.Focus();
        private void fullNameClick(object sender, EventArgs e) => txtStudentName.Focus();
        private void userClick(object sender, EventArgs e) => txtStudentID.Focus();
        private void userClick2(object sender, EventArgs e) => txtStudentName.Focus();
        private void idClick2(object sender, EventArgs e) => txtPass.Focus();
        private void passClick(object sender, EventArgs e) => txtPass.Focus();
    }
    class SeiyaMarx
    {
        [DllImport("Gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        public void RoundCorner(Control ctr, int val) => ctr.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, ctr.Width, ctr.Height, val, val));
    }


    
}
