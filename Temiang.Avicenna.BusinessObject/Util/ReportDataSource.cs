using System;
using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Core;
using System.Data.SqlClient;

namespace Temiang.Avicenna.BusinessObject.Util
{
    public class ReportDataSource : esEntityCollection
    {
        protected override IMetadata Meta
        {
            get
            {
                return AppReportParameterMetadata.Meta();
            }
        }

        public DataTable GetDataTable(string programID, PrintJobParameterCollection printJobParameters)
        {
            AppProgram appProgram = new AppProgram();
            appProgram.LoadByPrimaryKey(programID);
            if (string.IsNullOrEmpty(appProgram.StoreProcedureName))
            {
                throw new Exception("Store procedure name not defined in program setting");
            }

            //esParameters parameters = new esParameters();
            //foreach (PrintJobParameter parameter in printJobParameters)
            //{
            //    if (parameter.ValueString != null && parameter.Name.Substring(0, 4) != "temp")
            //        parameters.Add(parameter.Name, parameter.ValueString);
            //    else if (parameter.ValueNumeric != null)
            //        parameters.Add(parameter.Name, parameter.ValueNumeric);
            //    else if (parameter.ValueDateTime != null)
            //        parameters.Add(parameter.Name, parameter.ValueDateTime);
            //}
            try
            {
                var table = new DataTable();
                using (SqlConnection conn = new SqlConnection(esConfigSettings.ConnectionInfo.Connections[esConfigSettings.ConnectionInfo.Default].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(appProgram.StoreProcedureName, conn))
                    {
                        cmd.CommandTimeout = conn.ConnectionTimeout;
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (PrintJobParameter parameter in printJobParameters)
                        {
                            if (parameter.ValueString != null && parameter.Name.Substring(0, 4) != "temp")
                                cmd.Parameters.Add("@" + parameter.Name, SqlDbType.VarChar).Value = parameter.ValueString;
                            else if (parameter.ValueNumeric != null)
                                cmd.Parameters.Add("@" + parameter.Name, SqlDbType.Decimal).Value = parameter.ValueNumeric;
                            else if (parameter.ValueDateTime != null)
                                cmd.Parameters.Add("@" + parameter.Name, SqlDbType.DateTime).Value = parameter.ValueDateTime;
                        }

                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(table);
                        }
                    }
                }

                //DataTable dtb = FillDataTable(esQueryType.StoredProcedure, appProgram.StoreProcedureName, parameters);
                return table;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return null;
        }

        public DataTable GetDataTable(string programID, PrintJobParameter printJobParameter)
        {
            AppProgram appProgram = new AppProgram();
            appProgram.LoadByPrimaryKey(programID);
            if (string.IsNullOrEmpty(appProgram.StoreProcedureName))
            {
                throw new Exception("Store procedure name not defined in program setting");
            }

            //esParameters parameters = new esParameters();
            //if (printJobParameter.ValueString != null && printJobParameter.Name.Substring(0, 4) != "temp")
            //    parameters.Add(printJobParameter.Name, printJobParameter.ValueString);
            //else if (printJobParameter.ValueNumeric != null)
            //    parameters.Add(printJobParameter.Name, printJobParameter.ValueNumeric);
            //else if (printJobParameter.ValueDateTime != null)
            //    parameters.Add(printJobParameter.Name, printJobParameter.ValueDateTime);
            try
            {
                //DataTable dtb = FillDataTable(esQueryType.StoredProcedure, appProgram.StoreProcedureName, parameters);
                //return dtb;

                var table = new DataTable();

                using (SqlConnection conn = new SqlConnection(esConfigSettings.ConnectionInfo.Connections[esConfigSettings.ConnectionInfo.Default].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(appProgram.StoreProcedureName, conn))
                    {
                        cmd.CommandTimeout = conn.ConnectionTimeout;
                        cmd.CommandType = CommandType.StoredProcedure;
                        //foreach (PrintJobParameter parameter in printJobParameters)
                        //{
                        if (printJobParameter.ValueString != null && printJobParameter.Name.Substring(0, 4) != "temp")
                            cmd.Parameters.Add("@" + printJobParameter.Name, SqlDbType.VarChar).Value = printJobParameter.ValueString;
                        else if (printJobParameter.ValueNumeric != null)
                            cmd.Parameters.Add("@" + printJobParameter.Name, SqlDbType.Decimal).Value = printJobParameter.ValueNumeric;
                        else if (printJobParameter.ValueDateTime != null)
                            cmd.Parameters.Add("@" + printJobParameter.Name, SqlDbType.DateTime).Value = printJobParameter.ValueDateTime;
                        //}

                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(table);
                        }
                    }
                }

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return null;
        }

        public DataTable GetDataTableDirect(string storeProcedureName, PrintJobParameterCollection printJobParameters)
        {
            //esParameters parameters = new esParameters();
            //foreach (PrintJobParameter parameter in printJobParameters)
            //{
            //    if (parameter.ValueString != null)
            //        parameters.Add(parameter.Name, parameter.ValueString);
            //    else if (parameter.ValueNumeric != null)
            //        parameters.Add(parameter.Name, parameter.ValueNumeric);
            //    else if (parameter.ValueDateTime != null)
            //        parameters.Add(parameter.Name, parameter.ValueDateTime);
            //}
            try
            {
                //DataTable dtb = FillDataTable(esQueryType.StoredProcedure, storeProcedureName, parameters);
                //return dtb;

                var table = new DataTable();

                using (SqlConnection conn = new SqlConnection(esConfigSettings.ConnectionInfo.Connections[esConfigSettings.ConnectionInfo.Default].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storeProcedureName, conn))
                    {
                        cmd.CommandTimeout = conn.ConnectionTimeout;
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (PrintJobParameter parameter in printJobParameters)
                        {
                            if (parameter.ValueString != null && parameter.Name.Substring(0, 4) != "temp")
                                cmd.Parameters.Add("@" + parameter.Name, SqlDbType.VarChar).Value = parameter.ValueString;
                            else if (parameter.ValueNumeric != null)
                                cmd.Parameters.Add("@" + parameter.Name, SqlDbType.Decimal).Value = parameter.ValueNumeric;
                            else if (parameter.ValueDateTime != null)
                                cmd.Parameters.Add("@" + parameter.Name, SqlDbType.DateTime).Value = parameter.ValueDateTime;
                        }

                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(table);
                        }
                    }
                }

                return table;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataTable DataKeadaanMorbiditasRawatInap(DateTime FromDate, DateTime ToDate)
        {
            esParameters par = new esParameters();
            par.Add("p_FromDate", FromDate);
            par.Add("p_ToDate", ToDate);
            return FillDataTable(esQueryType.StoredProcedure,
            "UspRptDataKeadaanMorbiditasRawatInap", par);
        }

        public DataTable DataKeadaanMorbiditasRawatJalan(DateTime FromDate, DateTime ToDate)
        {
            esParameters par = new esParameters();
            par.Add("p_FromDate", FromDate);
            par.Add("p_ToDate", ToDate);
            return FillDataTable(esQueryType.StoredProcedure,
            "UspRptDataKeadaanMorbiditasRawatJalan", par);
        }

        public DataTable LaporanGeografiKunjungan(DateTime FromDate, DateTime ToDate)
        {
            esParameters par = new esParameters();
            par.Add("p_FromDate", FromDate);
            par.Add("p_ToDate", ToDate);
            return FillDataTable(esQueryType.StoredProcedure,
            "UspRptLaporanGeografiKunjungan", par);
        }

        public DataTable LaporanGeografiPenyakit(DateTime FromDate, DateTime ToDate)
        {
            esParameters par = new esParameters();
            par.Add("p_FromDate", FromDate);
            par.Add("p_ToDate", ToDate);
            return FillDataTable(esQueryType.StoredProcedure,
            "UspRptLaporanGeografiPenyakit", par);
        }

        public DataTable IdentiasPasien(string MedicalNo)
        {
            esParameters par = new esParameters();
            par.Add("@p_MedicalNo", MedicalNo);
            return FillDataTable(esQueryType.StoredProcedure,
            "usprptIdentiasPasien", par);
        }

        public DataTable Kwitansi(string TransactionNo)
        {
            esParameters par = new esParameters();
            par.Add("p_TransactionNo", TransactionNo);
            return FillDataTable(esQueryType.StoredProcedure,
            "usprptKwitansi", par);
        }

        public DataTable LaporanJumlahPenyakitTerbanyak(DateTime FromDate, DateTime ToDate)
        {
            esParameters par = new esParameters();
            par.Add("p_FromDate", FromDate);
            par.Add("p_ToDate", ToDate);
            return FillDataTable(esQueryType.StoredProcedure,
            "usrptLaporanJumlahPenyakitTerbanyak", par);
        }

        public DataTable NotaResep(string PrescriptionNo)
        {
            esParameters par = new esParameters();
            par.Add("@p_PrescriptionNo", PrescriptionNo);
            return FillDataTable(esQueryType.StoredProcedure,
            "UspRptNotaResep", par);
        }

        public DataTable SlipPendaftaran(string RegistrationNo)
        {
            esParameters par = new esParameters();
            par.Add("@p_RegistrationNo", RegistrationNo);
            return FillDataTable(esQueryType.StoredProcedure,
            "UspRptSlipPendaftaran", par);
        }
    }
}