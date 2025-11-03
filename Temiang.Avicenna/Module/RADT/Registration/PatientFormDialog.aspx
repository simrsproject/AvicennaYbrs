<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientFormDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientFormDialog" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <asp:Panel ID="pnlPatientHealthRecordLine" runat="server" />
</asp:Content>
