using MySqlConnector;
using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Common.Worklist.RSTS
{
    public class Service
    {
        public bool Insert(List<Data> data)
        {
            var row = 0;

            foreach (Data item in data)
            {
                var connection = new MySqlConnection("Server=103.18.46.210;Port=6033;User ID=root;Password=y511A582pu2;Database=ris");
                connection.Open();

                var command = connection.CreateCommand();
                var str = $"INSERT INTO radorder (ordercode,trancode,patientid,patientname," +
                    $"patientbirth,orderdate,ordertime," +
                    $"doctorid,doctorname,iostatus," +
                    $"examinationid,examinationname,clinicdiag," +
                    $"additional,flag,orderstatus," +
                    $"unitcode,unitname,patientsex," +
                    $"patientaddress,no_hp,branch) " +
                    $"VALUES ('{item.ordercode}','{item.trancode}','{item.patientid}','{item.patientname}'," +
                    $"'{item.patientbirth}','{item.orderdate}','{item.ordertime}'," +
                    $"'{item.doctorid}','{item.doctorname}','{item.iostatus}'," +
                    $"'{item.examinationid}','{item.examinationname}','{item.clinicdiag}'," +
                    $"'{item.additional}','{item.flag}','{item.orderstatus}'," +
                    $"'{item.unitcode}','{item.unitname}','{item.patientsex}'," +
                    $"'{item.patientaddress}','{item.no_hp}','{item.branch}');";
                command.CommandText = str;
                row += command.ExecuteNonQuery();

                connection.Close();
            }

            return row > 0;
        }

        public bool Read(Data data)
        {
            var str = $"SELECT REPORT_TEXT FROM report WHERE ACCESSION_NUM = '{data.ordercode}' ORDER BY CONFIRM_DATE DESC LIMIT 1;";
            var connection = new MySqlConnection("Server=103.18.46.210;Port=6033;User ID=root;Password=y511A582pu2;Database=ris");
            connection.Open();
            using (var command = new MySqlCommand(str, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var text = reader.GetString(0);

                        var ids = data.ordercode.Split('_');

                        var tc = new TransChargesQuery("a");
                        var tci = new TransChargesItemQuery("b");
                        var tcic = new TransChargesItemCompQuery("c");

                        tc.Select(tc.TransactionNo, tci.ItemID, tcic.ParamedicID);
                        tc.InnerJoin(tci).On(tc.TransactionNo == tci.TransactionNo);
                        tc.InnerJoin(tcic).On(tci.TransactionNo == tcic.TransactionNo && tci.SequenceNo == tcic.SequenceNo && tcic.TariffComponentID == "01");
                        tc.Where($"<a.TransactionNo + '_' + b.SequenceNo = '{data.ordercode}'>");

                        var tbl = tc.LoadDataTable();
                        if (tbl.Rows.Count == 0) return false;

                        var result = new TestResult();
                        if (!result.LoadByPrimaryKey(ids[0], tbl.Rows[0]["ItemID"].ToString()))
                        {
                            result = new TestResult();
                            result.TransactionNo = tbl.Rows[0]["TransactionNo"].ToString();
                            result.ItemID = tbl.Rows[0]["ItemID"].ToString();
                            result.ParamedicID = tbl.Rows[0]["ParamedicID"].ToString();
                            result.TestResultDateTime = DateTime.Now;
                            result.TestResult = text;
                        }
                        else result.TestResult = text;
                        result.LastUpdateByUserID = "WEBSERVICE";
                        result.LastUpdateDateTime = DateTime.Now;
                        result.Save();

                        var detail = new TransChargesItem();
                        if (detail.LoadByPrimaryKey(ids[0], ids[1]))
                        {
                            detail.FilmNo = data.ordercode;
                            detail.Save();
                        }
                    }
                }
            }
            if (connection.State == System.Data.ConnectionState.Open) connection.Close();
            return true;
        }
    }

    public class Data
    {
        public string ordercode { get; set; }
        public string trancode { get; set; }
        public string patientid { get; set; }
        public string patientname { get; set; }
        public string patientbirth { get; set; }
        public string orderdate { get; set; }
        public string ordertime { get; set; }
        public string doctorid { get; set; }
        public string doctorname { get; set; }
        public string iostatus { get; set; }
        public string examinationid { get; set; }
        public string examinationname { get; set; }
        public string clinicdiag { get; set; }
        public string additional { get; set; }
        public string flag { get; set; }
        public string orderstatus { get; set; }
        public string unitcode { get; set; }
        public string unitname { get; set; }
        public string patientsex { get; set; }
        public string patientaddress { get; set; }
        public string no_hp { get; set; }
        public string branch { get; set; }
    }
}
