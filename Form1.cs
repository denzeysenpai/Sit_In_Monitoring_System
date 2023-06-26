using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Sit_In_Monitoring
{
    public partial class SitInMonitoringForm : Form
    {
        readonly SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\source\\repos\\denzeysenpai\\Sit_In_Monitoring_System\\db\\SitInMonitoring.mdf;Integrated Security=True;Connect Timeout=30");
        readonly DataSet ds = new DataSet();

        #region ATTRIBUTES
        readonly SeiyaMarxElls Design = new SeiyaMarxElls();


        SeiyaMarxElls TextboxMargins;
        SeiyaMarxElls TextboxBodies;
        SeiyaMarxElls Fifteens;

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

        #region ALL OF FUNCTIONS // hekhokhekohok - Mark
        void ResetValuesInInput()
        {
            newFirstNameValue.Clear();
            newMiddleInitialValue.Clear();
            newLastNameValue.Clear();
            newSectionValue.Clear();
        }

        void OpenSQL() =>
            conn.Open();
        void CloseSQL() =>
            conn.Close();
        void SetNotificationOnLoad() =>
            pnlNotification.Left = Width;
        void NotifySuccessfulSitIn() => // Notification for successful log in
            notify = (notificationMessage.Text = "Logged-in Successfully!") == "Logged-in Successfully!" || notify;
        void NotifySuccessfulLogOut() => // Notification for successful log out
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

            void ReasonIsForPrint() =>
                pnlSetPrintOptions.Show();

            void ReasonIsEditStudentDetails()
            {
                oldStudentIDValue.Text = studentId;
                oldSectionValue.Text = section;
                oldFirstNameValue.Text = fName;
                oldLastNameValue.Text = lName;
                oldMiddleInitialValue.Text = mInitial;

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
                case "edit": ReasonIsEditStudentDetails(); break;
                case "print": ReasonIsForPrint(); break;
                case "delete": ReasonIsForDelete(); break;
                case "records": ReasonIsForRecords(); break;
                default: /**/ break;
            }
            CloseConfirmation();

            // MONTHLY
            /* WEEKLY
             * DAILY //DONE
             * PER STUDENT 
             * SEMESTRAL
             * 
             * 
             * ALLOW PRINTING OF DIFFERENT CONDITIONS
             
             */
        }


        public void EnableStart() =>
            BtnStart.Enabled = (BtnStart.BackColor = CanStart) == CanStart;

        public void DisableStart() =>
            BtnStart.Enabled = !((BtnStart.BackColor = NotStart) == NotStart);

        public void DefaultEnable() // Enabling of Default Controls
        {
            void SetControls(List<Control> ctr)
            {
                foreach(Control c in ctr)
                {
                    c.Enabled = false;
                    c.Text = string.Empty;
                }
            }
            List<Control> txts = new List<Control>() { txtStudentName, txtSection, txtMiddleInitial , txtStudentLastName };
            SetControls(txts);

            BtnStart.Enabled = false;
            BtnStart.BackColor = NotStart;
            BtnSearch.Text = "SEARCH";

            txtStudentID.Enabled = true;
            txtStudentID.Focus();
        }

        public void clearStudentText()
        {
            txtSection.Text = string.Empty;
            txtStudentName.Text = string.Empty;
            txtMiddleInitial.Text = string.Empty;
            txtStudentLastName.Text = string.Empty;
        }//DONE
        #endregion

        #region main functions-Ez
        public void AddStudent()
        {
            DateTime val = DateTime.Now;
            string time1 = val.ToString("HH:mm:ss");
            string date = $"{val.Date: MM/dd/yyyy}";

            SqlDataAdapter check = new SqlDataAdapter("SELECT * FROM currentSession where studentid = '" + txtStudentID.Text + "'", conn);
            System.Data.DataTable dt = new System.Data.DataTable();

            check.Fill(dt);

            try
            {
                if (dt.Rows.Count == 0)
                {
                    OpenSQL();
                    SqlDataAdapter s = new SqlDataAdapter("SELECT * FROM Students WHERE Studentid = '" + txtStudentID.Text.ToString() + "'", conn);
                    ds.Clear();
                    s.Fill(ds);

                    void InsertNewDataToCurrentSessions()
                    {
                        SqlCommand cmd1 = new SqlCommand("INSERT INTO currentSession(studentId, date, timeIn, timeout, personid) SELECT @studentId, @date, @timeIn, CONVERT(VARCHAR(8), CONVERT(TIME, DATEADD(minute, 60, CONVERT(DATETIME, @timeout))), 108), s.personid FROM Students s WHERE s.studentid = @studentId", conn);
                        cmd1.Parameters.AddWithValue("@studentId", txtStudentID.Text);
                        cmd1.Parameters.AddWithValue("@date", date);
                        cmd1.Parameters.AddWithValue("@timeIn", time1);
                        cmd1.Parameters.AddWithValue("@timeout", time1);
                        cmd1.ExecuteNonQuery();
                        cmd1.Parameters.Clear();

                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        InsertNewDataToCurrentSessions();
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

                        InsertNewDataToCurrentSessions();

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
            }
            catch (SqlException)
            {
                DefaultEnable();
            }
            catch (ArgumentOutOfRangeException)
            {
                DefaultEnable();
            }
            catch (Exception) {/**/}
            
        }//DONE

        bool NoInjection = true;
        /// <summary>
        /// Checks for basic SQL Injection attack attempt |> Mark
        /// </summary>
        /// <param name="txt"></param>
        void CatchSQLInjection(Control txt) 
        {
            void SecurityMeasure()
            {
                txt.Text = string.Empty;
                MessageBox.Show(
                    "Injection attempt detected, if this was a mistake," +
                    "\nplease double check input!",
                    "INJECTION ATTEMPT?",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);

                NoInjection = false;
            }

            foreach (char s in txt.Text)
            {
                if (s == '\'')
                {
                    SecurityMeasure();
                }
            }

            if (txt.Text.ToUpper().Contains("SELECT") ||
                txt.Text.ToUpper().Contains("DROP") ||
                txt.Text.ToUpper().Contains("DELETE") ||
                txt.Text.ToUpper().Contains("\'--") ||
                txt.Text.ToUpper().Contains("UPDATE") ||
                txt.Text.ToUpper().Contains("*") ||
                txt.Text.ToUpper().Contains("SET"))
            {
                SecurityMeasure();
            }
        }


        public void StudentExisted()
        {
            CatchSQLInjection(txtStudentID);
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
            SqlDataAdapter s = new SqlDataAdapter("SELECT cs.Date, s.studentId, s.firstName, s.middleInitial, s.lastname, s.section, cs.TimeIn, cs.timeout FROM students s JOIN currentSession cs on s.personid = cs.personid and cs.date = '" + dateToday.Value.ToString(" MM/dd/yyyy") + "'", conn);

            System.Data.DataTable dt = new System.Data.DataTable();
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
            ProcessingDataBase = true;
            DateTime date = DateTime.Now;
            string dateDb = dateToday.Value.ToString(" MM/dd/yyyy");
            string time1 = date.ToString("HH:mm:ss tt");
            DataGridViewRow row = this.DataGrid.Rows[e.RowIndex];
            string studentId = row.Cells["STUDENT_ID"].Value.ToString();

            SqlDataAdapter count = new SqlDataAdapter("SELECT * FROM SessionLogs;", conn);
            SqlDataAdapter calc = new SqlDataAdapter("SELECT DATEDIFF(second, TimeIn, '" + time1 + "') / 3600.0 from currentsession where studentid = '" + studentId + "' and date = '" + dateDb + "'", conn);
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable timeUsed = new System.Data.DataTable();

            calc.Fill(timeUsed);

            count.Fill(dt);

            OpenSQL();
            SqlCommand cmd = new SqlCommand("INSERT INTO SessionLogs SELECT cs.studentId, cs.date, cs.timeIn, @timeOut, @timeUsed, cs.personid FROM currentSession cs WHERE cs.studentid = @studentId and date = @dateNow", conn);
            cmd.Parameters.AddWithValue("@studentId", studentId);
            cmd.Parameters.AddWithValue("@timeOut", time1);
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
        }
        public void SearchStudentAllLogs()//DONE
        {
            recordsView.Rows.Clear();
            SqlDataAdapter s = new SqlDataAdapter("SELECT sl.Date, s.studentId, s.firstName, s.middleInitial ,s.lastname, s.section, sl.TimeIn, sl.timeout, sl.timeUsed FROM students s JOIN sessionLogs sl on s.studentid = sl.studentid where concat(sl.studentid, s.firstName, s.middleInitial,s.lastname, s.section) like '%" + txtSearchId.Text + "%'", conn);

            System.Data.DataTable dt = new System.Data.DataTable();
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

            System.Data.DataTable dt = new System.Data.DataTable();
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
            SqlDataAdapter balance = new SqlDataAdapter("SELECT RemainingTime FROM students where studentid = '" + txtStudentID.Text + "'", conn);
            ds.Clear();
            balance.Fill(ds);

            double remainingTime = Convert.ToDouble(ds.Tables[0].Rows[0]["RemainingTime"]);

            if (remainingTime > 0)
            {
                SqlDataAdapter check = new SqlDataAdapter("SELECT * FROM currentSession where studentid = '" + txtStudentID.Text + "'", conn);
                System.Data.DataTable wew = new System.Data.DataTable();

                check.Fill(wew);


                if (wew.Rows.Count == 0)
                {
                    SqlDataAdapter restrict = new SqlDataAdapter("SELECT studentID, SUM(TimeUsed) AS TotalTimeUsed FROM SessionLogs WHERE studentID = '" + txtStudentID.Text + "' AND DATE = '" + dateToday.Value.ToString(" MM/dd/yyyy") + "' GROUP BY studentID HAVING SUM(TimeUsed) >= 1;", conn);
                    System.Data.DataTable dt = new System.Data.DataTable();

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
            }
            else
            {
                MessageBox.Show("Student have already used total hours sit-in!");
                clearStudentText();
            }


        }//DONE

        public void notifyTimeDone()
        {
            try
            {
                DateTime val = DateTime.Now;
                TimeSpan time1 = TimeSpan.Parse(val.ToString("HH:mm:ss"));

                // Iterate through the rows in the DataGridView
                foreach (DataGridViewRow row in DataGrid.Rows)
                {
                    // Check if the current row matches the criterion
                    if (TimeSpan.Parse(row.Cells["TIME_OUT"].Value.ToString()) <= time1)
                    {
                        // Change the color of the row
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }//DONE 6/23/23
        public void EditStudentDetails()//BUG - AFTER EDITING THE TEXT THAT WAS PREVIOUSLY PUT DOESN'T CLEAR-- MUST BE FIX. ALSO DISABLE THE STUDENT ID EDIT. STUDENT ID IS NOT EDITABLE. // FIXED |> MARK POGI
        {
            OpenSQL();
            SqlDataAdapter name = new SqlDataAdapter("SELECT * FROM students WHERE studentid = '" + displayID.Text + "'", conn);
            ds.Clear();
            name.Fill(ds);

            string currentid = displayID.Text;
            string studentid = newStudentIDValue.Text == string.Empty ? displayID.Text : newStudentIDValue.Text;
            string fname = newFirstNameValue.Text == string.Empty ? ds.Tables[0].Rows[0]["firstName"].ToString() : newFirstNameValue.Text;
            string mname = newMiddleInitialValue.Text == string.Empty ? ds.Tables[0].Rows[0]["middleInitial"].ToString() : newMiddleInitialValue.Text;
            string lname = newLastNameValue.Text == string.Empty ? ds.Tables[0].Rows[0]["lastName"].ToString() : newLastNameValue.Text;
            string section = newSectionValue.Text == string.Empty ? displaySection.Text : newSectionValue.Text;

            SqlCommand cmd = new SqlCommand("UPDATE students SET studentid = @studentid, firstName = @firstName, middleInitial = @middleInitial, lastName = @lastName, section = @section where studentid = @currentstudentId", conn);
            cmd.Parameters.AddWithValue("@currentstudentId", currentid);
            cmd.Parameters.AddWithValue("@studentid", studentid);
            cmd.Parameters.AddWithValue("@firstName", fname);
            cmd.Parameters.AddWithValue("@middleInitial", mname);
            cmd.Parameters.AddWithValue("@lastName", lname);
            cmd.Parameters.AddWithValue("@section", section);
            cmd.ExecuteNonQuery();

            DisplayForLogs();

            oldFirstNameValue.Text = fname;
            oldLastNameValue.Text = lname;
            oldMiddleInitialValue.Text = mname;
            oldSectionValue.Text = section;

            displayName.Text = $"{lname}, {fname} {mname}.";
            displaySection.Text = section;
            displayID.Text = studentid;


            fName = fname;
            mInitial = mname;
            lName = lname;
            this.section = section;
            studentId = studentid;

            ResetValuesInInput();

            ReasonForPassword = "";
            pnlEditUser.Hide();
            ds.Clear();
            name.Fill(ds);
            CloseSQL();
        }
        #endregion

        public SitInMonitoringForm()
        {
            InitializeComponent();

            // Add design
            Design.RoundCorner(this, 25);

            TextboxBodies = new SeiyaMarxElls(tm1, tm2, l1, l2, l3, l4, l6, l7, 17);
            TextboxMargins = new SeiyaMarxElls(pass, mrg1, mrg2, mrg3, mrg4, mrg6, mrg7, borderpass, 18);
            Fifteens = new SeiyaMarxElls(pnlmrgn, pnlLoginFrame, pnlStudsRec, pnlDesign, pnlDepth, DataGrid, recordsView, pnlStudentInfo, pnlDate, 15);

            TextboxMargins.RoundCorner();
            TextboxBodies.RoundCorner();
            Fifteens.RoundCorner();

            #region Panel |> Round Corners
            Design.RoundCorner(pnlNotification, 50);
            Design.RoundCorner(pnlDateMargin, 15);
            Design.RoundCorner(pnlLoginBody, 15);
            Design.RoundCorner(pnlAdminLock, 18);
            Design.RoundCorner(pnlEditUser, 18);
            Design.RoundCorner(pnltm2, 15);
            Design.RoundCorner(pnlIn, 16);
            Design.RoundCorner(pnlPleaseWait, 40);
            Design.RoundCorner(pnlCustomReport, 16);
            Design.RoundCorner(pnlDayReport, 16);
            #endregion

            #region Edit Panel |> Round Corners
            Design.RoundCorner(m1, 18);
            Design.RoundCorner(m2, 18);
            Design.RoundCorner(m3, 18);
            Design.RoundCorner(m4, 18);
            Design.RoundCorner(m5, 18);
            Design.RoundCorner(n1, 18);
            Design.RoundCorner(n2, 18);
            Design.RoundCorner(n3, 18);
            Design.RoundCorner(n4, 18);
            Design.RoundCorner(n5, 18);
            #endregion
        }
        private void SitInMonitoringForm_Load(object sender, EventArgs e) // Form Load
        {
            // Default values on first load, may or may not change during run time
            notClicked = Color.FromArgb(210, 242, 250);
            Clicked = Color.FromArgb(65, 205, 242);
            CanStart = Color.FromArgb(7, 163, 58);
            NotStart = Color.FromArgb(65, 205, 242);
            exitApp = false;
            Update_Data();
            DisplayForLogs();
            ProcessingDataBase = false;


            System.Drawing.Point AlignInCenter(Control ctr) =>
                new System.Drawing.Point((Width / 2) - (ctr.Width / 2), (Height / 2) - (ctr.Height / 2));


            void SetPanelsToCenter(List<Control> panels)
            {
                foreach (Control panel in panels)
                {
                    panel.Location = AlignInCenter(panel);
                    panel.Hide();
                }
            }
            List<Control> MainPanels = new List<Control>() { pnlRecords, pnlEditUser, pnlAdminLock, pnlSetPrintOptions, pnlPleaseWait };
            SetPanelsToCenter(MainPanels);


            void SetPanelsForPrintSetUpPanel(List<Control> panels)
            {
                foreach (Control panel in panels)
                {
                    panel.Location = new System.Drawing.Point(999, 85);
                    panel.Hide();
                }
            }
            List<Control> controls = new List<Control>() { pnlDayReport, pnlCustomReport };
            SetPanelsForPrintSetUpPanel(controls);


            Display_Student_Info("00-0000000", "---- ----", "-", "-------", "---- - ---", 0, 0, 0);
            attemptsOfLogin = 0;
            password = "hehe";

            buttonColors = Color.FromArgb(8, 136, 194);

            BtnStart.BackColor = buttonColors;
            BtnSearch.BackColor = buttonColors;

            BtnConfirm.BackColor = Color.Black;
            BtnCancelIn.BackColor = Color.Black;
            pnlEditUser.BackColor = Color.Black;

            BtnPrint.BackColor = Color.FromArgb(0, 50, 94);
            BtnEdit.BackColor = Color.FromArgb(0, 134, 158);
            BtnDelete.BackColor = Color.FromArgb(0, 93, 130);

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

        #region _Behavior UI - Mark
        private void idNumberHasInput(object sender, EventArgs e)
        {
            BtnSearch.Enabled = true;
            BtnSearch.Text = "SEARCH";
            CheckForBadInput_In(txtStudentID, placeholder1);
        }

        #region Bad Input |> Behavior UI - Mark
        private void CheckForBadInput(object sender, KeyPressEventArgs e) =>
            e.Handled = char.IsLetter(e.KeyChar);
        private void fullNameHasInput(object sender, EventArgs e) =>
            CheckForBadInput_In(txtStudentName, placeholder2);
        private void PassHasInput(object sender, EventArgs e) =>
            CheckForBadInput_In(txtPass, placeholder3);
        private void lastnamehasinput(object sender, EventArgs e) =>
            CheckForBadInput_In(txtStudentLastName, placeholder4);
        private void sectioninput(object sender, EventArgs e) =>
            CheckForBadInput_In(txtSection, placeholder5);
        private void searchedchanged(object sender, EventArgs e) =>
            CheckForBadInput_In(txtSearchId, placeholder7);
        private void initialHasInput(object sender, EventArgs e) =>
            CheckForBadInput_In(txtMiddleInitial, placeholder8);
        private void e1(object sender, KeyPressEventArgs e) =>
            CheckForBadInput_In(newStudentIDValue, oldStudentIDValue);
        private void e2(object sender, KeyPressEventArgs e) =>
            CheckForBadInput_In(newFirstNameValue, oldFirstNameValue);
        private void e3(object sender, KeyPressEventArgs e) =>
            CheckForBadInput_In(newLastNameValue, oldLastNameValue);
        private void e4(object sender, KeyPressEventArgs e) =>
            CheckForBadInput_In(newSectionValue, oldSectionValue);
        private void e5(object sender, KeyPressEventArgs e) =>
            CheckForBadInput_In(newMiddleInitialValue, oldMiddleInitialValue);

        #endregion

        #region Clicks |> Behavior UI - Mark
        private void idClick(object sender, EventArgs e) =>
            txtStudentID.Focus();
        private void fullNameClick(object sender, EventArgs e) =>
            txtStudentName.Focus();
        private void userClick(object sender, EventArgs e) =>
            txtStudentID.Focus();
        private void userClick2(object sender, EventArgs e) =>
            txtStudentName.Focus();
        private void idClick2(object sender, EventArgs e) =>
            txtPass.Focus();
        private void passClick(object sender, EventArgs e) =>
            txtPass.Focus();
        private void lnclick(object sender, EventArgs e) =>
            txtStudentLastName.Focus();
        private void seclick(object sender, EventArgs e) =>
            txtSection.Focus();
        private void qw1(object sender, EventArgs e) =>
            txtStudentLastName.Focus();
        private void qw2(object sender, EventArgs e) =>
            txtSection.Focus();
        private void placeholder7click(object sender, EventArgs e) =>
            txtSearchId.Focus();
        private void inClick(object sender, EventArgs e) =>
            txtMiddleInitial.Focus();
        private void editIdClick(object sender, EventArgs e) =>
            newStudentIDValue.Focus();

        private void firstEditClick(object sender, EventArgs e) =>
            newFirstNameValue.Focus();

        private void LastEditClick(object sender, EventArgs e) =>
            newLastNameValue.Focus();

        private void sectionEditClick(object sender, EventArgs e) =>
            newSectionValue.Focus();

        private void midEditClick(object sender, EventArgs e) =>
            newMiddleInitialValue.Focus();
        #endregion

        private void NotificationTimerSpecific_Tick(object sender, EventArgs e)
        {

            pnlNotification.Visible = !pnlPleaseWait.Visible;
            // NOTIFICATION PANEL

            // Notification Animation go BrrRrRrrrR - Mark
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
        /// Checks for input in the given textboxes
        /// </summary>
        /// <param name="ctr"></param>
        /// <param name="placeholder"></param>
        void CheckForBadInput_In(Control ctr, Control placeholder)
        {
            if (ctr.Text == string.Empty || ctr.Text == null) { placeholder.BringToFront(); } else { placeholder.SendToBack(); }
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
            placeholder7.Visible = (mrg6.BackColor = txtSearchId.Focused ? Clicked : Color.FromArgb(0, 70, 92)) == Color.FromArgb(0, 70, 92);

            #region Textbox Behaviors |> Mark
            TextBox_Behavior(txtStudentID, mrg1, placeholder1);
            TextBox_Behavior(txtStudentName, mrg2, placeholder2);
            TextBox_Behavior(txtPass, borderpass, placeholder3);
            TextBox_Behavior(txtStudentLastName, mrg3, placeholder4);
            TextBox_Behavior(txtSection, mrg4, placeholder5);

            TextBox_Behavior(newStudentIDValue, m1, oldStudentIDValue);
            TextBox_Behavior(newFirstNameValue, m2, oldFirstNameValue);
            TextBox_Behavior(newLastNameValue, m3, oldLastNameValue);
            TextBox_Behavior(newSectionValue, m4, oldSectionValue);
            TextBox_Behavior(newMiddleInitialValue, m5, oldMiddleInitialValue);
            TextBox_Behavior(txtMiddleInitial, mrg7, placeholder8);
            #endregion

            if (CheckForBadInput_In(txtStudentID) && CheckForBadInput_In(txtStudentName) && CheckForBadInput_In(txtStudentLastName) && CheckForBadInput_In(txtMiddleInitial) && CheckForBadInput_In(txtSection))
                EnableStart();
            else
                DisableStart();
        }

        private void FormWillBeClosed(object sender, FormClosingEventArgs e)
        {
            ReasonForPassword = "exit";
            pnlAdminLock.Visible = e.Cancel = !exitApp;
        }
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            bool PasswordIsCorrect = txtPass.Text == password;
            if (attemptsOfLogin > 9 && !PasswordIsCorrect)
                MessageBox.Show($"" +
                    $"Please contact an assistant! \n" +
                    $"After ({attemptsOfLogin}), you have failed to input the correct password!",
                    "Password Incorrect!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            else
            {
                if (PasswordIsCorrect)
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
            Keys Key = e.KeyCode;
            void TabIndexCatch(Control txt, Control Btn)
            {
                if (Key == Keys.Tab && txt.Focused)
                    Btn.Focus();
            }

            if (Key == Keys.F1 && e.Control)
            {
                ReasonForPassword = "records";
                ShowAdminPasswordInput();
            }

            // Tab Switch Code - maki hekhok // HEHEHEHEHEHEHEHE MUGANA
            TabIndexCatch(txtStudentID, BtnSearch);
            TabIndexCatch(txtStudentLastName, txtStudentName);
            TabIndexCatch(txtStudentName, txtMiddleInitial);
            TabIndexCatch(txtStudentID, txtSection);
            TabIndexCatch(txtSection, BtnStart);
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
            System.Data.DataTable dt = new System.Data.DataTable();
            bool Add;
            restrict.Fill(dt);

            // Check Row Count
            if (dt.Rows.Count >= 1)
                Add = false;
            else
                Add = true;

            // Check For error
            string ErrorMessage = "";
            bool ErrorInInputIsDetected = false; // this checks overall invalid input

            bool ControlHasNullInputIn(Control txtbox) => // this specifies invalid input
                ErrorInInputIsDetected = txtbox.Text == null || txtbox.Text == "";
            //      ^^^^^INVALID INPUT DETECTED

            #region CheckForSQLInjection
            void CatchAttempt(List<Control> txt)
            {
                foreach (Control t in txt)
                    CatchSQLInjection(t);
            }
            List<Control> txts = new List<Control>() { txtStudentID, txtStudentName, txtMiddleInitial, txtStudentLastName, txtSection };
            CatchAttempt(txts);
            #endregion

            #region CheckForInvalidError |> Mark
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
            #endregion

            if (ErrorInInputIsDetected && !NoInjection)
                MessageBox.Show($"{ErrorMessage}", "INVALID INPUT DETECTED", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                try // No errors, but check for Exceptions
                {
                    DefaultEnable();
                    if (Add == true)
                        AddStudent();
                    else
                        RestrictTime();
                    NoInjection = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            CloseSQL();
        }
        private void DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) // Update data during log out
        {
            bool StudentLogsOut = e.RowIndex > -1 && (e.ColumnIndex == DataGrid.Columns["LOG_OUT"].Index);
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
                CloseSQL();
                Update_Data();
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
            CloseSQL();
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

            newStudentIDValue.Enabled = false;

            void SetDefaults(Control text)
            {
                text.BringToFront();
                text.Visible = true;
            }

            SetDefaults(oldFirstNameValue);
            SetDefaults(oldMiddleInitialValue);
            SetDefaults(oldLastNameValue);
            SetDefaults(oldSectionValue);

            ResetValuesInInput();

            ShowAdminPasswordInput();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            ReasonForPassword = "print";
            ShowAdminPasswordInput();
        }

        #region Buttons Go To |> Mark
        private void BtnSearchInRecords_Click(object sender, EventArgs e) =>
            SearchStudentAllLogs();

        private void txtSearchId_KeyPress(object sender, KeyPressEventArgs e) =>
            DisplayForLogs();

        private void StudentIdNumberEffect(object sender, EventArgs e) =>
            placeholder7.ForeColor = Color.FromArgb(0, 234, 255);

        private void StudentIdNumberEffectEnd(object sender, EventArgs e) =>
            placeholder7.ForeColor = Color.FromArgb(154, 214, 230);
        #endregion

        #region GetTimeComputation |> M
        private string GetApproximateTime(System.Data.DataSet dataTable)
        {
            double timeValue = Convert.ToDouble(dataTable.Tables[0].Rows[0]["RemainingTime"].ToString());
            double hours = Math.Floor(timeValue);
            double minutes = Math.Floor((timeValue - hours) * 60);

            return $"{hours} hours, {minutes} minutes";
        }

        private string GetApproximateTime(System.Data.DataTable dataTable) // varchar to numeric error
        {
            double timeValue = Convert.ToDouble(dataTable.Rows[0][0]);
            double hours = Math.Floor(timeValue);
            double minutes = Math.Floor((timeValue - hours) * 60);

            return $"{hours} hours, {minutes} minutes";
        }
        #endregion

        private void RecordView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (recordsView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
                    string GetRecordViewCellValueOf(string identifier) =>
                        recordsView.Rows[e.RowIndex].Cells[identifier].FormattedValue.ToString(); // Go to Expression of Value of specified identifier
                    string StudentIdInRecords() =>
                        GetRecordViewCellValueOf("lStudentId");
                    string IDSelectFrom(string table) =>
                        $"SELECT * FROM {table} WHERE studentId";

                    SqlDataAdapter c = new SqlDataAdapter($"{IDSelectFrom("sessionlogs")} = '{StudentIdInRecords()}'", conn);
                    SqlDataAdapter s = new SqlDataAdapter($"{IDSelectFrom("students")} = '{StudentIdInRecords()}'", conn);

                    dt.Clear();
                    ds.Clear();
                    s.Fill(ds);
                    c.Fill(dt);

                    // Initialize value
                    fName = GetRecordViewCellValueOf("lFirstName");
                    mInitial = GetRecordViewCellValueOf("lMiddleInitial");
                    lName = GetRecordViewCellValueOf("lLastName");
                    studentId = StudentIdInRecords();
                    section = GetRecordViewCellValueOf("lSection");

                    // Display
                    displayID.Text = studentId;
                    displayName.Text = $"{lName}, {fName} {mInitial}.";
                    displaySection.Text = section;
                    displayNumOfSitIns.Text = dt.Rows.Count.ToString();
                    displayBalance.Text = GetApproximateTime(ds);


                    // New Initialized Value 
                    oldFirstNameValue.Text = fName;
                    oldLastNameValue.Text = lName;
                    oldMiddleInitialValue.Text = mInitial;
                    oldSectionValue.Text = section;
                }
            }
            catch (Exception) {/**/}
        }

        private void dateForRecords_ValueChanged(object sender, EventArgs e) =>
            DisplayForLogs();

        private void LBLBtnSEARCH_Click(object sender, EventArgs e) =>
            SearchStudentAllLogs();

        private void BtnCancelEdit_Click(object sender, EventArgs e)
        {
            ReasonForPassword = "";
            pnlEditUser.Hide();
        }

        private void BtnConfirmEdit_Click(object sender, EventArgs e)
        {
            try
            {
                EditStudentDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region UI Behaviour NEW |> Edit Panel - Mark

        void CheckForChanges(string _old, string _new, Control changes)
        {
            bool ThereAreNewEditsInRecord() =>
                _old != _new && (_new != null && _new != string.Empty);
            changes.Visible = ThereAreNewEditsInRecord();
            if (ThereAreNewEditsInRecord())
                changes.Text = $"{_old} {arrow} {_new}";
            else
                changes.Text = "";
        }

        private void studentidTextChangedEdit(object sender, EventArgs e) =>
            CheckForChanges(oldStudentIDValue.Text, newStudentIDValue.Text, changesInfo1);

        private void firstNameTextChangedEdit(object sender, EventArgs e) =>
            CheckForChanges(oldFirstNameValue.Text, newFirstNameValue.Text, changesInfo2);

        private void lastNameTextChangedEdit(object sender, EventArgs e) =>
            CheckForChanges(oldLastNameValue.Text, newLastNameValue.Text, changesInfo3);

        private void middleInitialTextChangedEdit(object sender, EventArgs e) =>
            CheckForChanges(oldMiddleInitialValue.Text, newMiddleInitialValue.Text, changesInfo4);

        private void sectionTextChangedEdit(object sender, EventArgs e) =>
            CheckForChanges(oldSectionValue.Text, newSectionValue.Text, changesInfo5);
        #endregion

        private void ProcessDB(object sender, EventArgs e)
        {
            // Please Wait Notification after Log Out
            if (ProcessingDataBase)
            {
                pnlPleaseWait.Show();
                NotifySuccessfulLogOut();
                DataGrid.Enabled = false;
                ProcessingDataBase = false;
                DataGrid.Columns["LOG_OUT"].Visible = false;
                Thread.Sleep(1500);
            }
            else
            {
                DataGrid.Enabled = true;
                pnlPleaseWait.Hide();
                DataGrid.Columns["LOG_OUT"].Visible = true;
            }
        }

        private void dateToday_ValueChanged(object sender, EventArgs e)
        {
            Update_Data();
        }

        private void cbxSelectForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
                SEMESTRAL REPORT
                MONTH REPORT
                STUDENT SPECIFIC REPORT
                WEEK REPORT
                DAY REPORT
                CUSTOM REPORT
             */

            /* 
             * semester report
             *      name
             *      id
             *      date
             *      time in
             *      time out
             *      section
             * 
             * month report
             *      name
             *      id
             *      date
             *      time in
             *      time out
             *      section
             *      
             * student specific report
             *      name
             *      id
             *      section
             *      date
             *      time in 
             *      time out
             *      number of sit ins
             *      total time used
             *      total balance
             *      
             * week report
             *      name
             *      id
             *      section
             *      date range (July 2 - July 9)
             *      time in
             *      time out
             * 
             * day report
             *      name
             *      id
             *      section
             *      time in 
             *      time out
             *      date
             *      
             * custom report
             *      name
             *      id
             *      section
             *      time in
             *      time out
             *      date (range)
             *      number of sit ins
             *      balance
             *      total time used
             *     
             *     
             *     
             * FUNCTIONS TO ADD!!
             *      
             */

            void ShowPanelForSelected(Control pnl, string match) =>
                pnl.Visible = (cbxSelectForm.Text.ToLower() == match.ToLower());

            ShowPanelForSelected(pnlCustomReport, "custom");
            ShowPanelForSelected(pnlDayReport, "day report");
        }



        // CUSTOM REPORT BUTTON CLICKS
        private void btnPreviewCustomReport_Click(object sender, EventArgs e)
        {

        }

        private void btnPrintCustomReport_Click(object sender, EventArgs e)
        {

        }




        // DAY REPORT BUTTON CLICKS
        private void btnPreviewDayReport_Click(object sender, EventArgs e)
        {

        }

        private void btnPrintDayReport_Click(object sender, EventArgs e)
        {

        }



        // CLOSE PRINT SET UP
        private void btnClosePrintSetUp_Click(object sender, EventArgs e)
        {
            pnlSetPrintOptions.Hide();
            ReasonForPassword = "";
        }
    }


    #region Round Corner |> Mark
    class SeiyaMarxElls
    {
        List<Control> Set = new List<Control>();
        int radius;
        public SeiyaMarxElls() {/**/}
        public SeiyaMarxElls(Control ctr1, Control ctr2, Control ctr3, Control ctr4, Control ctr5, Control ctr6, Control ctr7, int radius)
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
        public SeiyaMarxElls(Control ctr1, Control ctr2, Control ctr3, Control ctr4, Control ctr5, Control ctr6, Control ctr7, Control ctr8, int radius)
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
        public SeiyaMarxElls(Control ctr1, Control ctr2, Control ctr3, Control ctr4, Control ctr5, Control ctr6, Control ctr7, Control ctr8, Control ctr9, int radius)
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
    #endregion
}
