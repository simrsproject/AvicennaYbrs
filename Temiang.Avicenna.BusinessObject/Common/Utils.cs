using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Temiang.Dal.Core;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using System.Data.SqlClient;
using System.Data;

namespace Temiang.Avicenna.BusinessObject.Common
{
    public static class Utils
    {
        const string AssemblyName = "Temiang.Avicenna.BusinessObject";
        static public esEntityWAuditLog GetEntity(Enums.EntityClassName entityClassName)
        {
            return GetEntity(entityClassName.ToString());
        }
        static public esEntityWAuditLog GetEntity(string entityClassName)
        {
            var entityType =
                Type.GetType(string.Concat(AssemblyName, ".", entityClassName, ", ", AssemblyName));
            return (esEntityWAuditLog)Activator.CreateInstance(entityType);
        }
        static public esDynamicQuery GetEntityQuery(string entityClassName)
        {
            var entityType =
                Type.GetType(string.Concat(AssemblyName, ".", entityClassName, "Query, ", AssemblyName));
            return (esDynamicQuery)Activator.CreateInstance(entityType);
        }
        public static string GetValidDefaulValue(esColumnMetadata metadata)
        {
            string defaulValue = metadata.Default.Substring(2);
            defaulValue = defaulValue.Substring(0, defaulValue.Length - 2);
            return defaulValue;
        }

        public static void PopulateCommonField(this esEntity entity)
        {
            DateTime? timeNow = null;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                string[] excludeTableNames = { "TransPayment", "JournalTransactions" };
                if (excludeTableNames.Contains(entity.es.Source)) return;

                if (entity.es.IsAdded)
                {
                    // Check null value untuk kasus data yg diimport
                    // CreatedByUserID / CreateByUserID
                    if (entity.es.Meta.Columns.FindByPropertyName("CreatedByUserID") != null && entity.GetColumn("CreatedByUserID") == DBNull.Value)
                    {
                        try
                        {
                            entity.SetColumn("CreatedByUserID", HttpContext.Current.Session["_UserLogin"] != null ? ((UserLogin)HttpContext.Current.Session["_UserLogin"]).UserID : "WEBSERVICE");
                        }
                        catch (Exception)
                        {
                            entity.SetColumn("CreatedByUserID", "WEBSERVICE");
                        }

                    }
                    else if (entity.es.Meta.Columns.FindByPropertyName("CreateByUserID") != null && entity.GetColumn("CreateByUserID") == DBNull.Value)
                    {
                        try
                        {
                            entity.SetColumn("CreateByUserID", HttpContext.Current.Session["_UserLogin"] != null ? ((UserLogin)HttpContext.Current.Session["_UserLogin"]).UserID : "WEBSERVICE");
                        }
                        catch (Exception)
                        {
                            entity.SetColumn("CreateByUserID", "WEBSERVICE");
                        }

                    }

                    // CreatedDateTime / CreateDateTime
                    if (entity.es.Meta.Columns.FindByPropertyName("CreatedDateTime") != null && entity.GetColumn("CreatedDateTime") == DBNull.Value)
                    {
                        timeNow = (new DateTime()).NowAtSqlServer();
                        entity.SetColumn("CreatedDateTime", timeNow);
                    }
                    else if (entity.es.Meta.Columns.FindByPropertyName("CreateDateTime") != null && entity.GetColumn("CreateDateTime") == DBNull.Value)
                    {
                        timeNow = (new DateTime()).NowAtSqlServer();
                        entity.SetColumn("CreateDateTime", timeNow);
                    }
                }


                // LastUpdateByUserID / LastUpdateDateTime
                if (entity.es.Meta.Columns.FindByPropertyName("LastUpdateByUserID") != null)
                {
                    if (entity.es.IsAdded || entity.GetColumn("LastUpdateByUserID") == DBNull.Value
                        || (entity.es.IsModified && entity.GetColumn("LastUpdateByUserID").Equals(entity.GetOriginalColumnValue("LastUpdateByUserID"))))
                    {
                        try
                        {
                            entity.SetColumn("LastUpdateByUserID",
                                ((UserLogin)HttpContext.Current.Session["_UserLogin"]).UserID != null
                                    ? ((UserLogin)HttpContext.Current.Session["_UserLogin"]).UserID
                                    : "WEBSERVICE");
                        }
                        catch (Exception)
                        {
                            entity.SetColumn("LastUpdateByUserID", "WEBSERVICE");
                        }
                    }
                }

                if (entity.es.Meta.Columns.FindByPropertyName("LastUpdateDateTime") != null)
                {
                    if (entity.es.IsAdded || entity.GetColumn("LastUpdateDateTime") == DBNull.Value
                        || (entity.es.IsModified && entity.GetColumn("LastUpdateDateTime").Equals(entity.GetOriginalColumnValue("LastUpdateDateTime"))))
                    {
                        entity.SetColumn("LastUpdateDateTime", timeNow ?? (new DateTime()).NowAtSqlServer());
                    }
                }
            }
        }


        public static DateTime NowAtSqlServer()
        {
            var util = new Dal.Core.esUtility();
            var dt = new DateTime();
            using (var reader = util.ExecuteReader(esQueryType.Text, "SELECT GetDate()"))
            {
                while (reader.Read())
                    dt = reader.GetDateTime(0);
            }
            return dt;
        }

        public static DataTable LoadDataTable(string cmdText)
        {
            try
            {
                var table = new DataTable();
                using (SqlConnection conn = new SqlConnection(esConfigSettings.ConnectionInfo.Connections[esConfigSettings.ConnectionInfo.Default].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandTimeout = conn.ConnectionTimeout;
                        cmd.CommandType = System.Data.CommandType.Text;
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

        public static DataTable LoadDataTableFromStoreProcedure(string storedProcedure, esParameters pars, int commandTimeout)
        {
            try
            {
                var table = new DataTable();
                using (SqlConnection conn = new SqlConnection(esConfigSettings.ConnectionInfo.Connections[esConfigSettings.ConnectionInfo.Default].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                    {
                        cmd.CommandTimeout = commandTimeout == 0 ? conn.ConnectionTimeout : commandTimeout;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        if (pars != null)
                        {
                            foreach (esParameter parameter in pars)
                            {
                                SqlParameter sqlParameter = cmd.Parameters.AddWithValue(Delimiters.Param + parameter.Name, parameter.Value);
                                switch (parameter.Direction)
                                {
                                    case esParameterDirection.InputOutput:
                                        sqlParameter.Direction = ParameterDirection.InputOutput;
                                        continue;
                                    case esParameterDirection.Output:
                                        sqlParameter.Direction = ParameterDirection.Output;
                                        sqlParameter.DbType = parameter.DbType;
                                        sqlParameter.Size = parameter.Size;
                                        sqlParameter.Scale = parameter.Scale;
                                        sqlParameter.Precision = parameter.Precision;
                                        continue;
                                    case esParameterDirection.ReturnValue:
                                        sqlParameter.Direction = ParameterDirection.ReturnValue;
                                        continue;
                                    default:
                                        continue;
                                }
                            }

                        }
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(table);

                            //if (!(cmd.Parameters.Count <= 0 || pars == null || pars.Count <= 0))
                            //{
                            //    var parameters = new esParameters();
                            //    foreach (esParameter parameter1 in pars)
                            //    {
                            //        if (parameter1.Direction != esParameterDirection.Input)
                            //        {
                            //            parameters.Add(parameter1);
                            //            SqlParameter parameter2 = cmd.Parameters[Delimiters.Param + parameter1.Name];
                            //            parameter1.Value = parameter2.Value;
                            //        }
                            //    }
                            //}
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


    }
    internal class Delimiters
    {
        public static string ColumnClose => "]";

        public static string ColumnOpen => "[";

        public static string Param => "@";

        public static string StoredProcNameClose => "]";

        public static string StoredProcNameOpen => "[";

        public static string StringClose => "'";

        public static string StringOpen => "'";

        public static string TableClose => "]";

        public static string TableOpen => "[";
    }
}
