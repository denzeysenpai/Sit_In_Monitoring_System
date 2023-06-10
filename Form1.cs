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
        readonly SqlConnection conn = new SqlConnection("Data Source=LAB5-PC21\\ACTSTUDENT;Initial Catalog=SitInMonitoring;Integrated Security=True");
        readonly SeiyaMarx Design = new SeiyaMarx();
        readonly DataSet ds = new DataSet();

        Color buttonColors;
        Color notClicked;
        Color Clicked;
        Color CanStart;
        Color NotStart;

        bool exitApp;
        string password;
        int attemptsOfLogin;

        #region ALL OF FUNCTIONS
        public void EnableStart() =>
            BtnStart.Enabled = (BtnStart.BackColor = CanStart) == CanStart;

        public void DisableStart() =>
            BtnStart.Enabled = !((BtnStart.BackColor = NotStart) == NotStart);


        public void DefaultEnable()
        {
            txtStudentName.Enabled = false;
            txtSection.Enabled = false;
            txtMiddleInitial.Enabled = false;
            txtStudentLastName.Enabled = false;
            BtnStart.Enabled = false;
            BtnStart.BackColor = NotStart;
            txtStudentID.Enabled = true;
            txtStudentID.Focus();
        }

        public void clearStudentText()
        {
            txtStudentName.Text = string.Empty;
            txtMiddleInitial.Text = string.Empty;
            txtStudentLastName.Text = string.Empty;
            txtSection.Text = string.Empty;
        }//DONE
        public void AddStudent()
        {
            DateTime val = DateTime.Now;
            string time1 = val.ToString("hh:mm:ss tt");
            string date = $"{val.Date: MM/dd/yyyy}";

            conn.Open();
            SqlDataAdapter s = new SqlDataAdapter("SELECT * FROM Students WHERE Studentid = '" + txtStudentID.Text.ToString() + "'", conn);
            ds.Clear();
            s.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SqlCommand cmd1 = new SqlCommand("INSERT INTO currentSession(studentId, date, timeIn, remainingTime, personid) SELECT @studentId, @date, @timeIn, s.remainingTime, s.personid FROM Students s WHERE s.studentid = @studentId", conn);
                cmd1.Parameters.AddWithValue("@studentId", txtStudentID.Text);
                cmd1.Parameters.AddWithValue("@date", date);
                cmd1.Parameters.AddWithValue("@timeIn", time1);
                cmd1.ExecuteNonQuery();
                cmd1.Parameters.Clear();
            }
            else
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Students VALUES(@studentId,@firstName, @middleInitial,@lastName,@section, @remainingTime);", conn);
                cmd.Parameters.AddWithValue("@studentId", txtStudentID.Text);
                cmd.Parameters.AddWithValue("@firstName", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@middleInitial", txtMiddleInitial.Text);
                cmd.Parameters.AddWithValue("@lastName", txtStudentLastName.Text);
                cmd.Parameters.AddWithValue("@section", txtSection.Text);
                cmd.Parameters.AddWithValue("@remainingTime", 60.ToString());
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                SqlCommand cmd1 = new SqlCommand("INSERT INTO currentSession(studentId, date, timeIn, remainingTime, personid) SELECT @studentId, @date, @timeIn, s.remainingTime, s.personid FROM Students s WHERE s.studentid = @studentId", conn);
                cmd1.Parameters.AddWithValue("@studentId", txtStudentID.Text);
                cmd1.Parameters.AddWithValue("@date", date);
                cmd1.Parameters.AddWithValue("@timeIn", time1);
                cmd1.ExecuteNonQuery();
                cmd1.Parameters.Clear();
            }
            
            txtStudentID.Text = string.Empty;
            clearStudentText();
            txtStudentID.Focus();
            MessageBox.Show("Student successfully sit in!");
            //Update_Data();
        }//DONE
        public void StudentExisted()
        {
            if (txtStudentID.Text != "")
            {
                SqlDataAdapter s = new SqlDataAdapter("SELECT * FROM Students WHERE Studentid = '" + txtStudentID.Text.ToString() + "'", conn);
                ds.Clear();
                s.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtStudentName.Text = ds.Tables[0].Rows[0]["firstName"].ToString();
                    txtMiddleInitial.Text = ds.Tables[0].Rows[0]["middleInitial"].ToString();
                    txtStudentLastName.Text = ds.Tables[0].Rows[0]["lastName"].ToString();
                    txtSection.Text = ds.Tables[0].Rows[0]["section"].ToString();

                    DialogResult d = MessageBox.Show("Student has been found!", "Record Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (d.Equals(DialogResult.OK))
                    {
                        BtnStart.Enabled = true;
                        BtnStart.BackColor = CanStart;
                    }
                }
                else
                {
                    DialogResult d = MessageBox.Show("This student is not yet registered!", "STUDENT NOT FOUND", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    clearStudentText();
                    
                    if (d.Equals(DialogResult.OK))
                    {
                        txtStudentLastName.Enabled = true;
                        txtSection.Enabled = true;
                        txtMiddleInitial.Enabled = true;
                        txtStudentName.Enabled = true;
                        txtStudentLastName.Focus();
                        DisableStart();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill out student ID first!", "INVALID INPUT!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStudentID.Focus();
            }

        }//DONE
        //public void Update_Data() // DONE
        //{
        //    SqlDataAdapter s = new SqlDataAdapter("SELECT cs.Date, s.studentId, s.firstName, s.middleInitial, s.lastname, s.section, cs.TimeIn, cs.timeout FROM students s JOIN currentSession cs on s.personid = cs.personid", conn);

        //    DataTable dt = new DataTable();
        //    s.Fill(dt);
        //    DataGrid.Rows.Clear();

        //    foreach(DataRow dr in dt.Rows)
        //    {
        //        int cs = DataGrid.Rows.Add();
        //        //int rv = recordsView.Rows.Add();
        //        for(int i = 0; i < 7; i++)
        //        {
        //            DataGrid.Rows[cs].Cells[i].Value = dr[i].ToString();
        //            //recordsView.Rows[rv].Cells[i].Value = dr[i].ToString();
        //        }
        //    }
        //}
        public void LogoutStudent(DataGridViewCellEventArgs e)//DONE
        {
            DateTime date = DateTime.Now;
            string time1 = date.ToString("hh:mm:ss tt");
            DataGridViewRow row = this.DataGrid.Rows[e.RowIndex];
            string studentId = row.Cells["STUDENT_ID"].Value.ToString();

            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO SessionLogs SELECT cs.studentId, cs.date, cs.timeIn, @timeOut, cs.remainingTime, cs.personid FROM currentSession cs WHERE cs.studentid = @studentId", conn);
            cmd.Parameters.AddWithValue("@timeOut", time1);
            cmd.Parameters.AddWithValue("@studentId", studentId);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Logged Out Successfully!");

            SqlCommand cmd2 = new SqlCommand("DELETE FROM currentSession WHERE studentId = @studentId", conn);
            cmd2.Parameters.AddWithValue("@studentId", studentId);
            cmd2.ExecuteNonQuery();

            DataGrid.Rows.RemoveAt(e.RowIndex);
        }
        public void DisplayLogs()
        {

        }//NEXT TO BE MADE -EZSUJERO 6/9/23

        //MARK PALIHOG KO TARONG NYA SA TAB INDEX SA TANANG TEXTBOX
        //NYA PALIHOG NLNG PUD KO REMOVE SA PRIORITY NUMBER MARK KAY 1ST COME 1ST SERVE RAMAN DIAY TA.
        //NYA PLEASE LANG SAD PUD KO MARK NGA E BUTANG DIRI ANG MGA LOCATIONS SA FORM IN CASE NAA KOY IPANG CHANGE, MAG ASK LNG NYA PUD KO UGMA
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
            Design.RoundCorner(mrg6, g1);
            Design.RoundCorner(mrg7, g1);

            int g2 = 16;
            Design.RoundCorner(tm1, g2);
            Design.RoundCorner(tm2, g2);
            Design.RoundCorner(l1, g2);
            Design.RoundCorner(l2, g2);
            Design.RoundCorner(l3, g2);
            Design.RoundCorner(l4, g2);
            Design.RoundCorner(l6, g2);
            Design.RoundCorner(l7, g2);

            Design.RoundCorner(this, 25);
            Design.RoundCorner(pnlDepth, 15);
            Design.RoundCorner(DataGrid, 15);
            Design.RoundCorner(recordsView, 15);
            Design.RoundCorner(pnlStudentInfo, 15);


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            notClicked = Color.FromArgb(210, 242, 250);
            Clicked = Color.FromArgb(65, 205, 242);
            CanStart = Color.FromArgb(7, 163, 58);
            NotStart = Color.FromArgb(65, 205, 242);

            pnlConfirmExit.Hide();
            exitApp = false;
            //Update_Data();

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

            txtStudentID.TabIndex = 0;
            BtnSearch.TabIndex = 1;
            txtStudentLastName.TabIndex = 2;
            txtStudentName.TabIndex = 3;
            txtMiddleInitial.TabIndex = 4;
            txtSection.TabIndex = 5;

            foreach (Control ctr in this.Controls)
            {
                if (ctr is Panel)
                {
                    ctr.TabIndex = 1;
                    ctr.TabStop = false;
                }
                if (ctr is Form)
                {
                    ctr.TabIndex = 0;
                    ctr.TabStop = false;
                    ctr.Enabled = true;
                    ctr.Focus();
                }
            }
            DefaultEnable();

        }

        #region Behavior UI

        private void CheckForBadInput(object sender, KeyPressEventArgs e) => e.Handled = char.IsLetter(e.KeyChar);

        private void idNumberHasInput(object sender, EventArgs e) => CheckForInput(txtStudentID, placeholder1);
        private void fullNameHasInput(object sender, EventArgs e) => CheckForInput(txtStudentName, placeholder2);
        private void PassHasInput(object sender, EventArgs e) => CheckForInput(txtPass, placeholder3);
        private void lastnamehasinput(object sender, EventArgs e) => CheckForInput(txtStudentLastName, placeholder4);
        private void sectioninput(object sender, EventArgs e) => CheckForInput(txtSection, placeholder5);
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
        private void qw1(object sender, EventArgs e) => txtStudentLastName.Focus();
        private void qw2(object sender, EventArgs e) => txtSection.Focus();
        private void placeholder7click(object sender, EventArgs e) => txtSearchId.Focus();
        private void inClick(object sender, EventArgs e) => txtMiddleInitial.Focus();

        #endregion
        #region ui
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
            bool CheckForInput(Control txt) => !(txt.Text == null || txt.Text == "");

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

            // Search
            TextBoxBehaviour(txtSearchId, mrg6, placeholder7);

            // Middle Initial
            TextBoxBehaviour(txtMiddleInitial, mrg7, placeholder8);

            if (CheckForInput(txtStudentID) && CheckForInput(txtStudentName) && CheckForInput(txtStudentLastName) && CheckForInput(txtMiddleInitial) && CheckForInput(txtSection))
                EnableStart();
            else
                DisableStart();
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
        /// <summary>
        /// Exits the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ENVI_EXIT(object sender, EventArgs e) { if (exitApp) this.Close(); }
        private void exitButton_Click(object sender, EventArgs e)
            => pnlConfirmExit.Show();
        private void BtnCancelIn_Click(object sender, EventArgs e)
        {
            pnlConfirmExit.Visible = exitApp = false;
            txtPass.Text = string.Empty;
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 && e.Control)
            {
                pnlRecords.Visible = true;
            }
        }

        private void CalendarClick(object sender, EventArgs e)
        {
            dateToday.Checked = true;
            dateToday.Select();
        }
        #endregion
        private void BtnStart_Click(object sender, EventArgs e) //Update data during log in
        {
            string ErrorMessage = "";
            bool ErrorInInputIsDetected = false;

            bool ControlHasNullInputIn(Control txtbox) =>
                ErrorInInputIsDetected = txtbox.Text == null || txtbox.Text == "";


            if (ControlHasNullInputIn(txtStudentID))
                ErrorMessage += "Please Fill out Student ID!\n";

            if (ControlHasNullInputIn(txtStudentName))
                ErrorMessage += "Please Fill out Student First Name!\n";

            if (ControlHasNullInputIn(txtMiddleInitial))
                ErrorMessage += "Please Fill out Student Middle Initial!\n";

            if (ControlHasNullInputIn(txtStudentLastName))
                ErrorMessage += "Please Fill out Student Last Name!\n";

            if (ControlHasNullInputIn(txtSection))
                ErrorMessage += "Please Fill out Student Section!\n";



            if (ErrorInInputIsDetected)
                MessageBox.Show($"{ErrorMessage}", "INVALID INPUT DETECTED", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                try
                {
                    DefaultEnable();
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
            bool StudentLogsOut = e.RowIndex >= 0 && e.ColumnIndex == DataGrid.Columns["LOG_OUT"].Index;

            if (StudentLogsOut)
            {
                try
                {
                    LogoutStudent(e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
                //Update_Data();
            }
        }

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
            string FullName = $"{stud_lName}, {stud_fName} {stud_mInitial}.";
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
            try
            {
                StudentExisted();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
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

        private void pnlRecords_Paint(object sender, PaintEventArgs e)
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
