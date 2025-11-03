<%@ Page Title="Covid-19 Status" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="CovidStatusDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.CovidStatusDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label">Covid-19 Status</td>
            <td class="entry">
                <telerik:RadComboBox ID="cboCovidStatus" runat="server" Width="304px">
                </telerik:RadComboBox>
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">Comorbid Status</td>
            <td class="entry">
                <telerik:RadComboBox ID="cboComorbidStatus" runat="server" Width="304px">
                </telerik:RadComboBox>
            </td>
            <td width="20px" />
            <td />
        </tr>
    </table>
</asp:Content>
