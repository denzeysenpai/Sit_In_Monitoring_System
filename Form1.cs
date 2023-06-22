using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Sit_In_Monitoring
{
    public partial class Form1 : Form
    {
        readonly SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ACT-STUDENT\\Documents\\GitHub\\Sit_In_Monitoring_System\\db\\SitInMonitoring.mdf;Integrated Security=True;Connect Timeout=30");
        readonly DataSet ds = new DataSet();
        #region ATTRIBUTES
        readonly SeiyaMarx Design = new SeiyaMarx();


        SeiyaMarx TextboxMargins;
        SeiyaMarx TextboxBodies;
        SeiyaMarx Fifteens;

        bool closeNotify;
        bool notify;
        bool exitApp;
        bool ProcessingDataBase;

        Color buttonColors;
        Color notClicked;
        Color Clicked;
        Color CanStart;
        Color NotStart;

        string password;
        string ReasonForPassword = "";

        int count = 0;
        int attemptsOfLogin;

        readonly int endOfNotification = 1000;

        //int hour;
        //int minute;
        string studentId;
        string section;
        string fName;
        string lName;
        string mInitial;
        const string arrow = "→";

        #endregion ATTRIBUTES

        #region ALL OF FUNCTIONS // hekhokhekohok
        public void SetNotificationOnLoad() =>
            pnlNotification.Left = Width;
        public void NotifySuccessfulSitIn() => // Notification for successful log in
            notify = (notificationMessage.Text = "Logged-in Successfully!") == "Logged-in Successfully!" || notify;
        public void NotifySuccessfulLogOut() => // Notification for successful log out
            notify = (notificationMessage.Text = "Logged-Out Successfully!") == "Logged-Out Successfully!" || notify;

        public void ShowAdminPasswordInput()
        {
            pnlAdminLock.Show();
            pnlAdminLock.BringToFront();
            pnlAdminLock.Enabled = true;
            txtPass.Focus();
            txtStudentID.Enabled = false;
            BtnSearch.Enabled = false;
        }

        public void CloseConfirmation()
        {
            txtStudentID.Enabled = true;
            pnlAdminLock.Hide();
            txtPass.Text = "";
            ReasonForPassword = "";
            BtnSearch.Enabled = true;
        }

        public void PrintRecordsToExelFormat()
        {
            if (recordsView.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < recordsView.Columns.Count + 1; i++)
                {
                    xcelApp.Cells[1, i] = recordsView.Columns[i - 1].HeaderText;

                }
                for (int i = 0; i < recordsView.Rows.Count; i++)
                {
                    for (int j = 0; j < recordsView.Columns.Count; j++)
                    {
                        xcelApp.Cells[i + 2, j + 1] = recordsView.Rows[i].Cells[j].Value.ToString();
                    }
                    xcelApp.Columns.AutoFit();
                    xcelApp.Visible = true;
                }
            }
        } // DONE

        // just add body in local functions
        public void AdminLockReasonConfirmation() // This method is only called after input matches the correct password - maki
        {
            void ReasonIsForExit() =>
                exitApp = true;


            void ReasonIsForEdit()
            {
                newStudentId.PlaceholderText = studentId;
                newSection.PlaceholderText = section;
                newFirstName.PlaceholderText = fName;
                newLastName.PlaceholderText = lName;
                newMiddleInitial.PlaceholderText = mInitial;

                pnlEditUser.Show();

                /* ADD CODE BODY FOR EDIT HERE
                 * 
                 */
            }


            void ReasonIsForDelete()
            {
                /* ADD CODE BODY FOR DELETE HERE
                 * 
                 */
            }


            void ReasonIsForPrint()
            {
                PrintRecordsToExelFormat();
            }


            void ReasonIsForRecords() // Show Records Page
            {
                pnlRecords.Show();
                pnlRecords.Enabled = true;
                pnlRecords.Focus();
                DisplayForLogs();
            }


            switch (ReasonForPassword) // DON'T CHANGE
            {
                case "exit": ReasonIsForExit(); break;
                case "edit": ReasonIsForEdit(); break;
                case "delete": ReasonIsForDelete(); break;
                case "print": ReasonIsForPrint(); break;
                case "records": ReasonIsForRecords(); break;
            }

            CloseConfirmation();
        }


        public void EnableStart() =>
            BtnStart.Enabled = (BtnStart.BackColor = CanStart) == CanStart;

        public void DisableStart() =>
            BtnStart.Enabled = !((BtnStart.BackColor = NotStart) == NotStart);


        public void DefaultEnable() // Enabling of Default Controls
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
        #endregion

        #region main functions-Ez
        public void AddStudent()
        {
            DateTime val = DateTime.Now;
            string time1 = val.ToString("HH:mm:ss tt");
            string date = $"{val.Date: MM/dd/yyyy}";

            SqlDataAdapter check = new SqlDataAdapter("SELECT * FROM currentSession where studentid = '" + txtStudentID.Text + "'", conn);
            DataTable dt = new DataTable();

            check.Fill(dt);


            if (dt.Rows.Count == 0)
            {
                conn.Open();
                SqlDataAdapter s = new SqlDataAdapter("SELECT * FROM Students WHERE Studentid = '" + txtStudentID.Text.ToString() + "'", conn);
                ds.Clear();
                s.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO currentSession(studentId, date, timeIn, timeout, personid) SELECT @studentId, @date, @timeIn, CONVERT(VARCHAR(8), CONVERT(TIME, DATEADD(minute, 60, CONVERT(DATETIME, @timeout))), 108), s.personid FROM Students s WHERE s.studentid = @studentId", conn);
                    cmd1.Parameters.AddWithValue("@studentId", txtStudentID.Text);
                    cmd1.Parameters.AddWithValue("@date", date);
                    cmd1.Parameters.AddWithValue("@timeIn", time1);
                    cmd1.Parameters.AddWithValue("@timeout", time1);
                    cmd1.ExecuteNonQuery();
                    cmd1.Parameters.Clear();

                    //SqlCommand cmd2 = new SqlCommand("INSERT INTO sessionLogs(studentId, date, timeIn, personid) SELECT @studentId, @date, @timeIn, s.personid FROM Students s WHERE s.studentid = @studentId", conn);
                    //cmd2.Parameters.AddWithValue("@studentId", txtStudentID.Text);
                    //cmd2.Parameters.AddWithValue("@date", date);
                    //cmd2.Parameters.AddWithValue("@timeIn", time1);
                    //cmd2.ExecuteNonQuery();
                    //NotifySuccessfulSitIn();
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

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO currentSession(studentId, date, timeIn, timeout,personid) SELECT @studentId, @date, @timeIn, CONVERT(VARCHAR(8), CONVERT(TIME, DATEADD(minute, 60, CONVERT(DATETIME, @timeout))), 108),s.personid FROM Students s WHERE s.studentid = @studentId", conn);
                    cmd1.Parameters.AddWithValue("@studentId", txtStudentID.Text);
                    cmd1.Parameters.AddWithValue("@date", date);
                    cmd1.Parameters.AddWithValue("@timeIn", time1);
                    cmd1.Parameters.AddWithValue("@timeout", time1);
                    cmd1.ExecuteNonQuery();
                    cmd1.Parameters.Clear();

                    NotifySuccessfulSitIn();
                }
            }
            else
            {
                MessageBox.Show("Student is already on a session!");
            }

            txtStudentID.Text = string.Empty;
            clearStudentText();
            txtStudentID.Focus();
            Update_Data();
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

                    DialogResult d = MessageBox.Show("Student Record Exists!", "Record Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (d.Equals(DialogResult.OK))
                    {
                        BtnStart.Enabled = true;
                        BtnStart.BackColor = CanStart;
                    }
                }
                else
                {
                    DialogResult d = MessageBox.Show("This student is not yet registered! \nPlease register before sit-in!", "STUDENT NOT FOUND", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
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
        public void Update_Data() // DONE
        {
            DisplayForLogs();
            SqlDataAdapter s = new SqlDataAdapter("SELECT cs.Date, s.studentId, s.firstName, s.middleInitial, s.lastname, s.section, cs.TimeIn, cs.timeout FROM students s JOIN currentSession cs on s.personid = cs.personid", conn);

            DataTable dt = new DataTable();
            s.Fill(dt);
            DataGrid.Rows.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                int cs = DataGrid.Rows.Add();
                for (int i = 0; i < 8; i++)
                {
                    DataGrid.Rows[cs].Cells[i].Value = dr[i].ToString();
                }
            }
        }

        public void LogoutStudent(DataGridViewCellEventArgs e) //FIXED 6/20/23 NEED TO BE TESTED
        {
            DateTime date = DateTime.Now;
            string dateDb = dateToday.Value.ToString(" MM/dd/yyyy");
            string time1 = date.ToString("HH:mm:ss tt");
            DataGridViewRow row = this.DataGrid.Rows[e.RowIndex];
            string studentId = row.Cells["STUDENT_ID"].Value.ToString();

            SqlDataAdapter count = new SqlDataAdapter("SELECT * FROM SessionLogs;", conn);
            SqlDataAdapter calc = new SqlDataAdapter("SELECT DATEDIFF(second, TimeIn, '"+ time1 +"') / 3600.0 from currentsession where studentid = '"+ studentId +"' and date = '"+ dateDb +"'", conn);
            DataTable dt = new DataTable();
            DataTable timeUsed = new DataTable();

            calc.Fill(timeUsed);

            count.Fill(dt);

            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO SessionLogs SELECT cs.studentId, cs.date, cs.timeIn, @timeOut, @timeUsed, cs.personid FROM currentSession cs WHERE cs.studentid = @studentId and date = @dateNow", conn);
            cmd.Parameters.AddWithValue("@timeOut", time1);
            cmd.Parameters.AddWithValue("@studentId", studentId);
            cmd.Parameters.AddWithValue("@dateNow", dateDb);
            cmd.Parameters.AddWithValue("@timeUsed", timeUsed.Rows[0][0].ToString());
            cmd.ExecuteNonQuery();

            SqlCommand substractTime = new SqlCommand("UPDATE Students SET remainingTime = remainingTime - (SELECT CONVERT(DECIMAL(16, 6), TimeUsed) FROM SessionLogs WHERE studentID = @studentId and timeout = @timeOut) WHERE studentID = @studentId;", conn);
            substractTime.Parameters.AddWithValue("studentId", studentId);
            substractTime.Parameters.AddWithValue("@timeOut", time1);
            substractTime.Parameters.AddWithValue("@logid", dt.Rows.Count);
            substractTime.Parameters.AddWithValue("date", dateToday.Value.ToString(" MM/dd/yyyy"));
            substractTime.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand("DELETE FROM currentSession WHERE studentId = @studentId and date = @dateNow", conn);
            cmd2.Parameters.AddWithValue("@studentId", studentId);
            cmd2.Parameters.AddWithValue("@dateNow", dateDb);
            cmd2.ExecuteNonQuery();

            DataGrid.Rows.RemoveAt(e.RowIndex);
            ProcessingDataBase = true;
        }
        public void SearchStudentAllLogs()//DONE
        {
            recordsView.Rows.Clear();
            SqlDataAdapter s = new SqlDataAdapter("SELECT sl.Date, s.studentId, s.firstName, s.middleInitial ,s.lastname, s.section, sl.TimeIn, sl.timeout, sl.timeUsed FROM students s JOIN sessionLogs sl on s.studentid = sl.studentid where concat(sl.studentid, s.firstName, s.middleInitial,s.lastname, s.section) like '%" + txtSearchId.Text + "%'", conn);

            DataTable dt = new DataTable();
            dt.Clear();
            s.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int rv = recordsView.Rows.Add();
                for (int i = 0; i < 9; i++)
                {
                    recordsView.Rows[rv].Cells[i].Value = dr[i].ToString();
                }
            }
        }
        public void DisplayForLogs()
        {
            recordsView.Rows.Clear();
            SqlDataAdapter s = new SqlDataAdapter("SELECT sl.Date, s.studentId, s.firstName, s.middleInitial ,s.lastname, s.section, sl.TimeIn, sl.timeout, sl.timeUsed FROM students s JOIN sessionLogs sl on s.studentid = sl.studentid where sl.Date like '%" + dateForRecords.Value.ToString("MM/dd/yyyy") + "%' and sl.studentid like '%" + txtSearchId.Text + "%'", conn);

            DataTable dt = new DataTable();
            dt.Clear();
            s.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int rv = recordsView.Rows.Add();
                for (int i = 0; i < 9; i++)
                {
                    recordsView.Rows[rv].Cells[i].Value = dr[i].ToString();
                }
            }
        }//DONE
        public void RestrictTime()
        {
            SqlDataAdapter check = new SqlDataAdapter("SELECT * FROM currentSession where studentid = '" + txtStudentID.Text + "'", conn);
            DataTable wew = new DataTable();

            check.Fill(wew);


            if (wew.Rows.Count == 0)
            {
                SqlDataAdapter restrict = new SqlDataAdapter("SELECT studentID, SUM(TimeUsed) AS TotalTimeUsed FROM SessionLogs WHERE studentID = '" + txtStudentID.Text + "' AND DATE = '" + dateToday.Value.ToString(" MM/dd/yyyy") + "' GROUP BY studentID HAVING SUM(TimeUsed) >= 1;", conn);
                DataTable dt = new DataTable();

                restrict.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    if (MessageBox.Show("This student have already reached session limit time!\r\n(CANCEL to override)", "Time Limit Reached!", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        if (MessageBox.Show("Do you want to override student's time?", "Override?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            AddStudent();
                        }
                        else
                        {
                            clearStudentText();
                        }

                    }
                    else
                    {
                        clearStudentText();
                    }
                }
            }
            else
            {
                MessageBox.Show("Student is already on a session!");
            }

        }//DONE

        public void notifyTimeDone()
        {
            //try
            //{
            //    { 
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
           
        }
        #endregion



        // 4, 150
        // 4, 370
        public Form1()
        {
            InitializeComponent();

            // Add design
            Design.RoundCorner(this, 25);

            TextboxMargins = new SeiyaMarx(pass, mrg1, mrg2, mrg3, mrg4, mrg6, mrg7, borderpass, 18);
            TextboxBodies = new SeiyaMarx(tm1, tm2, l1, l2, l3, l4, l6, l7, 17);
            Fifteens = new SeiyaMarx(pnlmrgn, pnlLoginFrame, pnlStudsRec, pnlDesign, pnlDepth, DataGrid, recordsView, pnlStudentInfo, pnlDate, 15);

            TextboxMargins.RoundCorner();
            TextboxBodies.RoundCorner();
            Fifteens.RoundCorner();

            Design.RoundCorner(pnlNotification, 50);
            Design.RoundCorner(pnlDateMargin, 15);
            Design.RoundCorner(pnlLoginBody, 15);
            Design.RoundCorner(pnlAdminLock, 18);
            Design.RoundCorner(pnlEditUser, 18);
            Design.RoundCorner(pnltm2, 15);
            Design.RoundCorner(pnlIn, 16);
            Design.RoundCorner(pnlPleaseWait, 40);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Default values on first load, may or may not change during run time
            notClicked = Color.FromArgb(210, 242, 250);
            Clicked = Color.FromArgb(65, 205, 242);
            CanStart = Color.FromArgb(7, 163, 58);
            NotStart = Color.FromArgb(65, 205, 242);

            pnlAdminLock.Hide();
            exitApp = false;
            Update_Data();
            DisplayForLogs();
            ProcessingDataBase = false;

            pnlAdminLock.Location = new Point(446, 204);
            pnlRecords.Location = new Point(0, 0);
            pnlRecords.Hide();

            Display_Student_Info("00-0000000", "---- ----", "-", "-------", "---- - ---", 0, 0, 0);
            attemptsOfLogin = 0;
            password = "hehe";

            buttonColors = Color.FromArgb(8, 136, 194);

            BtnConfirm.BackColor = Color.Black;
            BtnCancelIn.BackColor = Color.Black;

            BtnDelete.BackColor = Color.FromArgb(0, 93, 130);
            BtnSearch.BackColor = buttonColors;
            BtnPrint.BackColor = Color.FromArgb(0, 50, 94);
            BtnStart.BackColor = buttonColors;
            BtnEdit.BackColor = Color.FromArgb(0, 134, 158);

            txtStudentID.TabIndex = 0;
            BtnSearch.TabIndex = 1;
            txtStudentLastName.TabIndex = 2;
            txtStudentName.TabIndex = 3;
            txtMiddleInitial.TabIndex = 4;
            txtSection.TabIndex = 5;

            pnlEditUser.Hide();
            pnlEditUser.Location = new Point(184, 95);
            pnlEditUser.Size = new Size(1115, 565);
            pnlEditUser.BackColor = Color.Black;
            pnlPleaseWait.Location = new Point((Width / 2) - (pnlPleaseWait.Width / 2), (Height / 2) - (pnlPleaseWait.Height / 2));

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

            closeNotify = false;
            notify = false;

            SetNotificationOnLoad();
            DefaultEnable();
            Update();
        }

        #region _Behavior UI
        private void idNumberHasInput(object sender, EventArgs e)
        {
            BtnSearch.Enabled = true;
            BtnSearch.Text = "SEARCH";
            CheckForBadInput_In(txtStudentID, placeholder1);
        }
        private void CheckForBadInput(object sender, KeyPressEventArgs e) => e.Handled = char.IsLetter(e.KeyChar);
        private void fullNameHasInput(object sender, EventArgs e) => CheckForBadInput_In(txtStudentName, placeholder2);
        private void PassHasInput(object sender, EventArgs e) => CheckForBadInput_In(txtPass, placeholder3);
        private void lastnamehasinput(object sender, EventArgs e) => CheckForBadInput_In(txtStudentLastName, placeholder4);
        private void sectioninput(object sender, EventArgs e) => CheckForBadInput_In(txtSection, placeholder5);
        private void searchedchanged(object sender, EventArgs e) => CheckForBadInput_In(txtSearchId, placeholder7);
        private void initialHasInput(object sender, EventArgs e) => CheckForBadInput_In(txtMiddleInitial, placeholder8);


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


        private void NotificationTimerSpecific_Tick(object sender, EventArgs e)
        {

            pnlNotification.Visible = !pnlPleaseWait.Visible;
            // NOTIFICATION PANEL

            // Notification Animation go BrrRrRrrrR
            if (ProcessingDataBase == false)
            {
                closeNotify = count >= 600 ? !(notify = (count = 0) != 0) : closeNotify;
                count = closeNotify == false && notify && pnlNotification.Left < endOfNotification + 40 ? count + 10 : count;
                pnlNotification.Left = notify && pnlNotification.Left > endOfNotification ? pnlNotification.Left - 40 : pnlNotification.Left;
                pnlNotification.Left = closeNotify && pnlNotification.Left < Width ? pnlNotification.Left + 40 : pnlNotification.Left;
                closeNotify = (!closeNotify || pnlNotification.Left < Width) && closeNotify;
            }
            // - hehe
        }

        /// <summary>
        /// HEKHOK ORASAN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ORASAN(object sender, EventArgs e)
        {
            lblTime2.Text = lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");

            notifyTimeDone();
        }
            
        /// <summary>
        /// Checks if the textbox is focused in the form
        /// </summary>
        private void TextBoxFocus(object sender, EventArgs e)
        {
            void TextBox_Behavior(Control txtbx, Control mrgin, Control placeholders) => // Text box placeholders _Behavior
                placeholders.Visible = (mrgin.BackColor = txtbx.Focused ? Clicked : notClicked) == notClicked;
            bool CheckForBadInput_In(Control txt) =>
                !(txt.Text == null || txt.Text == "");

            // Border highlight
            TextBox_Behavior(txtStudentID, mrg1, placeholder1);

            // Border highlight
            TextBox_Behavior(txtStudentName, mrg2, placeholder2);

            // Border highlight
            TextBox_Behavior(txtPass, borderpass, placeholder3);

            // Last name
            TextBox_Behavior(txtStudentLastName, mrg3, placeholder4);

            // Section
            TextBox_Behavior(txtSection, mrg4, placeholder5);

            // Search
            placeholder7.Visible = (mrg6.BackColor = txtSearchId.Focused ? Clicked : Color.FromArgb(0, 70, 92)) == Color.FromArgb(0, 70, 92);

            // Middle Initial
            TextBox_Behavior(txtMiddleInitial, mrg7, placeholder8);

            if (CheckForBadInput_In(txtStudentID) && CheckForBadInput_In(txtStudentName) && CheckForBadInput_In(txtStudentLastName) && CheckForBadInput_In(txtMiddleInitial) && CheckForBadInput_In(txtSection))
                EnableStart();
            else
                DisableStart();


            lblChanges.Visible = lblConfirm.Visible = (confirm1.Visible || confirm2.Visible || confirm3.Visible || confirm4.Visible || confirm5.Visible);
        }


        /// <summary>
        /// Checks for input in the given textboxes
        /// </summary>
        /// <param name="ctr"></param>
        /// <param name="placeholder"></param>
        void CheckForBadInput_In(Control ctr, Control placeholder)
        {
            if (ctr.Text == string.Empty || ctr.Text == null) { placeholder.BringToFront(); } else { placeholder.SendToBack(); }
        }

        private void FormWillBeClosed(object sender, FormClosingEventArgs e)
        {
            ReasonForPassword = "exit";
            pnlAdminLock.Visible = e.Cancel = !exitApp;
        }
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (attemptsOfLogin > 9 && txtPass.Text != password)
                MessageBox.Show($"Please contact an assistant! \nAfter ({attemptsOfLogin}), you have failed to input the correct password!", "Password Incorrect!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                if (txtPass.Text == password)
                {
                    AdminLockReasonConfirmation();
                    attemptsOfLogin = 0;
                }
                else
                    attemptsOfLogin++;
            }
        }
        /// <summary>
        /// Exits the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ENVI_EXIT(object sender, EventArgs e) { if (exitApp) this.Close(); }
        private void exitButton_Click(object sender, EventArgs e)
        {
            ReasonForPassword = "exit";
            ShowAdminPasswordInput();
        }
        private void BtnCancelIn_Click(object sender, EventArgs e)
        {
            txtPass.Text = (pnlAdminLock.Visible = exitApp = false) ? string.Empty : string.Empty;
            BtnSearch.Enabled = true;
            txtStudentID.Enabled = true;
            txtStudentID.Focus();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 && e.Control)
            {
                ReasonForPassword = "records";
                ShowAdminPasswordInput();
            }


            // Tab Switch Code - maki hekhok // HEHEHEHEHEHEHEHE MUGANA
            if (e.KeyCode == Keys.Tab && txtStudentID.Focused)
                BtnSearch.Focus();
            if (e.KeyCode == Keys.Tab && txtStudentLastName.Focused)
                txtStudentName.Focus();
            if (e.KeyCode == Keys.Tab && txtStudentName.Focused)
                txtMiddleInitial.Focus();
            if (e.KeyCode == Keys.Tab && txtStudentID.Focused)
                txtSection.Focus();
            if (e.KeyCode == Keys.Tab && txtSection.Focused)
                BtnStart.Focus();
        }

        private void CalendarClick(object sender, EventArgs e) // Calendar in main page
        {
            dateToday.Checked = true;
            dateToday.Select();
        }
        #endregion

        private void BtnStart_Click(object sender, EventArgs e) //Update data during log in
        {
            SqlDataAdapter restrict = new SqlDataAdapter("SELECT studentID, SUM(TimeUsed) AS TotalTimeUsed FROM SessionLogs WHERE studentID = '" + txtStudentID.Text + "' AND DATE = '" + dateToday.Value.ToString(" MM/dd/yyyy") + "' GROUP BY studentID HAVING SUM(TimeUsed) >= 1;", conn);
            DataTable dt = new DataTable();
            bool Add;
            restrict.Fill(dt);

            if (dt.Rows.Count >= 1)
            {
                Add = false;
            }
            else
            {
                Add = true;
            }
            string ErrorMessage = "";
            bool ErrorInInputIsDetected = false; // this checks overall invalid input

            bool ControlHasNullInputIn(Control txtbox) => // this specifies invalid input
                ErrorInInputIsDetected = txtbox.Text == null || txtbox.Text == "";
            //      ^^^^^INVALID INPUT DETECTED

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
                    if (Add == true)
                    {
                        AddStudent();
                    }
                    else
                    {
                        RestrictTime();
                    }
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
                Update_Data();
            }
        }

        // USE THIS TO DISPLAY INFO IN SEARCHED RECORDS
        ///// <summary>
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
        private void hideRecords_Click(object sender, EventArgs e) =>
            pnlRecords.Visible = false;


        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // search button is disabled after search, if edit in ID is detected then button is enabled true
                BtnSearch.Text = "-----";
                BtnSearch.Enabled = false;
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
            ReasonForPassword = "delete";
            ShowAdminPasswordInput();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            ReasonForPassword = "edit";

            changesInfo1.Hide();
            changesInfo2.Hide();
            changesInfo3.Hide();
            changesInfo4.Hide();
            changesInfo5.Hide();

            lblChanges.Hide();
            lblConfirm.Hide();

            confirm1.Hide();
            confirm2.Hide();
            confirm3.Hide();
            confirm4.Hide();
            confirm5.Hide();

            ShowAdminPasswordInput();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            ReasonForPassword = "print";
            ShowAdminPasswordInput();
        }

        private void BtnSearchInRecords_Click(object sender, EventArgs e)
        {
            SearchStudentAllLogs();
        }

        private void txtSearchId_KeyPress(object sender, KeyPressEventArgs e)
        {
            DisplayForLogs();
        }

        private void StudentIdNumberEffect(object sender, EventArgs e)
        {
            placeholder7.ForeColor = Color.FromArgb(0, 234, 255);
        }

        private void StudentIdNumberEffectEnd(object sender, EventArgs e)
        {
            placeholder7.ForeColor = Color.FromArgb(154, 214, 230);
        }

        private void RecordView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (recordsView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    SqlDataAdapter c = new SqlDataAdapter("SELECT * FROM SessionLogs WHERE studentId = '" + recordsView.Rows[e.RowIndex].Cells["lStudentId"].FormattedValue.ToString() + "'", conn);
                    SqlDataAdapter s = new SqlDataAdapter("SELECT * FROM students WHERE studentId = '" + recordsView.Rows[e.RowIndex].Cells["lStudentId"].FormattedValue.ToString() + "'", conn);
                    DataTable dt = new DataTable();
                    dt.Clear();
                    ds.Clear();
                    s.Fill(ds);
                    c.Fill(dt);

                    studentId = displayID.Text = recordsView.Rows[e.RowIndex].Cells["lStudentId"].FormattedValue.ToString();
                    displayName.Text = recordsView.Rows[e.RowIndex].Cells["lFirstName"].FormattedValue.ToString() + " " + recordsView.Rows[e.RowIndex].Cells["lMiddleInitial"].FormattedValue.ToString() + " " + recordsView.Rows[e.RowIndex].Cells["lLastName"].FormattedValue.ToString();
                    section = displaySection.Text = recordsView.Rows[e.RowIndex].Cells["lSection"].FormattedValue.ToString();
                    displayBalance.Text = ds.Tables[0].Rows[0]["RemainingTime"].ToString() + " hours";
                    displayNumOfSitIns.Text = dt.Rows.Count.ToString();
                    fName = recordsView.Rows[e.RowIndex].Cells["lFirstName"].FormattedValue.ToString();
                    lName = recordsView.Rows[e.RowIndex].Cells["lLastName"].FormattedValue.ToString();
                    mInitial = recordsView.Rows[e.RowIndex].Cells["lMiddleInitial"].FormattedValue.ToString();

                }
            }
            catch (Exception)
            {

            }

        }

        private void dateForRecords_ValueChanged(object sender, EventArgs e)
        {
            DisplayForLogs();
        }

        private void LBLBTNSEARCH_Click(object sender, EventArgs e)
        {
            SearchStudentAllLogs();
        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            ReasonForPassword = "";
            pnlEditUser.Hide();
        }

        private void btnConfirmEdit_Click(object sender, EventArgs e) //won't display the text, adjust tommorow
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE students SET studentid = @studentid, firstName = @firstName, middleInitial = @middleInitial, lastName = @lastName, section = @section where studentid = @studentid", conn);

                cmd.Parameters.AddWithValue("@studentid", newStudentId.PlaceholderText);
                cmd.Parameters.AddWithValue("@firstName", newFirstName.PlaceholderText);
                cmd.Parameters.AddWithValue("@middleInitial", newMiddleInitial.PlaceholderText);
                cmd.Parameters.AddWithValue("@lastName", newLastName.PlaceholderText);
                cmd.Parameters.AddWithValue("@section", newSection.PlaceholderText);
                cmd.ExecuteNonQuery();

                DisplayForLogs();
                conn.Close();

                ReasonForPassword = "";
                pnlEditUser.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }    
        }



        void CheckForChanges(string _old, string _new, Control changes, CheckBox cbx)
        {
            if (_old != _new && (_new != null && _new != string.Empty))
            {
                changes.Visible = true;
                changes.Text = $"{_old} {arrow} {_new}";
                cbx.Visible = true;
            }
            else
            {
                changes.Visible = false;
                changes.Text = "";
                cbx.Visible = false;
            }
        }

        private void studentidTextChangedEdit(object sender, EventArgs e) =>
            CheckForChanges(newStudentId.PlaceholderText, newStudentId.Texts, changesInfo1, confirm1);


        private void firstNameTextChangedEdit(object sender, EventArgs e) =>
            CheckForChanges(newFirstName.PlaceholderText, newFirstName.Texts, changesInfo2, confirm2);

        private void lastNameTextChangedEdit(object sender, EventArgs e) =>
            CheckForChanges(newLastName.PlaceholderText, newLastName.Texts, changesInfo3, confirm3);

        private void middleInitialTextChangedEdit(object sender, EventArgs e) =>
            CheckForChanges(newMiddleInitial.PlaceholderText, newMiddleInitial.Texts, changesInfo4, confirm4);

        private void sectionTextChangedEdit(object sender, EventArgs e) =>
            CheckForChanges(newSection.PlaceholderText, newSection.Texts, changesInfo5, confirm5);

        private void ProcessDB(object sender, EventArgs e)
        {
            // Please Wait Notification after Log Out
            if (ProcessingDataBase)
            {
                DataGrid.Enabled = false;
                pnlPleaseWait.Show();
                DataGrid.Columns["LOG_OUT"].Visible = false;
                Thread.Sleep(1500);
                NotifySuccessfulLogOut();
                ProcessingDataBase = false;
            }
            else
            {
                pnlPleaseWait.Hide();
                DataGrid.Columns["LOG_OUT"].Visible = true;
                DataGrid.Enabled = true;
            }
        }
    }
    class SeiyaMarx
    {
        List<Control> Set = new List<Control>();
        int radius;
        public SeiyaMarx() {/**/}
        public SeiyaMarx(Control ctr1, Control ctr2, Control ctr3, Control ctr4, Control ctr5, Control ctr6, Control ctr7, int radius)
        {
            Set.Add(ctr1);
            Set.Add(ctr2);
            Set.Add(ctr3);
            Set.Add(ctr4);
            Set.Add(ctr5);
            Set.Add(ctr6);
            Set.Add(ctr7);
            this.radius = radius;
        }
        public SeiyaMarx(Control ctr1, Control ctr2, Control ctr3, Control ctr4, Control ctr5, Control ctr6, Control ctr7, Control ctr8, int radius)
        {
            Set.Add(ctr1);
            Set.Add(ctr2);
            Set.Add(ctr3);
            Set.Add(ctr4);
            Set.Add(ctr5);
            Set.Add(ctr6);
            Set.Add(ctr7);
            Set.Add(ctr8);
            this.radius = radius;
        }
        public SeiyaMarx(Control ctr1, Control ctr2, Control ctr3, Control ctr4, Control ctr5, Control ctr6, Control ctr7, Control ctr8, Control ctr9, int radius)
        {
            Set.Add(ctr1);
            Set.Add(ctr2);
            Set.Add(ctr3);
            Set.Add(ctr4);
            Set.Add(ctr5);
            Set.Add(ctr6);
            Set.Add(ctr7);
            Set.Add(ctr8);
            Set.Add(ctr9);
            this.radius = radius;
        }

        [DllImport("Gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        public void RoundCorner(Control ctr, int val) => ctr.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, ctr.Width, ctr.Height, val, val));
        public void RoundCorner()
        {
            foreach (Control ctr in Set)
            {
                ctr.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, ctr.Width, ctr.Height, radius, radius));
            }
        }
    }
}
