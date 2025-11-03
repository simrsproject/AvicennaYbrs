<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PendingPrescription.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.EmrCommon.PendingPrescription" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%=PrescriptionDetailInHTML(RegistrationNo) %>
</asp:Content>