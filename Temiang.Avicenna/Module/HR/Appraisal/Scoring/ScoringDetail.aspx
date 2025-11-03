<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ScoringDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.ScoringDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">Scoring Date</td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtScoringDate" runat="server" Width="100px" />
            </td>
            <td width="20px"></td>
            <td />
        </tr>
        <tr>
            <td class="label">Period Year</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPeriodYear" runat="server" Width="100px" ReadOnly="true" /></td>
            <td width="20px"></td>
            <td />
        </tr>
        <tr>
            <td class="label">Evaluator Name</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEvaluatorName" runat="server" Width="300px" ReadOnly="true" /></td>
            <td width="20px"></td>
            <td />
        </tr>
        <tr>
            <td class="label">Subject Name</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="300px" ReadOnly="true" /></td>
            <td width="20px"></td>
            <td />
        </tr>
    </table>
    <asp:Panel ID="pnlAppraisalQuestionItem" runat="server" />
</asp:Content>
