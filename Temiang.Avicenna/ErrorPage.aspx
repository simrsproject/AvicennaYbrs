<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterBasePage.Master" AutoEventWireup="true"
    CodeBehind="ErrorPage.aspx.cs" Inherits="Temiang.Avicenna.ErrorPage" Title="Untitled Page" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .middleDiv
        {
            position: absolute;
            width: 750px;
            height: 400px;
            left: 50%;
            top: 50%;
            margin-left: -375px; /* half of the width  */
            margin-top: -200px; /* half of the height */
        }
    </style>
    
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function Close() {
            var oWnd = GetRadWindow();
            if (oWnd != null)
                oWnd.close();
            else
                history.go(-1);
        }
    </script>
    <div class="middleDiv" >
        <div style="height: 35px">
        </div>
        <asp:Label ID="lblErrorMessage" runat="server" Font-Size="12px" Text="We are sorry for any inconvenience cause. Should you have any problem, please contact the IT helpdesk for any assistance. Thank you." />
        <div style="height: 35px">
        </div>
        <cc:CollapsePanel ID="clpErrorDetail" runat="server" Title="Click here for show / hide error detail" IsCollapsed="false"
            Width="756px">
            <asp:TextBox runat="server" ID="txtErrorTrace" Width="750px" Height="300px" TextMode="MultiLine"
                ReadOnly="True"></asp:TextBox>
        </cc:CollapsePanel>
        <div style="width: 100%; height: 35px; margin-top: 10px; margin-right: 10px;">
            <div style="float: right; width: 5px; height: 10px;">
            </div>
            <asp:Button ID="btnBack" Width="70px" runat="server" Text="Back" Style="float: right;"
                OnClientClick="javascript:Close();return false;" />
        </div>
    </div>
</asp:Content>
