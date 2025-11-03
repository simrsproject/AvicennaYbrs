using System.Data;
using System.Linq;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.ReportLibrary.MCU.Systemic;

namespace Temiang.Avicenna.ReportLibrary.MCU.Systemic
{
    /// <summary>
    /// Summary description for Physical Exam.
    /// </summary>
    public partial class MCUSystemicExamRpt : Report
    {
        //private static string regNo, ;

        public MCUSystemicExamRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            PatientHealthRecordLineQuery phrl = new PatientHealthRecordLineQuery("phrl");
            QuestionFormQuery qf = new QuestionFormQuery("qf");
            QuestionGroupQuery qg = new QuestionGroupQuery("qg");
            QuestionGroupQuery qg2 = new QuestionGroupQuery("qg2");
            QuestionQuery q = new QuestionQuery("q");
            QuestionInGroupQuery qig = new QuestionInGroupQuery("qig");
            QuestionInGroupQuery qig2 = new QuestionInGroupQuery("qig2");
            QuestionAnswerSelectionLineQuery qasl = new QuestionAnswerSelectionLineQuery("qasl");

            phrl.Select(
                @"< phrl.RegistrationNo,
		            qf.QuestionFormName,
		            phrl.QuestionFormID,
		            ISNULL(qig.QuestionGroupID,qig2.QuestionGroupID) AS QuestionGroupID,
		            ISNULL(qg.QuestionGroupNameEN,qg2.QuestionGroupNameEN) AS QuestionGroupNameEN,
		            ISNULL(qg.QuestionGroupName,qg2.QuestionGroupName) AS QuestionGroupName,
		            ISNULL(qg.OrderNo,qg2.OrderNo) AS OrderNo,
		            ISNULL(qig.RowIndex,qig2.RowIndex) AS RowIndex,
		            phrl.QuestionID,
		            q.QuestionLevel,
		            q.QuestionText AS QuestionText,
		            q.QuestionShortText,
		            q.SRAnswerType,
		            phrl.QuestionAnswerPrefix,
		            phrl.QuestionAnswerSuffix,
		            CASE 
			            WHEN qasl.QuestionAnswerSelectionLineText IS NULL AND phrl.QuestionAnswerText = '' AND phrl.QuestionAnswerNum IS NULL
			            THEN ''
                        ELSE 
            	            CASE 
					            WHEN qasl.QuestionAnswerSelectionLineText IS NOT NULL AND phrl.QuestionAnswerText = '' AND phrl.QuestionAnswerNum IS NULL
					            THEN qasl.QuestionAnswerSelectionLineText
					            ELSE 
						            CASE 
							            WHEN qasl.QuestionAnswerSelectionLineText IS NULL AND phrl.QuestionAnswerText <> '' AND phrl.QuestionAnswerNum IS NULL
							            THEN phrl.QuestionAnswerText
							            ELSE phrl.QuestionAnswerPrefix + ' '
							            +LTRIM(STR(phrl.QuestionAnswerNum))
							            +' '+phrl.QuestionAnswerSuffix						
						            END 
				            END 
                    END AS AnswerText,
		            REPLICATE('&nbsp;', q.QuestionLevel * 5) + q.QuestionText 
		            + CASE 
			            WHEN qasl.QuestionAnswerSelectionLineText IS NULL AND phrl.QuestionAnswerText = '' AND phrl.QuestionAnswerNum IS NULL
			            THEN '' 
                        ELSE 
            	            CASE 
					            WHEN qasl.QuestionAnswerSelectionLineText IS NOT NULL AND phrl.QuestionAnswerText = '' AND phrl.QuestionAnswerNum IS NULL
					            THEN ': ' + qasl.QuestionAnswerSelectionLineText 
					            ELSE 
						            CASE 
							            WHEN qasl.QuestionAnswerSelectionLineText IS NULL AND phrl.QuestionAnswerText <> '' AND phrl.QuestionAnswerNum IS NULL AND phrl.QuestionAnswerPrefix = ''
							            THEN ': ' + phrl.QuestionAnswerText 
							            WHEN  qasl.QuestionAnswerSelectionLineText IS NOT NULL AND phrl.QuestionAnswerText <> '' AND phrl.QuestionAnswerNum IS NULL AND phrl.QuestionAnswerPrefix <> ''
							            THEN ' | ' + phrl.QuestionAnswerPrefix + ': ' + phrl.QuestionAnswerText 
							            WHEN  qasl.QuestionAnswerSelectionLineText IS NOT NULL AND phrl.QuestionAnswerText <> '' AND phrl.QuestionAnswerNum IS NULL AND phrl.QuestionAnswerPrefix = ''
							            THEN phrl.QuestionAnswerPrefix + ': ' + phrl.QuestionAnswerText 
							            WHEN  qasl.QuestionAnswerSelectionLineText IS NULL AND phrl.QuestionAnswerText <> '' AND phrl.QuestionAnswerNum IS NULL AND phrl.QuestionAnswerPrefix <> ''
							            THEN ' | ' + phrl.QuestionAnswerPrefix + ': ' + phrl.QuestionAnswerText 
							            ELSE ': ' + phrl.QuestionAnswerPrefix + ' ' 
							            +LTRIM(STR(ISNULL(phrl.QuestionAnswerNum,0)))
							            +' '+phrl.QuestionAnswerSuffix					
						            END 
				            END 
                    END AS QuestionAnswer,
		            qasl.QuestionAnswerSelectionLineText,
                    --REPLACE(phrl.QuestionAnswerText, '|', '') as QuestionAnswerText,
		            phrl.QuestionAnswerText,
		            phrl.QuestionAnswerNum                  

                  >"
                );

            phrl.InnerJoin(q).On(q.QuestionID == phrl.QuestionID);
            phrl.InnerJoin(qf).On(qf.QuestionFormID == phrl.QuestionFormID);
            phrl.LeftJoin(qig).On(qig.QuestionID == phrl.QuestionID);
            phrl.LeftJoin(qg).On(qg.QuestionGroupID == phrl.QuestionGroupID);
            phrl.LeftJoin(qig2).On(qig2.QuestionID == q.ParentQuestionID);
            phrl.LeftJoin(qg2).On(qig2.QuestionGroupID == qg2.QuestionGroupID);
            phrl.LeftJoin(qasl).On(qasl.QuestionAnswerSelectionLineID == phrl.QuestionAnswerSelectionLineID);

            phrl.Where(phrl.RegistrationNo == printJobParameters.FindByParameterName("p_RegistrationNo").ValueString
                && q.SRAnswerType != string.Empty
                && phrl.QuestionFormID == printJobParameters.FindByParameterName("p_QuestionFormID").ValueString);

            DataTable dtb = phrl.LoadDataTable();

            var report = new MCUSystemicChildRpt();

            foreach (DataRow row in dtb.Rows)
            {
                var quest = row["QuestionAnswer"].ToString().Split(':');
                if (quest.Length > 1)
                {
                    if (!string.IsNullOrEmpty(quest[1]))
                    {
                        var test = quest[1].Split('|');
                        if (test.Length > 1)
                        {
                            var tab = quest[0];
                            var a = test.Aggregate(string.Empty,
                                                      (current, s) =>
                                                      current + (string.IsNullOrEmpty(s.Trim()) ? "-|" : s + "|"));
                            if (a.Length > 0)
                                a = a.Remove(a.Length - 1, 1);
                            row["QuestionAnswer"] = tab + ": " + a;
                        }
                    }
                }
            }

            dtb.AcceptChanges();

            report.DataSource = dtb;
            subReport1.ReportSource = new InstanceReportSource { ReportDocument = report };

        }
    }
}