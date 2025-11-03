using MySqlConnector;
using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Newtonsoft.Json;
using System.Configuration;
using System.Text;

namespace Temiang.Avicenna.Common.Worklist.RSBK
{
    public class Service
    {
        //private readonly string connectionELVA = "Server=201.5.80.149;Port=8900;User ID=elva;Password=avicenna;Database=pacs";
        private readonly string connectionString;
        public Service()
        {
            string server = ConfigurationManager.AppSettings["PacsServer"];
            string port = ConfigurationManager.AppSettings["PacsPort"];
            string user = ConfigurationManager.AppSettings["PacsUser"];
            string pass = ConfigurationManager.AppSettings["PacsPass"];
            string database = ConfigurationManager.AppSettings["PacsDatabase"];

            connectionString = $"Server={server};Port={port};User ID={user};Password={pass};Database={database}";
        }

        public bool CheckConnection()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "",
                    UrlAddress = "CheckConnectionSERVICERIS",
                    Params = JsonConvert.SerializeObject(ex),
                    Response = ex.Message,
                    Totalms = 0
                };
                log.Save();
                return false;
            }
        }

        public bool InsertExamOrder(List<DataExamOrder> data)
        {
            var row = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (DataExamOrder item in data)
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = @"INSERT INTO examOrder (patient_id, patient_name, patient_sex, patient_birthday,
                                            patient_weight, patient_class, ward, attending_doctor, referring_doctor, order_control,
                                            order_department, accession_number, study_code, study_name, order_datetime,
                                            scheduled_datetime, clinic_comments, sickness_name, reason_for_study, body_part,
                                            ordering_doctor, exam_room, modality, operator_name, exam_urgent, issuer, if_flag,
                                            result, urllink)
                                            VALUES (@patient_id, @patient_name, @patient_sex, @patient_birthday, @patient_weight,
                                            @patient_class, @ward, @attending_doctor, @referring_doctor, @order_control,
                                            @order_department, @accession_number, @study_code, @study_name, @order_datetime,
                                            @scheduled_datetime, @clinic_comments, @sickness_name, @reason_for_study, @body_part,
                                            @ordering_doctor, @exam_room, @modality, @operator_name, @exam_urgent, @issuer,
                                            @if_flag, @result, @urllink)";

                            command.Parameters.AddWithValue("@patient_id", item.patient_id);
                            command.Parameters.AddWithValue("@patient_name", item.patient_name);
                            command.Parameters.AddWithValue("@patient_sex", item.patient_sex);
                            command.Parameters.AddWithValue("@patient_birthday", item.patient_birthday);
                            command.Parameters.AddWithValue("@patient_weight", item.patient_weight);
                            command.Parameters.AddWithValue("@patient_class", item.patient_class);
                            command.Parameters.AddWithValue("@ward", item.ward);
                            command.Parameters.AddWithValue("@attending_doctor", item.attending_doctor);
                            command.Parameters.AddWithValue("@referring_doctor", item.referring_doctor);
                            command.Parameters.AddWithValue("@order_control", item.order_control);
                            command.Parameters.AddWithValue("@order_department", item.order_department);
                            command.Parameters.AddWithValue("@accession_number", item.accession_number);
                            command.Parameters.AddWithValue("@study_code", item.study_code);
                            command.Parameters.AddWithValue("@study_name", item.study_name);
                            command.Parameters.AddWithValue("@order_datetime", item.order_datetime);
                            command.Parameters.AddWithValue("@scheduled_datetime", item.scheduled_datetime);
                            command.Parameters.AddWithValue("@clinic_comments", item.clinic_comments);
                            command.Parameters.AddWithValue("@sickness_name", item.sickness_name);
                            command.Parameters.AddWithValue("@reason_for_study", item.reason_for_study);
                            command.Parameters.AddWithValue("@body_part", item.body_part);
                            command.Parameters.AddWithValue("@ordering_doctor", item.ordering_doctor);
                            command.Parameters.AddWithValue("@exam_room", item.exam_room);
                            command.Parameters.AddWithValue("@modality", item.modality);
                            command.Parameters.AddWithValue("@operator_name", item.operator_name);
                            command.Parameters.AddWithValue("@exam_urgent", item.exam_urgent);
                            command.Parameters.AddWithValue("@issuer", item.issuer);
                            command.Parameters.AddWithValue("@if_flag", item.if_flag);
                            command.Parameters.AddWithValue("@result", item.result);
                            command.Parameters.AddWithValue("@urllink", item.urllink);

                            row += command.ExecuteNonQuery();
                        }
                    }
                }

                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "",
                    UrlAddress = "InsertToRIS",
                    Params = JsonConvert.SerializeObject(data),
                    Response = string.Empty,
                    Totalms = 0
                };
                log.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "",
                    UrlAddress = "SERVICERISNW",
                    Params = JsonConvert.SerializeObject(data),
                    Response = ex.Message,
                    Totalms = 0
                };
                log.Save();
                throw;
            }
            return row > 0;
        }

        public bool CancelExamOrder(List<DataExamOrder> data)
        {
            var row = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (DataExamOrder item in data)
                    {
                        string existingAccessionNumber = FindExamOrder(item);
                        if (existingAccessionNumber == null)
                        {
                            Console.WriteLine("Failed to cancel because this already complete in pacs");
                            return false;
                        }

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = @"INSERT INTO examOrder (patient_id, patient_name, patient_sex, patient_birthday,
                                            patient_weight, patient_class, ward, attending_doctor, referring_doctor, order_control,
                                            order_department, accession_number, study_code, study_name, order_datetime,
                                            scheduled_datetime, clinic_comments, sickness_name, reason_for_study, body_part,
                                            ordering_doctor, exam_room, modality, operator_name, exam_urgent, issuer, if_flag,
                                            result, urllink)
                                            VALUES (@patient_id, @patient_name, @patient_sex, @patient_birthday, @patient_weight,
                                            @patient_class, @ward, @attending_doctor, @referring_doctor, @order_control,
                                            @order_department, @accession_number, @study_code, @study_name, @order_datetime,
                                            @scheduled_datetime, @clinic_comments, @sickness_name, @reason_for_study, @body_part,
                                            @ordering_doctor, @exam_room, @modality, @operator_name, @exam_urgent, @issuer,
                                            @if_flag, @result, @urllink)";

                            // Add parameters
                            command.Parameters.AddWithValue("@patient_id", item.patient_id);
                            command.Parameters.AddWithValue("@patient_name", item.patient_name);
                            command.Parameters.AddWithValue("@patient_sex", item.patient_sex);
                            command.Parameters.AddWithValue("@patient_birthday", item.patient_birthday);
                            command.Parameters.AddWithValue("@patient_weight", item.patient_weight);
                            command.Parameters.AddWithValue("@patient_class", item.patient_class);
                            command.Parameters.AddWithValue("@ward", item.ward);
                            command.Parameters.AddWithValue("@attending_doctor", item.attending_doctor);
                            command.Parameters.AddWithValue("@referring_doctor", item.referring_doctor);
                            command.Parameters.AddWithValue("@order_control", item.order_control);
                            command.Parameters.AddWithValue("@order_department", item.order_department);
                            command.Parameters.AddWithValue("@accession_number", item.accession_number);
                            command.Parameters.AddWithValue("@study_code", item.study_code);
                            command.Parameters.AddWithValue("@study_name", item.study_name);
                            command.Parameters.AddWithValue("@order_datetime", item.order_datetime);
                            command.Parameters.AddWithValue("@scheduled_datetime", item.scheduled_datetime);
                            command.Parameters.AddWithValue("@clinic_comments", item.clinic_comments);
                            command.Parameters.AddWithValue("@sickness_name", item.sickness_name);
                            command.Parameters.AddWithValue("@reason_for_study", item.reason_for_study);
                            command.Parameters.AddWithValue("@body_part", item.body_part);
                            command.Parameters.AddWithValue("@ordering_doctor", item.ordering_doctor);
                            command.Parameters.AddWithValue("@exam_room", item.exam_room);
                            command.Parameters.AddWithValue("@modality", item.modality);
                            command.Parameters.AddWithValue("@operator_name", item.operator_name);
                            command.Parameters.AddWithValue("@exam_urgent", item.exam_urgent);
                            command.Parameters.AddWithValue("@issuer", item.issuer);
                            command.Parameters.AddWithValue("@if_flag", item.if_flag);
                            command.Parameters.AddWithValue("@result", item.result);
                            command.Parameters.AddWithValue("@urllink", item.urllink);

                            row += command.ExecuteNonQuery();
                        }
                    }
                }

                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "",
                    UrlAddress = "CancelToRIS",
                    Params = JsonConvert.SerializeObject(data),
                    Response = string.Empty,
                    Totalms = 0
                };
                log.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "",
                    UrlAddress = "SERVICERISCA",
                    Params = JsonConvert.SerializeObject(data),
                    Response = ex.Message,
                    Totalms = 0
                };
                log.Save();
                throw;
            }
            return row > 0;
        }

        public bool UpdateExamOrder(List<DataExamOrder> data)
        {
            var row = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (DataExamOrder item in data)
                    {
                        string existingAccessionNumber = FindExamOrder(item);
                        if (existingAccessionNumber == null)
                        {
                            Console.WriteLine("Failed to cancel because this already complete in pacs");
                            return false;
                        }

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = @"INSERT INTO examOrder (patient_id, patient_name, patient_sex, patient_birthday,
                                            patient_weight, patient_class, ward, attending_doctor, referring_doctor, order_control,
                                            order_department, accession_number, study_code, study_name, order_datetime,
                                            scheduled_datetime, clinic_comments, sickness_name, reason_for_study, body_part,
                                            ordering_doctor, exam_room, modality, operator_name, exam_urgent, issuer, if_flag,
                                            result, urllink)
                                            VALUES (@patient_id, @patient_name, @patient_sex, @patient_birthday, @patient_weight,
                                            @patient_class, @ward, @attending_doctor, @referring_doctor, @order_control,
                                            @order_department, @accession_number, @study_code, @study_name, @order_datetime,
                                            @scheduled_datetime, @clinic_comments, @sickness_name, @reason_for_study, @body_part,
                                            @ordering_doctor, @exam_room, @modality, @operator_name, @exam_urgent, @issuer,
                                            @if_flag, @result, @urllink)";

                            // Add parameters
                            command.Parameters.AddWithValue("@patient_id", item.patient_id);
                            command.Parameters.AddWithValue("@patient_name", item.patient_name);
                            command.Parameters.AddWithValue("@patient_sex", item.patient_sex);
                            command.Parameters.AddWithValue("@patient_birthday", item.patient_birthday);
                            command.Parameters.AddWithValue("@patient_weight", item.patient_weight);
                            command.Parameters.AddWithValue("@patient_class", item.patient_class);
                            command.Parameters.AddWithValue("@ward", item.ward);
                            command.Parameters.AddWithValue("@attending_doctor", item.attending_doctor);
                            command.Parameters.AddWithValue("@referring_doctor", item.referring_doctor);
                            command.Parameters.AddWithValue("@order_control", item.order_control);
                            command.Parameters.AddWithValue("@order_department", item.order_department);
                            command.Parameters.AddWithValue("@accession_number", item.accession_number);
                            command.Parameters.AddWithValue("@study_code", item.study_code);
                            command.Parameters.AddWithValue("@study_name", item.study_name);
                            command.Parameters.AddWithValue("@order_datetime", item.order_datetime);
                            command.Parameters.AddWithValue("@scheduled_datetime", item.scheduled_datetime);
                            command.Parameters.AddWithValue("@clinic_comments", item.clinic_comments);
                            command.Parameters.AddWithValue("@sickness_name", item.sickness_name);
                            command.Parameters.AddWithValue("@reason_for_study", item.reason_for_study);
                            command.Parameters.AddWithValue("@body_part", item.body_part);
                            command.Parameters.AddWithValue("@ordering_doctor", item.ordering_doctor);
                            command.Parameters.AddWithValue("@exam_room", item.exam_room);
                            command.Parameters.AddWithValue("@modality", item.modality);
                            command.Parameters.AddWithValue("@operator_name", item.operator_name);
                            command.Parameters.AddWithValue("@exam_urgent", item.exam_urgent);
                            command.Parameters.AddWithValue("@issuer", item.issuer);
                            command.Parameters.AddWithValue("@if_flag", item.if_flag);
                            command.Parameters.AddWithValue("@result", item.result);
                            command.Parameters.AddWithValue("@urllink", item.urllink);

                            row += command.ExecuteNonQuery();
                        }
                    }
                }

                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "",
                    UrlAddress = "UpdateToRIS",
                    Params = JsonConvert.SerializeObject(data),
                    Response = string.Empty,
                    Totalms = 0
                };
                log.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "",
                    UrlAddress = "SERVICERISRO",
                    Params = JsonConvert.SerializeObject(data),
                    Response = ex.Message,
                    Totalms = 0
                };
                log.Save();
                throw;
            }
            return row > 0;
        }

        public string FindExamOrder(DataExamOrder dataExamOrder)
        {
            string accession_number = null;
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                var query = "SELECT accession_number FROM examOrder WHERE accession_number = @accession_number AND order_control = 'NW' AND issuer = 'H' ORDER BY createdate DESC LIMIT 1;";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accession_number", dataExamOrder.accession_number);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            accession_number = reader["accession_number"].ToString();
                        }
                        else
                        {
                            Console.WriteLine("No matching records found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "",
                    UrlAddress = "SERVICERISFE",
                    Params = JsonConvert.SerializeObject(accession_number),
                    Response = ex.Message,
                    Totalms = 0
                };
                log.Save();
            }
            return accession_number;
        }

        public string GetUrllink(DataExamOrder dataExamOrder)
        {
            string urllink = null;
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                var query = "SELECT urllink FROM examOrder WHERE accession_number = @accession_number AND order_control = 'CM' AND issuer = 'P' ORDER BY createdate DESC;";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accession_number", dataExamOrder.accession_number);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            urllink = reader["urllink"].ToString();
                        }
                        else
                        {
                            Console.WriteLine("No matching records found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "",
                    UrlAddress = "SERVICERISDCM",
                    Params = JsonConvert.SerializeObject(urllink),
                    Response = ex.Message,
                    Totalms = 0
                };
                log.Save();
            }
            return urllink;
        }

        public List<DataExamReport> GetReport(DataExamReport dataExamReport)
        {
            var reportList = new List<DataExamReport>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    var query = "SELECT * FROM examReport WHERE accession_number = @accession_number AND issuer = 'P' ORDER BY createdate DESC;";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accession_number", dataExamReport.accession_number);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var report = new DataExamReport
                                {
                                    createdate = reader["createdate"].ToString(),
                                    accession_number = reader["accession_number"].ToString(),
                                    examid = reader["examid"].ToString(),
                                    patient_id = reader["patient_id"].ToString(),
                                    modality = reader["modality"].ToString(),
                                    reading_doctor1 = reader["reading_doctor1"].ToString(),
                                    reading_doctor2 = reader["reading_doctor2"].ToString(),
                                    reading_doctor3 = reader["reading_doctor3"].ToString(),
                                    confirm_doctor = reader["confirm_doctor"].ToString(),
                                    report_date_time = reader["report_date_time"].ToString(),
                                    confirm_date_time = reader["confirm_date_time"].ToString(),
                                    report_status = reader["report_status"].ToString(),
                                    report = reader["report"].ToString()
                                };

                                reportList.Add(report);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetReport: {ex.Message}");
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "",
                    UrlAddress = "SERVICERISEPS",
                    Params = JsonConvert.SerializeObject(dataExamReport),
                    Response = ex.Message,
                    Totalms = 0
                };
                log.Save();
            }
            return reportList;
        }

        private static string WrapText(string text, int maxLength)
        {
            if (text.Length <= maxLength)
                return text;

            var sb = new StringBuilder();
            int index = 0;

            while (index < text.Length)
            {
                sb.Append(text.Substring(index, Math.Min(maxLength, text.Length - index)));
                if (index + maxLength < text.Length)
                    sb.AppendLine();
                index += maxLength;
            }

            return sb.ToString();
        }
    }

    public class DataExamOrder
    {
        public string id { get; set; }
        public string createdate { get; set; }
        public string patient_id { get; set; }
        public string patient_name { get; set; }
        public string patient_sex { get; set; }
        public string patient_birthday { get; set; }
        public string patient_weight { get; set; }
        public string patient_class { get; set; }
        public string ward { get; set; }
        public string attending_doctor { get; set; }
        public string referring_doctor { get; set; }
        public string order_control { get; set; }
        public string order_department { get; set; }
        public string accession_number { get; set; }
        public string study_code { get; set; }
        public string study_name { get; set; }
        public string order_datetime { get; set; }
        public string scheduled_datetime { get; set; }
        public string clinic_comments { get; set; }
        public string sickness_name { get; set; }
        public string reason_for_study { get; set; }
        public string body_part { get; set; }
        public string ordering_doctor { get; set; }
        public string exam_room { get; set; }
        public string modality { get; set; }
        public string operator_name { get; set; }
        public string exam_urgent { get; set; }
        public string issuer { get; set; }
        public int if_flag { get; set; }
        public int result { get; set; }
        public string urllink { get; set; }
    }

    public class DataExamReport
    {
        public string id { get; set; }
        public string createdate { get; set; }
        public string accession_number { get; set; }
        public string examid { get; set; }
        public string patient_id { get; set; }
        public string modality { get; set; }
        public string reading_doctor1 { get; set; }
        public string reading_doctor2 { get; set; }
        public string reading_doctor3 { get; set; }
        public string confirm_doctor { get; set; }
        public string report_date_time { get; set; }
        public string confirm_date_time { get; set; }
        public string report_status { get; set; }
        public string report { get; set; }
        public string issuer { get; set; }
        public int if_flag { get; set; }
        public int result { get; set; }
    }
}
