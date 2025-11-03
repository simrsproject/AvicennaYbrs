<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PCareReferenceCtl.ascx.cs" Inherits="Temiang.Avicenna.PCareCommon.PCareReferenceCtl" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

    <script language="javascript" type="text/javascript">

        function openWinPCareLookUp() {
            var oWnd = $find("<%= winPCareLookUp.ClientID %>");
            var url = '<%=Page.ResolveUrl("~/PCareCommon/LookUp/LookUpMaster.aspx")+"?rtype="+ ReferenceType%>';
            // JANGAN PAKAI radopen,  urlnya harus lengkap dgn rootnya jika pakai radopen
            oWnd.setUrl(url);
            oWnd.center();
            oWnd.show();

        }

        function onWinPCareLookUpClose(oWnd, args) {
            var txtCode = $find("<%= txtPCareItemID.ClientID %>");

            if (oWnd.argument)
                txtCode.set_value(oWnd.argument.code);
        }

    </script>
</telerik:RadCodeBlock>
<telerik:RadWindow ID="winPCareLookUp" Animation="None" Width="600px" Height="400px"
    runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
    Modal="true" OnClientClose="onWinPCareLookUpClose">
</telerik:RadWindow>

<table width="100%" cellpadding="0" cellspacing="0">

    <tr>
        <td class="label">
            <asp:Label ID="Label3" runat="server" Text="PCare Code"></asp:Label>
        </td>
        <td class="entry">

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="100px">
                        <telerik:RadTextBox ID="txtPCareItemID" runat="server" Width="100px" ShowButton="true"
                            ClientEvents-OnButtonClick="openWinPCareLookUp" OnTextChanged="txtPCareItemID_TextChanged"
                            AutoPostBack="true"  />

                    </td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblPCareItemName" runat="server" CssClass="labeldescription" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="20"></td>
        <td></td>
    </tr>
</table>
