<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="NosocomialSurgeryEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.NosocomialSurgeryEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            var height = 0;
            var width = 0;
            function openWinBookingInfo() {
                var curWnd = GetRadWindow();
                var curWndBounds = curWnd.getWindowBounds();
                // Not Maximized
                if (curWndBounds.y > 0 & curWndBounds.x > 0) {
                    height = curWndBounds.height;
                    width = curWndBounds.width;

                    var setHeight = 580;
                    if (setHeight < height)
                        setHeight = height;

                    var setWidth = 1080;
                    if (setWidth < width)
                        setWidth = width;


                    curWnd.setSize(setWidth, setHeight);
                    curWnd.center();
                }

                var oWnd = $find("<%= winBooking.ClientID %>");
                oWnd.setUrl('<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/Common/Phr/BookingReferenceDialog.aspx?regno=<%= RegistrationNo %>');
                oWnd.show();
                oWnd.center();

            }

            function winBooking_ClientClose(oWnd, args) {
                var curWnd = GetRadWindow();
                if (width > 0) {
                    curWnd.setSize(width, height);
                    curWnd.center();
                }

                if (oWnd.argument) {
                    __doPostBack("<%= txtReferenceNo.UniqueID %>", oWnd.argument.id);
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="500px" Behavior="None" VisibleTitlebar="False"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winBooking_ClientClose"
        ID="winBooking" />
    <asp:HiddenField runat="server" ID="hdnEditRegistrationNo" />
    <asp:HiddenField runat="server" ID="hdnEditMonitoringNo" />

    <telerik:RadAjaxPanel runat="server">
        <table width="100%">
            <tr>
                <td class="label">Operating Room Booking No 
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="300px" ShowButton="true"
                        ClientEvents-OnButtonClick="openWinBookingInfo" />

                </td>
                <td style="width: 20px;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Operating Room Booking No."
                        ValidationGroup="entry" ControlToValidate="txtReferenceNo" SetFocusOnError="True"
                        Width="100%">
                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator></td>

            </tr>
            <tr>
                <td class="label">Surgery Date 
                </td>
                <td class="entry">
                    <telerik:RadDateTimePicker ID="txtInstallationDateTime" runat="server" Width="170px" DatePopupButton-Visible="False" Enabled="False" />
                </td>
                <td style="width: 20px;"></td>
                <td></td>

            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="Label4" runat="server" Text="Surgery By"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtSurgeryByName" runat="server" Width="304px" ReadOnly="True" />
                </td>
                <td width="20px"></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="Label1" runat="server" Text="Wound Classification"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtWoundClassification" runat="server" Width="304px" ReadOnly="True" />
                </td>
                <td width="20px"></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="Label3" runat="server" Text="ASA"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtAsaScore" runat="server" Width="304px" ReadOnly="True" />
                </td>
                <td width="20px"></td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Surgery Location"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtLocation" runat="server" Width="304px" />
            </td>
            <td style="width: 20px;"></td>
            <td></td>
        </tr>

        <tr>
            <td class="label">
                <asp:Label ID="Label6" runat="server" Text="Antibiotic Consumption"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadAutoCompleteBox ID="acbAntibiotic" runat="server" Width="304px" DropDownHeight="150">
                </telerik:RadAutoCompleteBox>
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label7" runat="server" Text="Other Drugs Consumption"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadAutoCompleteBox ID="acbOtherDrug" runat="server" Width="304px" DropDownHeight="150">
                </telerik:RadAutoCompleteBox>
            </td>
            <td width="20px"></td>
        </tr>

    </table>
</asp:Content>
