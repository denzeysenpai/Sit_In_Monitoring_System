using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sit_In_Monitoring
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=LAB5-PC10\\ACTSTUDENT;Initial Catalog=monitoring;Integrated Security=True");
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
            Design.RoundCorner(pass, 12);
            Design.RoundCorner(mrg1, 12);
            Design.RoundCorner(mrg2, 12);
            Design.RoundCorner(mrg3, 12);
            Design.RoundCorner(mrg4, 12);
            Design.RoundCorner(mrg5, 12);
            Design.RoundCorner(mrg6, 12);
            Design.RoundCorner(this, 25);
            Design.RoundCorner(pnlStudentInfo, 15);
            Design.RoundCorner(DataGrid, 15);
            Design.RoundCorner(recordsView, 15);
            Design.RoundCorner(tm1, 15);
            Design.RoundCorner(tm2, 16);
            Design.RoundCorner(l1, 11);
            Design.RoundCorner(l2, 11);
            Design.RoundCorner(l3, 11);
            Design.RoundCorner(l4, 11);
            Design.RoundCorner(l5, 11);
            Design.RoundCorner(l6, 11);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Clicked = Color.FromArgb(4, 146, 191);
            notClicked = Color.FromArgb(210, 242, 250);

            mrg1.BackColor = notClicked;
            mrg2.BackColor = notClicked;

            pnlConfirmExit.Hide();
            exitApp = false;
            Update_Data();
            pnlConfirmExit.Location = new Point(446, 204);
            pnlRecords.Location = new Point(0, 0);
            pnlRecords.Hide();
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
                mrg1.BackColor = Clicked;
                placeholder1.Hide();
            }
            else
            {
                mrg1.BackColor = notClicked;
                placeholder1.Show();
            }

            // Border highlight

            if (txtStudentName.Focused)
            {
                mrg2.BackColor = Clicked;
                placeholder2.Hide();
            }
            else
            {
                mrg2.BackColor = notClicked;
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

            // Last name

            if (txtStudentLastName.Focused)
            {
                mrg3.BackColor = Clicked;
                placeholder4.Hide();
            }
            else
            {
                mrg3.BackColor = notClicked;
                placeholder4.Show();
            }

            // Section

            if (txtSection.Focused)
            {
                mrg4.BackColor = Clicked;
                placeholder5.Hide();
            }
            else
            {
                mrg4.BackColor = notClicked;
                placeholder5.Show();
            }

            // Priority num

            if (txtPriorityNum.Focused)
            {
                mrg5.BackColor = Clicked;
                placeholder6.Hide();
            }
            else
            {
                mrg5.BackColor = notClicked;
                placeholder6.Show();
            }

            // Search

            if (txtSearchId.Focused)
            {
                mrg6.BackColor = Clicked;
                placeholder7.Hide();
            }
            else
            {
                mrg6.BackColor = notClicked;
                placeholder7.Show();
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
            dateToday.Select();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 && e.Control)
            {
                pnlRecords.Show();
            }
        }
        public void addStudent()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Student VALUES(@studentId,@firstName,@lastName,@section)", conn);
            cmd.Parameters.AddWithValue("@studentId", txtStudentID.Text);
            cmd.Parameters.AddWithValue("@firstName", txtStudentName.Text);
            cmd.Parameters.AddWithValue("@lastName", txtStudentLastName.Text);
            cmd.Parameters.AddWithValue("@section", txtSection.Text);

        }
        public void Update_Data() // display the data register
        {
            //SitInMonitoringEntities sit = new SitInMonitoringEntities();
            //var query = from cs in sit.currentSessions
            //            join s in sit.Students on cs.personid equals s.personid
            //            where s.studentid = 
            //EZ - 6/7/2023
                        


            //SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM sit_in_table", conn);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //DataGrid.Rows.Clear();

            //foreach (DataRow dr in dt.Rows)
            //{
            //    int n = DataGrid.Rows.Add();
            //    int m = recordsView.Rows.Add();
            //    for (int i = 0; i < 5; i++)
            //    {
            //        DataGrid.Rows[n].Cells[i].Value = dr[i].ToString();
            //        recordsView.Rows[m].Cells[i].Value = dr[i].ToString();
            //    }
            //}
        }


        private void btnStart_Click(object sender, EventArgs e) //Update data during log in
        {
            DateTime val = DateTime.Now;
            string time1 = val.ToString("hh:mm:ss tt");
            string date = $"{val.Date: MM/dd/yyyy}";



            addStudent();
            //try
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand("INSERT INTO sit_in_table VALUES(@date_time, @student_id, @student_full_name, @time_in, @time_out)", conn);
            //    cmd.Parameters.AddWithValue("@date_time", date);
            //    cmd.Parameters.AddWithValue("@student_id", txtStudentID.Text.ToString());
            //    cmd.Parameters.AddWithValue("@student_full_name", txtStudentName.Text.ToString());
            //    cmd.Parameters.AddWithValue("@time_in", time1);
            //    cmd.Parameters.AddWithValue("@time_out", string.Empty);
            //    cmd.ExecuteNonQuery();
            //    MessageBox.Show("Successfully Logged In!");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}


            //conn.Close();
            //Update_Data();

            //txtStudentID.Text = string.Empty;
            //txtStudentName.Text = string.Empty;
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

                    SqlCommand cmd2 = new SqlCommand("INSERT INTO records_table VALUES(@date_time, @student_id, @student_full_name, @time_in, @time_out)", conn);
                    cmd2.Parameters.AddWithValue("@date_time", time.ToString("MM/dd/yyyy"));
                    cmd2.Parameters.AddWithValue("@student_id", txtStudentID.Text.ToString());
                    cmd2.Parameters.AddWithValue("@student_full_name", txtStudentName.Text.ToString());
                    cmd2.Parameters.AddWithValue("@time_in", time1);
                    cmd2.Parameters.AddWithValue("@time_out", time1);
                    cmd2.ExecuteNonQuery();

                    DataGrid.Rows.RemoveAt(e.RowIndex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                conn.Close();
                Update_Data();
            }
        }

        #region Behavior UI
        private void idNumberHasInput(object sender, EventArgs e) => CheckForInput(txtStudentID, placeholder1);
        private void fullNameHasInput(object sender, EventArgs e) => CheckForInput(txtStudentName, placeholder2);
        private void PassHasInput(object sender, EventArgs e) => CheckForInput(txtPass, placeholder3);
        private void lastnamehasinput(object sender, EventArgs e) => CheckForInput(txtStudentLastName, placeholder4);
        private void sectioninput(object sender, EventArgs e) => CheckForInput(txtSection, placeholder5);
        private void prioinput(object sender, EventArgs e) => CheckForInput(txtPriorityNum, placeholder6);
        private void searchedchanged(object sender, EventArgs e) => CheckForInput(txtSearchId, placeholder7);

        // Text Focus
        private void idClick(object sender, EventArgs e) => txtStudentID.Focus();
        private void fullNameClick(object sender, EventArgs e) => txtStudentName.Focus();
        private void userClick(object sender, EventArgs e) => txtStudentID.Focus();
        private void userClick2(object sender, EventArgs e) => txtStudentName.Focus();
        private void idClick2(object sender, EventArgs e) => txtPass.Focus();
        private void passClick(object sender, EventArgs e) => txtPass.Focus();
        private void lnclick(object sender, EventArgs e) => txtStudentLastName.Focus();
        private void seclick(object sender, EventArgs e) => txtSection.Focus();
        private void prnumclick(object sender, EventArgs e) => txtPriorityNum.Focus();
        private void qw1(object sender, EventArgs e) => txtStudentLastName.Focus();
        private void qw2(object sender, EventArgs e) => txtSection.Focus();
        private void qw3(object sender, EventArgs e) => txtPriorityNum.Focus();
        private void placeholder7click(object sender, EventArgs e) => txtSearchId.Focus();

        #endregion





        // RECORDS PAGE FUNCTIONS
        private void hideRecords_Click(object sender, EventArgs e)
        {
            pnlRecords.Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchInRecords_Click(object sender, EventArgs e)
        {

        }
    }
    class SeiyaMarx
    {
        [DllImport("Gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        public void RoundCorner(Control ctr, int val) => ctr.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, ctr.Width, ctr.Height, val, val));
    }



}
