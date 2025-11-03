<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="Prescription.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.Medication.Prescription" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%=PrescriptionDetailInHTML(PrescriptionNo) %>
</asp:Content>