using System;
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
        int attemptsOfLogin;
        string password;
        Color buttonColors;
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
            Design.RoundCorner(mrg7, 12);


            Design.RoundCorner(tm1, 15);
            Design.RoundCorner(tm2, 16);


            Design.RoundCorner(l1, 11);
            Design.RoundCorner(l2, 11);
            Design.RoundCorner(l3, 11);
            Design.RoundCorner(l4, 11);
            Design.RoundCorner(l5, 11);
            Design.RoundCorner(l6, 11);
            Design.RoundCorner(l7, 11);


            Design.RoundCorner(this, 25);
            Design.RoundCorner(DataGrid, 15);
            Design.RoundCorner(recordsView, 15);
            Design.RoundCorner(pnlStudentInfo, 15);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Clicked = Color.FromArgb(65, 205, 242);
            notClicked = Color.FromArgb(210, 242, 250);
            
            mrg1.BackColor = notClicked;
            mrg2.BackColor = notClicked;

            pnlConfirmExit.Hide();
            exitApp = false;
            Update_Data();
            pnlConfirmExit.Location = new Point(446, 204);
            pnlRecords.Location = new Point(0, 0);
            pnlRecords.Hide();

            Display_Student_Info("00-0000000","---- ----","-","-------","---- - ---", 0, 0, 0);
            attemptsOfLogin = 0;
            password = "hehe";

            buttonColors = Color.FromArgb(8, 136, 194);

            btnCancelIn.BackColor = buttonColors;
            btnConfirm.BackColor = buttonColors;
            btnDelete.BackColor = buttonColors;
            btnEdit.BackColor = buttonColors;
            btnPrint.BackColor = buttonColors;
            btnSearch.BackColor = buttonColors;
            btnSearchInRecords.BackColor = buttonColors;
            btnStart.BackColor = buttonColors;    
        }



        /// <summary>
        /// HEKHOK ORASAN
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

            /// Main Behaviour for Textbox User Interface - Mark
            void TextBoxBehaviour(Control txtbx, Control mrgin, Control placeholders)
            {
                if (txtbx.Focused)
                {
                    mrgin.BackColor = Clicked;
                    placeholders.Hide();
                }
                else
                {
                    mrgin.BackColor = notClicked;
                    placeholders.Show();
                }
            }


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
            if (attemptsOfLogin > 10)
                MessageBox.Show($"Please contact an assistant! \nAfter ({attemptsOfLogin}), you have failed to input the correct password!", "Password Incorrect!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (attemptsOfLogin < 10)
            {
                attemptsOfLogin += 1;

                if (txtPass.Text == password) // Check if user input pass is correct // please change implementation during finalization - Mark
                {
                    attemptsOfLogin = 0;
                    exitApp = true;
                }
                else
                    exitApp = false;
            }
        }

        private void ENVI_EXIT(object sender, EventArgs e) { if (exitApp) this.Close(); }
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
                pnlRecords.Show();
        }
        public void AddStudent()
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



            AddStudent();
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

        private void CheckForBadInput(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
    class SeiyaMarx
    {
        [DllImport("Gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        public void RoundCorner(Control ctr, int val) => ctr.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, ctr.Width, ctr.Height, val, val));
    }



}
