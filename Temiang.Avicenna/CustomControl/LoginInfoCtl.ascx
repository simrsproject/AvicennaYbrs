<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginInfoCtl.ascx.cs"
    Inherits="Temiang.Avicenna.CustomControl.LoginInfoCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script language="javascript">
    function logoutClick() {
        //    window.location = "<%=Helper.UrlRoot()%>/login/login.aspx?tp=logout";
        window.location = '<%=Page.ResolveClientUrl("~/login/login.aspx?tp=logout")%>';
    }

    function changePasswordClick() {
        //    window.location = "<%=Helper.UrlRoot()%>/login/ChangePassword.aspx";
        window.location = '<%=Page.ResolveClientUrl("~/login/ChangePassword.aspx")%>';
    }
</script>

<asp:Label ID="lblWelcome" runat="server" Text="Welcome, " />&nbsp;[<%=AppSession.UserLogin.SRUserType%>]&nbsp;
<asp:Label ID="lblUserName" runat="server" Text="[User Name]" />
&nbsp;
<a href="<%=Page.ResolveClientUrl("~/")%>"><img src="<%=Page.ResolveClientUrl("~/Images/Toolbar/home_purple16_h.png")%>" /></a>
&nbsp;&nbsp;[
<asp:LinkButton ID="lnbLogout" runat="server" Text="Logout" OnClientClick="javascript:logoutClick();return false;"
    ForeColor="WhiteSmoke" />
&nbsp;|&nbsp;
<asp:LinkButton ID="lnbChangePassword" runat="server" Text="Change Password" OnClientClick="javascript:changePasswordClick();return false;"
    ForeColor="WhiteSmoke" />
&nbsp;&nbsp;
<telerik:RadComboBox runat="server" ID="cboThemes" Width="104px" AutoPostBack="true"
    OnSelectedIndexChanged="cboThemes_SelectedIndexChanged">
    <Items>
        <telerik:RadComboBoxItem Value="WebBlue" Text="Aviat" />
        <telerik:RadComboBoxItem Value="Default" Text="Avicenna" />
    </Items>
</telerik:RadComboBox>
]&nbsp;

