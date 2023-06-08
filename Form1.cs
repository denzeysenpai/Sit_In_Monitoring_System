using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sit_In_Monitoring
{
    public partial class Form1 : Form
    {
        readonly SqlConnection conn = new SqlConnection("Data Source=LAB5-PC10\\ACTSTUDENT;Initial Catalog=SitInMonitoringEZ;Integrated Security=True");
        readonly SeiyaMarx Design = new SeiyaMarx();

        Color buttonColors;
        Color notClicked;
        Color Clicked;

        bool exitApp;
        string password;
        int attemptsOfLogin;

        #region ALL OF FUNCTIONS

        public void AddStudent()
        {
            DateTime val = DateTime.Now;
            string time1 = val.ToString("hh:mm:ss tt");
            string date = $"{val.Date: MM/dd/yyyy}";

            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Students VALUES(@studentId,@firstName, @middleInitial,@lastName,@section, @remainingTime);", conn);
            cmd.Parameters.AddWithValue("@studentId", txtStudentID.Text);
            cmd.Parameters.AddWithValue("@firstName", txtStudentName.Text);
            cmd.Parameters.AddWithValue("@middleInitial", txtMiddleInitial.Text);
            cmd.Parameters.AddWithValue("@lastName", txtStudentLastName.Text);
            cmd.Parameters.AddWithValue("@section", txtSection.Text);
            cmd.Parameters.AddWithValue("@remainingTime", 60.ToString());        
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            SqlCommand cmd1 = new SqlCommand("INSERT INTO currentSession VALUES(@studentId ,@date, @timeIn, NULL, @remainingTime)", conn);
            cmd1.Parameters.AddWithValue("@studentId", txtStudentID.Text);
            cmd1.Parameters.AddWithValue("@date", date);
            cmd1.Parameters.AddWithValue("@timeIn", time1);
            cmd1.Parameters.AddWithValue("@remainingTime", 60.ToString());
            cmd1.ExecuteNonQuery();
            cmd1.Parameters.Clear();

            MessageBox.Show("Student successfully sit in!");


            #region previous way to add student
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
            #endregion
        }
        public void Update_Data() // display the data register
        { 
            //var query = from cs in sit.currentSessions
            //            join s in sit.Students on cs.personid equals s.personid
            //            where s.studentid = 
            //EZ - 6/7/2023


            #region prev. display
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
            #endregion
        }

        #endregion

        public Form1()
        {
            InitializeComponent();

            // Add design
            Design.RoundCorner(pnlConfirmExit, 18);
            Design.RoundCorner(pnlLoginFrame, 16);
            Design.RoundCorner(pnlStudsRec, 16);
            Design.RoundCorner(borderpass, 10);
            Design.RoundCorner(pnlDesign, 14);

            int g1 = 14;
            Design.RoundCorner(pass, g1);
            Design.RoundCorner(mrg1, g1);
            Design.RoundCorner(mrg2, g1);
            Design.RoundCorner(mrg3, g1);
            Design.RoundCorner(mrg4, g1);
            Design.RoundCorner(mrg5, g1);
            Design.RoundCorner(mrg6, g1);
            Design.RoundCorner(mrg7, g1);

            int g2 = 16;
            Design.RoundCorner(tm1, g2);
            Design.RoundCorner(tm2, g2);
            Design.RoundCorner(l1, g2);
            Design.RoundCorner(l2, g2);
            Design.RoundCorner(l3, g2);
            Design.RoundCorner(l4, g2);
            Design.RoundCorner(l5, g2);
            Design.RoundCorner(l6, g2);
            Design.RoundCorner(l7, g2);

            Design.RoundCorner(this, 25);
            Design.RoundCorner(DataGrid, 15);
            Design.RoundCorner(recordsView, 15);
            Design.RoundCorner(pnlStudentInfo, 15);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            notClicked = Color.FromArgb(210, 242, 250);
            Clicked = Color.FromArgb(65, 205, 242);

            pnlConfirmExit.Hide();
            exitApp = false;
            Update_Data();

            pnlConfirmExit.Location = new Point(446, 204);
            pnlRecords.Location = new Point(0, 0);
            pnlRecords.Hide();

            Display_Student_Info("00-0000000", "---- ----", "-", "-------", "---- - ---", 0, 0, 0);
            attemptsOfLogin = 0;
            password = "hehe";

            buttonColors = Color.FromArgb(8, 136, 194);

            BtnSearchInRecords.BackColor = buttonColors;
            BtnCancelIn.BackColor = buttonColors;
            BtnConfirm.BackColor = buttonColors;
            BtnDelete.BackColor = buttonColors;
            BtnSearch.BackColor = buttonColors;
            BtnPrint.BackColor = buttonColors;
            BtnStart.BackColor = buttonColors;
            BtnEdit.BackColor = buttonColors;
        }


        /// <summary>
        /// HEKHOK ORASAN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ORASAN(object sender, EventArgs e)
            => lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");


        /// <summary>
        /// Checks if the textbox is focused in the form
        /// </summary>
        private void TextBoxFocus(object sender, EventArgs e)
        {
            void TextBoxBehaviour(Control txtbx, Control mrgin, Control placeholders) =>
                placeholders.Visible = (mrgin.BackColor = txtbx.Focused ? Clicked : notClicked) == notClicked;

            // Border highlight
            TextBoxBehaviour(txtStudentID, mrg1, placeholder1);

            // Border highlight
            TextBoxBehaviour(txtStudentName, mrg2, placeholder2);

            // Border highlight
            TextBoxBehaviour(txtPass, borderpass, placeholder3);

            // Last name
            TextBoxBehaviour(txtStudentLastName, mrg3, placeholder4);

            // Section
            TextBoxBehaviour(txtSection, mrg4, placeholder5);

            // Priority num
            TextBoxBehaviour(txtPriorityNum, mrg5, placeholder6);

            // Search
            TextBoxBehaviour(txtSearchId, mrg6, placeholder7);

            // Middle Initial
            TextBoxBehaviour(txtMiddleInitial, mrg7, placeholder8);
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
            => pnlConfirmExit.Visible = e.Cancel = !exitApp;
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (attemptsOfLogin > 9 && txtPass.Text != password)
                MessageBox.Show($"Please contact an assistant! \nAfter ({attemptsOfLogin}), you have failed to input the correct password!", "Password Incorrect!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                attemptsOfLogin = (exitApp = txtPass.Text == password) ? 0 : attemptsOfLogin + 1;
        }
        private void ENVI_EXIT(object sender, EventArgs e) { if (exitApp) this.Close(); }
        private void exitButton_Click(object sender, EventArgs e)
            => pnlConfirmExit.Show();
        private void BtnCancelIn_Click(object sender, EventArgs e)
        {
            pnlConfirmExit.Visible = exitApp = false;
            txtPass.Text = string.Empty;
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
             => pnlRecords.Visible = (e.KeyCode == Keys.F1 && e.Control);

        private void CalendarClick(object sender, EventArgs e)
        {
            dateToday.Checked = true;
            dateToday.Select();
        }

        private void BtnStart_Click(object sender, EventArgs e) //Update data during log in
        {
            if (txtStudentID.Text == "" && txtStudentName.Text == "" && txtStudentLastName.Text == "" && txtSection.Text == "" && txtPriorityNum.Text == "")
            {
                MessageBox.Show("Please fill out all fields provided!");
            }
            else
            {
                try
                {
                    AddStudent();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }  
            }
            conn.Close();
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

        private void CheckForBadInput(object sender, KeyPressEventArgs e) => e.Handled = char.IsLetter(e.KeyChar);

        private void idNumberHasInput(object sender, EventArgs e) => CheckForInput(txtStudentID, placeholder1);
        private void fullNameHasInput(object sender, EventArgs e) => CheckForInput(txtStudentName, placeholder2);
        private void PassHasInput(object sender, EventArgs e) => CheckForInput(txtPass, placeholder3);
        private void lastnamehasinput(object sender, EventArgs e) => CheckForInput(txtStudentLastName, placeholder4);
        private void sectioninput(object sender, EventArgs e) => CheckForInput(txtSection, placeholder5);
        private void prioinput(object sender, EventArgs e) => CheckForInput(txtPriorityNum, placeholder6);
        private void searchedchanged(object sender, EventArgs e) => CheckForInput(txtSearchId, placeholder7);
        private void initialHasInput(object sender, EventArgs e) => CheckForInput(txtMiddleInitial, placeholder8);

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
        private void inClick(object sender, EventArgs e) => txtMiddleInitial.Focus();

        #endregion



        // USE THIS TO DISPLAY INFO IN SEARCHED RECORDS

        /// <summary>
        /// Displays the student info in correct format
        /// </summary>
        /// <param name="stud_ID">Student ID to display</param>
        /// <param name="stud_fName">Student's First Name</param>
        /// <param name="stud_mInitial">Student's Middle Initial</param>
        /// <param name="stud_lName">Student's Last Name</param>
        /// <param name="stud_Sect">Section</param>
        /// <param name="remaining_Hours">Remaining Hours the student has</param>
        /// <param name="remaining_Minutes">Remaining minutes the student has</param>
        /// <param name="num_of_sit_ins">Number of sit-ins recorded</param>
        private void Display_Student_Info(string stud_ID, string stud_fName, string stud_mInitial, string stud_lName, string stud_Sect, int remaining_Hours, int remaining_Minutes, int num_of_sit_ins)
        {
            // MARK - 6/7/2023 - 8:56 PM
            string FullName = $"{stud_fName}, {stud_fName} {stud_mInitial}.";
            string RemainingBalance = $"{remaining_Hours} Hours, {remaining_Minutes} Minutes";


            displayID.Text = stud_ID;
            displayName.Text = FullName;
            displaySection.Text = stud_Sect;
            displayBalance.Text = RemainingBalance;
            displayNumOfSitIns.Text = num_of_sit_ins.ToString();
        }



        // RECORDS PAGE FUNCTIONS
        private void hideRecords_Click(object sender, EventArgs e)
        {
            pnlRecords.Visible = false;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {

        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {

        }

        private void BtnSearchInRecords_Click(object sender, EventArgs e)
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
