using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.Reporting.Drawing;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment
{
    class Utils
    {
        public static void PopulateKeadaanUmum(PatientHealthRecordLineCollection phrlColl,
            Telerik.Reporting.TextBox tekananDarah,
            Telerik.Reporting.TextBox temperature,
            Telerik.Reporting.TextBox respiratoryRate,
            Telerik.Reporting.TextBox heartRate,
            Telerik.Reporting.TextBox weight,
            Telerik.Reporting.TextBox height
            )
        {
            // sistole diastole dijadi satu
            Fill(phrlColl, tekananDarah, new string[] { "VIT.SGN.01", "VIT.SGN.02" }, "/");
            Fill(phrlColl, temperature, "VIT.SGN.05");
            Fill(phrlColl, respiratoryRate, "VIT.SGN.04");
            Fill(phrlColl, heartRate, "VIT.SGN.03");
            Fill(phrlColl, weight, "GEN.SGN.02");
            Fill(phrlColl, height, "GEN.SGN.01");
        }

        public static void PopulatePengkajianDokter(RegistrationInfoMedic rim, BusinessObject.Registration reg,
            Telerik.Reporting.TextBox txtAnamnesis,
            Telerik.Reporting.TextBox txtPhysicalExam,
            Telerik.Reporting.TextBox txtPlanning,
            Telerik.Reporting.TextBox txtDiagnose,
            Telerik.Reporting.CheckBox chkDirujukKe,
            Telerik.Reporting.CheckBox chkKonsulKe,
            Telerik.Reporting.CheckBox chkPulang,
            Telerik.Reporting.CheckBox chkRawatInap
            )
        {
            txtAnamnesis.Value = rim.Info1;

            if (txtPhysicalExam != null) // PhysicalExam bisa jadi dari jsonField dan tidak diisi disini
                txtPhysicalExam.Value = rim.Info2;

            txtPlanning.Value = rim.Info4;
            txtDiagnose.Value = rim.Info3;

            var dischargeMethod = reg.SRDischargeMethod;
            if (string.IsNullOrEmpty(dischargeMethod))
                dischargeMethod = "EMPTY"; //Jika kosong fungsi Contains akan mengembalikan nilai true

            // Dirujuk
            //E09 RUJUK RS LAIN - ALAT (-)
            //E11 RUJUK - ATAS PERMINTAAN PASIEN
            //E12 RUJUK - TEMPAT PENUH
            chkDirujukKe.Value = "E09_E11_E12".Contains(dischargeMethod);

            // KONSUL KE SPESIALIS 
            chkKonsulKe.Value = dischargeMethod == "O01" || dischargeMethod == "E01"; //O01 -> Out Patient, E01 -> Emergency

            // Pulang
            // Emergency
            //E03	DIIZINKAN PULANG
            //E04	PULANG PAKSA
            // Outpatient
            //O04 DIIZINKAN PULANG
            //O05 PULANG PAKSA
            chkPulang.Value = "E03_E04_O04_O05".Contains(dischargeMethod);


            // Dirawat
            //O02	RAWAT INAP VIA REGISTRASI
            //O03	RAWAT INAP VIA IGD
            //E10 KEMBALI KE RAWAT INAP
            //E02 DIRAWAT (MRS)
            chkRawatInap.Value = "O02_O03_E10_E02".Contains(dischargeMethod);
        }

        public static string PatientAllergy(string patientID)
        {
            return BusinessObject.PatientAllergy.DrugAndFoodAllergy(patientID);

        }

        public static DataTable GetDataSource(string transactionNo, string registrationNo,
            string QuestionFormID, string assDateTime, ref bool hideSignature, string UserID)
        {
            var dt = NursingDiagnosaTransDT.ReportDataSourceGeneral(
                   transactionNo, registrationNo, QuestionFormID
                   );

            CustomRSCH(dt, QuestionFormID, assDateTime, ref hideSignature, UserID);

            return dt;
        }

        public static void crosstab_ItemDataBound(object sender, EventArgs e, ref List<int> CTColWidth)
        {
            var ct = (Telerik.Reporting.Processing.Table)sender;

            if (CTColWidth.Count > 0)
            {
                Unit Selisih = new Unit(0);
                for (var i = 1; i <= CTColWidth.Count; i++)
                {
                    if (CTColWidth[i - 1] > 0)
                    {
                        //ct.Columns[i].ReportItem.Width = new Unit(CTColWidth[i]);
                        for (var j = 0; j < ct.Rows.Count; j++)
                        {
                            Unit tSel = new Unit(0);
                            if (j == 0)
                                tSel = ct.Columns[i].GetCell(j).Item.Width;

                            ct.Columns[i].GetCell(j).Item.Width = new Unit(CTColWidth[i - 1]);

                            if (j == 0)
                                Selisih = Selisih + ct.Columns[i].GetCell(j).Item.Width - tSel;
                        }
                    }
                }
                ct.Width = ct.Width + Selisih;
            }
            CTColWidth.Clear();
        }


        //public static void SetTableSource(Telerik.Reporting.Processing.DetailSection section,
        //    Telerik.Reporting.Processing.Table crosstbl, Telerik.Reporting.Processing.Table tbl, ref List<int> CTColWidth)
        //{
        //    //var rpt = (GeneralExamSubReport)sender;
        //    var SRAnswerType = section.DataObject["SRAnswerType"].ToString();
        //    var TextToDisplay = section.DataObject["TextToDisplay"].ToString();
        //    var oTblPrintAsGroup = section.DataObject["TblPrintAsGroup"] ?? string.Empty;
        //    var TblPrintAsGroup = oTblPrintAsGroup.ToString();

        //    //var QuestionText = section.DataObject["QuestionText"].ToString();

        //    switch (SRAnswerType)
        //    {
        //        case "TBL":
        //            {
        //                if (TblPrintAsGroup == "" || TblPrintAsGroup == "0")
        //                {
        //                    var cListW = section.DataObject["QuestionAnswerSelectionID"].ToString().Split('|');
        //                    var cList = new System.Collections.Generic.List<string>();
        //                    CTColWidth.Clear(); int cNo = 0;
        //                    foreach (var c in cListW)
        //                    {
        //                        cNo++;
        //                        var ls = c.Split(':');
        //                        cList.Add(ls[0] + "_" + cNo.ToString());
        //                        if (ls.Length > 1)
        //                        {
        //                            if (Temiang.Avicenna.Common.Helper.IsNumeric(ls[1]))
        //                            {
        //                                CTColWidth.Add(System.Convert.ToInt16(ls[1]));
        //                            }
        //                            else
        //                            {
        //                                CTColWidth.Add(50);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            CTColWidth.Add(50);
        //                        }
        //                    }
        //                    //var rCount = System.Convert.ToInt16(section.DataObject["AnswerWidth"]);

        //                    // create table source
        //                    DataTable dstbl = new DataTable();
        //                    dstbl.Columns.Add("No", typeof(int));
        //                    dstbl.Columns.Add("ColName", typeof(string));
        //                    dstbl.Columns.Add("Content", typeof(string));

        //                    // fill table
        //                    var tblValues = TextToDisplay.ToString().Split('|');
        //                    int rno = 0;
        //                    int cLen = cList.Count;
        //                    string lastGroupName = "";
        //                    int lastNo = 0;
        //                    //int unique = 0;
        //                    foreach (var ans in tblValues)
        //                    {
        //                        var row = dstbl.NewRow();
        //                        if (string.IsNullOrEmpty(TblPrintAsGroup) || TblPrintAsGroup.ToLower().Trim() == "0")
        //                        {
        //                            row["No"] = (System.Convert.ToInt16(rno / cLen)) + 1;
        //                        }
        //                        else
        //                        {
        //                            var colName = (cList[rno % cLen].Split('_'))[0];
        //                            var groupName = ans;
        //                            if (colName.ToLower().Trim() == "kriteria")
        //                            {
        //                                if (groupName != lastGroupName)
        //                                {
        //                                    lastGroupName = groupName;
        //                                    lastNo++;
        //                                }
        //                            }
        //                            else
        //                            {

        //                            }
        //                            row["No"] = lastNo;
        //                        }
        //                        row["ColName"] = cList.IndexOf(cList[rno % cLen]).ToString() + "_" + (cList[rno % cLen].Split('_'))[0];
        //                        //row["ColName"] = Array.IndexOf(cList, cList[rno % cLen]).ToString() + "_" + cList[rno % cLen];
        //                        row["Content"] = ans;// +unique.ToString();
        //                        dstbl.Rows.Add(row);
        //                        rno++;
        //                        //unique++;
        //                    }

        //                    crosstbl.DataSource = dstbl;
        //                    if (tbl != null) tbl.Visible = false;
        //                }
        //                else
        //                {
        //                    if (tbl != null)
        //                    {
        //                        var cListW = section.DataObject["QuestionAnswerSelectionID"].ToString().Split('|');
        //                        var dstbl = NursingDiagnosaTransDT.ParsingStrToDatatableForReport(TextToDisplay, cListW.Count());
        //                        dstbl.Columns.Add("No", typeof(int));
        //                        int rowNo = 0; string lastContent = "xxxxxxxxx";
        //                        foreach (DataRow dr in dstbl.Rows)
        //                        {
        //                            if (dr["c1"].ToString() != lastContent)
        //                            {
        //                                rowNo++;
        //                            }
        //                            dr["No"] = rowNo;
        //                        }

        //                        tbl.DataSource = dstbl;
        //                    }
        //                    crosstbl.Visible = false;
        //                }

        //                break;
        //            }
        //        default:
        //            {
        //                crosstbl.Visible = false;
        //                if (tbl != null) tbl.Visible = false;
        //                break;
        //            }
        //    }
        //}

        public static Telerik.Reporting.Drawing.SizeU CalculateSize(Telerik.Reporting.Processing.TextBox textBox, object o)
        {
            double padLen = 0;
            padLen = ((o == null) ? 0 : System.Convert.ToDouble(o));

            var w = textBox.Width;
            var l = textBox.Left;
            var h = textBox.Height;
            w = w.Add(new Unit(-padLen * 10));
            var obj = new Telerik.Reporting.Drawing.SizeU(w, h);
            return obj;
        }

        public static Unit CalculateLeft(object o)
        {
            double padLen = 0;
            padLen = ((o == null) ? 0 : System.Convert.ToDouble(o));
            return (new Unit(padLen * 10));
        }

        #region Custom RSCH
        private static void CustomRSCH(DataTable dt, string QuestionFormID, string assDateTime, ref bool hideSignature, string UserID)
        {
            var rs = Healthcare.GetHealthcare(); 
            var rsInit = new AppParameter(); rsInit.LoadByPrimaryKey("HealthcareInitial");
            if (rsInit.ParameterValue != "RSCH") return;

            var usr = new AppUser();
            var usrname = string.Empty;
            if (usr.LoadByPrimaryKey(UserID)) usrname = usr.UserName;

            #region PKJRJUMUM1
            // khusus pengkajian rawat jalan maunya tanda tangan perawat diatas tulisan diagnosa
            if (QuestionFormID == "PKJRJUMUM1")
            {
                var rxs = dt.AsEnumerable().Where(x => x.Field<string>("QuestionID") == "RJ.E.005");
                if (rxs.Count() > 0)
                {
                    hideSignature = true;

                    var rx = rxs.First();
                    var rno = dt.Rows.IndexOf(rx);

                    var sGroup = FindLatesAvailableGroup(dt, rno);
                    var iRn = rno + 1; var level = 19;
                    // space 
                    var r0 = dt.NewRow(); r0["QuestionID"] = "r0"; r0["TextToDisplay"] = "";
                    r0["QuestionLevel"] = level; r0["QuestionGroupName"] = sGroup; r0["SRAnswerType"] = "-";
                    dt.Rows.InsertAt(r0, iRn); iRn++;
                    // city
                    var r1 = dt.NewRow(); r1["QuestionID"] = "r1"; r1["TextToDisplay"] = rs.City + ", " + assDateTime;
                    r1["QuestionLevel"] = level; r1["QuestionGroupName"] = sGroup; r1["SRAnswerType"] = "-";
                    dt.Rows.InsertAt(r1, iRn); iRn++;
                    // nurse  
                    var r2 = dt.NewRow(); r2["QuestionID"] = "r2"; r2["TextToDisplay"] = "Perawat yang mengkaji,";
                    r2["QuestionLevel"] = level; r2["QuestionGroupName"] = sGroup; r2["SRAnswerType"] = "-";
                    dt.Rows.InsertAt(r2, iRn); iRn++;
                    // space 
                    var r3 = dt.NewRow(); r3["QuestionID"] = "r3"; r3["TextToDisplay"] = "";
                    r3["QuestionLevel"] = level; r3["QuestionGroupName"] = sGroup; r3["SRAnswerType"] = "-";
                    dt.Rows.InsertAt(r3, iRn); iRn++;
                    // space 
                    var r4 = dt.NewRow(); r4["QuestionID"] = "r4"; r4["TextToDisplay"] = "";
                    r4["QuestionLevel"] = level; r4["QuestionGroupName"] = sGroup; r4["SRAnswerType"] = "-";
                    dt.Rows.InsertAt(r4, iRn); iRn++;
                    // space
                    var r5 = dt.NewRow(); r5["QuestionID"] = "r5"; r5["TextToDisplay"] = "";
                    r5["QuestionLevel"] = level; r5["QuestionGroupName"] = sGroup; r5["SRAnswerType"] = "-";
                    dt.Rows.InsertAt(r5, iRn); iRn++;
                    // username area 
                    var r6 = dt.NewRow(); r6["QuestionID"] = "r6"; r6["TextToDisplay"] = string.Format("({0})", usrname);
                    r6["QuestionLevel"] = level; r6["QuestionGroupName"] = sGroup; r6["SRAnswerType"] = "-";
                    dt.Rows.InsertAt(r6, iRn); iRn++;
                }
            }
            #endregion

            #region BAYIKB
            if (QuestionFormID == "BAYIKB")
            {
                var rxs = dt.AsEnumerable().Where(x => x.Field<string>("QuestionID") == "BY.P.PKL");
                if (rxs.Count() > 0)
                {
                    //hideSignature = true;

                    var rx = rxs.First();
                    var rno = dt.Rows.IndexOf(rx);

                    var sGroup = FindLatesAvailableGroup(dt, rno);
                    var iRn = rno + 1; var level = 19;
                    // space 
                    var r0 = dt.NewRow(); r0["QuestionID"] = "r0"; r0["TextToDisplay"] = "";
                    r0["QuestionLevel"] = level; r0["QuestionGroupName"] = sGroup; r0["SRAnswerType"] = "-";
                    dt.Rows.InsertAt(r0, iRn); iRn++;
                    // city
                    var r1 = dt.NewRow(); r1["QuestionID"] = "r1"; r1["TextToDisplay"] = rs.City + ", " + assDateTime;
                    r1["QuestionLevel"] = level; r1["QuestionGroupName"] = sGroup; r1["SRAnswerType"] = "-";
                    dt.Rows.InsertAt(r1, iRn); iRn++;
                    // nurse  
                    var r2 = dt.NewRow(); r2["QuestionID"] = "r2"; r2["TextToDisplay"] = "Perawat/Bidan yang mengkaji Di Kamar Bersalin,";
                    r2["QuestionLevel"] = level; r2["QuestionGroupName"] = sGroup; r2["SRAnswerType"] = "-";
                    dt.Rows.InsertAt(r2, iRn); iRn++;
                    // space 
                    var r3 = dt.NewRow(); r3["QuestionID"] = "r3"; r3["TextToDisplay"] = "";
                    r3["QuestionLevel"] = level; r3["QuestionGroupName"] = sGroup; r3["SRAnswerType"] = "-";
                    dt.Rows.InsertAt(r3, iRn); iRn++;
                    // space 
                    var r4 = dt.NewRow(); r4["QuestionID"] = "r4"; r4["TextToDisplay"] = "";
                    r4["QuestionLevel"] = level; r4["QuestionGroupName"] = sGroup; r4["SRAnswerType"] = "-";
                    dt.Rows.InsertAt(r4, iRn); iRn++;
                    // space
                    var r5 = dt.NewRow(); r5["QuestionID"] = "r5"; r5["TextToDisplay"] = "";
                    r5["QuestionLevel"] = level; r5["QuestionGroupName"] = sGroup; r5["SRAnswerType"] = "-";
                    dt.Rows.InsertAt(r5, iRn); iRn++;
                    // username area 
                    var r6 = dt.NewRow(); r6["QuestionID"] = "r6"; r6["TextToDisplay"] = "(______________________)";
                    r6["QuestionLevel"] = level; r6["QuestionGroupName"] = sGroup; r6["SRAnswerType"] = "-";
                    dt.Rows.InsertAt(r6, iRn); iRn++;
                }
            }
            #endregion

            dt.AcceptChanges();
        }

        private static string FindLatesAvailableGroup(DataTable dt, int rno)
        {
            var group = ""; rno++;
            do
            {
                rno--;
                group = dt.Rows[rno].Field<string>("QuestionGroupName");
            } while (rno >= 0 && dt.Rows[rno].Field<string>("TextToDisplay") == "DELETE");
            return group;
        }
        #endregion

        #region RSMP
        public static void FillCheckBox(PatientHealthRecordLineCollection pColl, Telerik.Reporting.CheckBox checkBox, string questionID, bool isRefreshText=true)
        {
            // Hanya untuk SRAnswerType CHK
            try
            {
                var q = pColl.Where(x => questionID.Equals(x.QuestionID)).FirstOrDefault();
                if (isRefreshText)
                {
                    var quest = new Question();
                    quest.LoadByPrimaryKey(questionID);
                    checkBox.Text = quest.QuestionText;
                }
                checkBox.Value = q.QuestionAnswerText.Equals("1");
            }
            catch
            {
                checkBox.Value = false;
            }
        }

        public static void FillCheckBoxAndText(PatientHealthRecordLineCollection pColl, Telerik.Reporting.CheckBox checkBox,Telerik.Reporting.TextBox textBox,
            string questionID, string checkedValue="1", string selectionID="", string lineID="")

        {
            // Hanya untuk SRAnswerType CBT (ComboBox and Text)
            try
            {
                var q = pColl.Where(x => questionID.Equals(x.QuestionID)).FirstOrDefault();
                if (!string.IsNullOrEmpty(selectionID) && !string.IsNullOrEmpty(lineID))
                {
                    var quest = new QuestionAnswerSelectionLine();
                    quest.LoadByPrimaryKey(selectionID, lineID);
                    checkBox.Text = quest.QuestionAnswerSelectionLineText;
                }
                var values = q.QuestionAnswerText.Split('|');
                checkBox.Value = q.QuestionAnswerSelectionLineID.Equals(checkedValue);
                textBox.Value = values[1];
            }
            catch
            {
                checkBox.Value = false;
            }
        }
        public static void FillCheckBox(PatientHealthRecordLineCollection pColl, Telerik.Reporting.CheckBox checkBox,
    string questionID, string checkedSelectionLineID, string selectionID="", string lineID="")
        {
            // Hanya untuk SRAnswerType CBO (ComboBox)
            try
            {
                var q = pColl.Where(x => questionID.Equals(x.QuestionID)).FirstOrDefault();
                if (!string.IsNullOrEmpty(selectionID) && !string.IsNullOrEmpty(lineID))
                {
                    var quest = new QuestionAnswerSelectionLine();
                    quest.LoadByPrimaryKey(selectionID, lineID);
                    checkBox.Text = quest.QuestionAnswerSelectionLineText;
                }
                checkBox.Value = q.QuestionAnswerSelectionLineID.Equals(checkedSelectionLineID);
            }
            catch
            {
                checkBox.Value = false;
            }
        }


        public static void FillCheckBox(PatientHealthRecordLineCollection pColl, Telerik.Reporting.TextBox txtForLabel, Telerik.Reporting.TextBox txtForCheckBox, string questionID)
        {
            try
            {
                var q = pColl.Where(x => questionID.Equals(x.QuestionID)).FirstOrDefault();
                var quest = new Question();
                quest.LoadByPrimaryKey(questionID);
                txtForLabel.Value = quest.QuestionText;
                FillCheckBox(txtForCheckBox, q.QuestionAnswerText.Equals("1"));
            }
            catch
            {
                txtForLabel.Value = "ERR";
                FillCheckBox(txtForCheckBox, false);
            }
        }

        public static void FillCheckBox(Telerik.Reporting.TextBox TextBox00, Telerik.Reporting.TextBox TextBox01, bool IsChecked)
        {
            FillCheckBox(TextBox00, IsChecked);
            FillCheckBox(TextBox01, IsChecked);
        }
        public static void FillCheckBox(Telerik.Reporting.TextBox TextBox00, bool IsChecked)
        {
            TextBox00.Value = IsChecked?"✓":string.Empty;
        }

        public static void Fill(PatientHealthRecordLineCollection pColl, Telerik.Reporting.TextBox TextBox0, string QuestionID)
        {
            Fill(pColl, TextBox0, new string[] { QuestionID }, string.Empty);
        }
        public static void Fill(PatientHealthRecordLineCollection pColl, Telerik.Reporting.TextBox TextBox0, string[] QuestionID, string Separator)
        {
            try
            {
                TextBox0.Value = string.Empty;
                var suffix = string.Empty;
                var qs = pColl.Where(x => QuestionID.Contains(x.QuestionID));
                foreach (var q in qs)
                {
                    TextBox0.Value += (TextBox0.Value.Equals(string.Empty) ? string.Empty : Separator) +
                        (q.QuestionAnswerNum.HasValue ? q.QuestionAnswerNum.Value.ToString("G29") : q.QuestionAnswerText);
                    suffix = q.QuestionAnswerSuffix;
                }
                TextBox0.Value += suffix;
            }
            catch
            {
                TextBox0.Value = "ERR";
            }
        }
        #endregion
    }
}
