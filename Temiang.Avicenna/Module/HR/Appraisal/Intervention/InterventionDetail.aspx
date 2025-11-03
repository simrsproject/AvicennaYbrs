<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="InterventionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.InterventionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">Period Year</td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPeriodYear" runat="server" Width="304px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">Subject Name</td>
            <td class="entry">
                <telerik:RadComboBox ID="cboEmployeeID" runat="server" Width="304px" EnableLoadOnDemand="true" AutoPostBack="true"
                    HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboEmployeeID_ItemDataBound"
                    OnItemsRequested="cboEmployeeID_ItemsRequested" OnSelectedIndexChanged="cboEmployeeID_SelectedIndexChanged">
                    <ItemTemplate>
                        Subject&nbsp;Name&nbsp;:&nbsp;<%# DataBinder.Eval(Container.DataItem, "EmployeeNumber") %>&nbsp;-&nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>)</b><br />
                        Evaluator&nbsp;Name&nbsp;:&nbsp;<%# DataBinder.Eval(Container.DataItem, "EvaluatorNumber") %>&nbsp;-&nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "EvaluatorName")%>)</b>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvEmployeeID" runat="server" ErrorMessage="Employee ID required."
                    ValidationGroup="entry" ControlToValidate="cboEmployeeID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">Intervention Date</td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtScoringDate" runat="server" Width="100px" />
            </td>
            <td width="20px"></td>
            <td />
        </tr>
        <tr>
            <td class="label">Evaluator Name</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEvaluatorName" runat="server" Width="300px" ReadOnly="true" />
            </td>
            <td width="20px"></td>
            <td />
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <asp:Panel ID="pnlAppraisalQuestionItem" runat="server" Enabled="false" />
            </td>
            <td width="50%">
                <asp:Panel ID="pnlAppraisalIntervention" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
