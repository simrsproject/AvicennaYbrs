<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ExamOrderImageZoomView.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.ExamOrderImageZoomView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadBinaryImage ID="imgDocumentImage" runat="server" AlternateText=""
        Width="100%"
        Height="100%" ResizeMode="None"
        BorderStyle="Double"></telerik:RadBinaryImage>
</asp:Content>
