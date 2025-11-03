<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="RegistrationPtoEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.RegistrationPtoEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
        var _height = 0;
        var _width = 0;
        function onPickerClose(sender, eventArgs) {

            // Restore window size
            var curWnd = GetRadWindow();
            if (_width > 0) {
                curWnd.setSize(_width, _height);
                curWnd.center();
            }

            // Get retval
            var arg = eventArgs.get_argument();
            if (arg) {
                var tgt = $find(arg.eventTarget);
                var prevVal = tgt.get_value();
                if (prevVal != "")
                    tgt.set_value(prevVal + '\r' + unescape(arg.retval));
                else
                    tgt.set_value(unescape(arg.retval));
            }
        }
        function pickP() {
            openPicker("PTOP","<%=txtPtoP.ClientID %>");
        }
        function pickA() {
            openPicker("PTOA","<%=txtPtoA.ClientID %>");
        }
        function openPicker(type, clientid) {

            // Resize window
            var curWnd = GetRadWindow();
            var curWndBounds = curWnd.getWindowBounds();
            // Not Maximized
            if (curWndBounds.y > 0 & curWndBounds.x > 0) {
                _height = curWndBounds.height;
                _width = curWndBounds.width;

                var setHeight = 700;
                if (setHeight < _height)
                    setHeight = _height;

                var setWidth = 1200;
                if (setWidth < _width)
                    setWidth = _width;

                curWnd.setSize(setWidth, setHeight);
                curWnd.center();
            }

            var oWnd = $find("<%= winPicker.ClientID %>");
            oWnd.setUrl("PtoPicker.aspx?refid=" + type + "&cet=" + clientid);

            oWnd.setSize(1000, 600);
            oWnd.center();
            oWnd.show();
        }

    </script>
    <telerik:RadWindow ID="winPicker" Width="680px" Height="620px" runat="server" Modal="true"
        ShowContentDuringLoad="false" Behaviors="None" VisibleStatusbar="False"
        OnClientClose="onPickerClose">
    </telerik:RadWindow>
    <table width="100%">
        <tr>
            <td class="label">No 
            </td>
            <td>
                <telerik:RadTextBox ID="txtPtoNo" runat="server" Width="50px" Enabled="false" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Date 
            </td>
            <td>
                <telerik:RadDateTimePicker ID="txtPtoDateTime" runat="server" Width="170px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Pto Time required."
                    ValidationGroup="entry" ControlToValidate="txtPtoDateTime" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label7" runat="server" Text="Subjective"></asp:Label>
            </td>
            <td>
                <telerik:RadTextBox ID="txtPtoS" TextMode="MultiLine" runat="server" Width="100%" Height="100px" MaxLength="500" Resize="Vertical" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Objective"></asp:Label>
            </td>
            <td>
                <telerik:RadTextBox ID="txtPtoO" TextMode="MultiLine" runat="server" Width="100%" Height="100px" MaxLength="500" Resize="Vertical" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Assessment"></asp:Label>
                <br />
                <asp:LinkButton runat="server" ID="lbtnPickA" OnClientClick="pickA(); return false;">
                    <img src="../../../../Images/Toolbar/views16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <telerik:RadTextBox ID="txtPtoA" TextMode="MultiLine" runat="server" Width="100%" Height="100px" MaxLength="500" Resize="Vertical" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Planning"></asp:Label>
                <br />
                <asp:LinkButton runat="server" ID="lbtnPickP" OnClientClick="pickP(); return false;">
                    <img src="../../../../Images/Toolbar/views16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <telerik:RadTextBox ID="txtPtoP" TextMode="MultiLine" runat="server" Width="100%" Height="100px" MaxLength="500" Resize="Vertical" />
            </td>
            <td width="20px"></td>
        </tr>
    </table>
</asp:Content>
