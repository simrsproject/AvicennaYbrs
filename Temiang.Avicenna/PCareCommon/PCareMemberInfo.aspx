<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PCareMemberInfo.aspx.cs" Inherits="Temiang.Avicenna.PCareCommon.PCareMemberInfo" %>

<%@ Register Src="~/PcareCommon/PCareMember.ascx" TagPrefix="uc1" TagName="BpjsMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:BpjsMember runat="server" ID="BpjsMember" />
</asp:Content>
