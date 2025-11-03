<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="QuestionnaireSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.CPA.QuestionnaireSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblQuestionnaireCode" runat="server" Text="Code"></asp:Label>
            </td>
            <td class="filter">

                <telerik:RadComboBox ID="cboFilterQuestionnaireCode" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>

            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtQuestionnaireCode" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblQuestionnaireName" runat="server" Text="Form Name"></asp:Label>
            </td>
            <td class="filter">

                <telerik:RadComboBox ID="cboFilterQuestionnaireName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>

            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtQuestionnaireName" runat="server" Width="300px" MaxLength="255" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
