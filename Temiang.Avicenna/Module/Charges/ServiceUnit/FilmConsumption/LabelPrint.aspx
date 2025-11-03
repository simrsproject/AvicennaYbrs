<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="LabelPrint.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.LabelPrint" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="lblTransactionNo" Text="Transaction No" />
            </td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtTransactionNo" Width="300px" Enabled="False" />
            </td>
            <td width="20px">
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="lblTransactionDate" Text="Transaction Date" />
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" Enabled="False" />
            </td>
            <td width="20px">
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="lblMedicalNo" Text="Medical No" />
            </td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtMedicalNo" Width="300px" Enabled="False" />
            </td>
            <td width="20px">
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="lblPatientName" Text="Patient Name" />
            </td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtPatientName" Width="300px" Enabled="False" />
            </td>
            <td width="20px">
            </td>
            <td />
        </tr>
        <tr runat="server" id="trFilmNo">
            <td class="label">
                <asp:Label runat="server" ID="lblFilmNo" Text="Film No" />
            </td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtFilmNo" Width="300px"/>
            </td>
            <td width="20px">
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label runat="server" ID="lblExamination" Text="Examination" />
            </td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtExamination" Width="300px" TextMode="MultiLine" />
            </td>
            <td width="20px">
            </td>
            <td />
        </tr>
        <asp:Panel runat="server" ID="pnlEnvelopeSize">
            <tr>
            <td class="label">
                <asp:Label runat="server" ID="lblEnvelopeSize" Text="Envelope Size" />
            </td>
            <td class="entry">
                <asp:RadioButtonList ID="rbtEnvelopeSize" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Value="0" Text="Small" Selected="True" />
                <asp:ListItem Value="1" Text="Large" />
            </asp:RadioButtonList>
            </td>
            <td width="20px">
            </td>
            <td />
        </tr>
        </asp:Panel>
    </table>
</asp:Content>
