<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="QuestionerSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.QuestionerSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblQuestionerName" runat="server" Text="Questionnaire Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">

                <telerik:RadComboBox ID="cboFilterQuestionerName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>

            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtQuestionerName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPeriodYear" runat="server" Text="PeriodYear" Width="100px"></asp:Label>
            </td>
            <td class="filter">

                <telerik:RadComboBox ID="cboFilterPeriodYear" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>

            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPeriodYear" runat="server" Width="300px" MaxLength="4" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
