<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PatientRiskColorDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.PatientRiskColorDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label"><asp:Label ID="lblSRPatientRiskColor" runat="server" Text="Patient Risk Color"></asp:Label></td>
            <td class="entry">
                <asp:RadioButtonList ID="rbtSRPatientRiskColor" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" SkinID="">
                    <asp:ListItem Value="0" Text="<span style='background-color:gray;width:30px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;No Risk" />
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Content>
