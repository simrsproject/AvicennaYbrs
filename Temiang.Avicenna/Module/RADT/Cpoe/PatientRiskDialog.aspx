<%@ Page Title="Patient Risk Status" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PatientRiskDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.PatientRiskDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label"><asp:Label ID="lblSRPatientRiskStatus" runat="server" Text="Patient Risk Status"></asp:Label></td>
            <td class="entry">
                <asp:RadioButtonList ID="rbtSRPatientRiskStatus" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                    <asp:ListItem Value="X" Text="No Risk" />
                    <asp:ListItem Value="0" Text="Low" />
                    <asp:ListItem Value="1" Text="Medium" />
                    <asp:ListItem Value="2" Text="High" />
                </asp:RadioButtonList>
            </td>
            <td width="20px" />
            <td />
        </tr>
    </table>
</asp:Content>
