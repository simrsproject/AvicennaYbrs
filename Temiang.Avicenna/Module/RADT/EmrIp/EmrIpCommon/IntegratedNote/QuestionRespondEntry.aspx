<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="QuestionRespondEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.QuestionRespondEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/PHR/PhrCtl.ascx" TagPrefix="uc1" TagName="PhrCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" id="tblEntry" runat="server">
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Date Time"></asp:Label>
            </td>
            <td width="300px">
                <telerik:RadDateTimePicker ID="txtDateTimeImplementation" Width="160px" runat="server" AutoPostBackControl="None">
                    <DateInput ID="DateInput1" runat="server"
                        DisplayDateFormat="dd/MM/yyyy HH:mm"
                        DateFormat="dd/MM/yyyy HH:mm">
                    </DateInput>
                    <TimeView runat="server" TimeFormat="HH:mm"></TimeView>
                </telerik:RadDateTimePicker>
            </td>
            <td width="25px">
                <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="Date required."
                    ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtDateTimeImplementation" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
    </table>
    <uc1:PhrCtl runat="server" ID="phrCtl" />
</asp:Content>
