<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="NosocomialBedRestDetailEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.NosocomialBedRestDetailEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">Time
            </td>
            <td class="entry">
                <telerik:RadDateTimePicker ID="txtMonitoringDateTime" runat="server" Width="170px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rf1" runat="server" ErrorMessage="Monitoring time required."
                    ValidationGroup="entry" ControlToValidate="txtMonitoringDateTime" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator></td>
        </tr>

        <tr>
            <td class="label">
                Mobilization
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMobilization" runat="server" Width="304px" MaxLength="250" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                Physiotherapy
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtFisiotherapi" runat="server" Width="304px" MaxLength="250" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">Injury Care</td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsInjuryCare" Text="Injury Care" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label" rowspan="2">
                Skin Condition
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSkinCondition" runat="server" Width="304px" MaxLength="250" Visible="False"/>
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="entry" colspan="2">
                <asp:CheckBox runat="server" ID="chkIsSkinRed" Text="Red" />
                <asp:CheckBox runat="server" ID="chkIsSkinComplete" Text="Complete" />
                <asp:CheckBox runat="server" ID="chkIsSkinNoBlister" Text="No Blister" />
                <asp:CheckBox runat="server" ID="chkIsSkinWarm" Text="Warm" />
                <asp:CheckBox runat="server" ID="chkIsSkinHard" Text="Hard" />
                <asp:CheckBox runat="server" ID="chkIsSkinItchy" Text="Itchy" />
            </td>
        </tr>

        <tr>
            <td class="label" rowspan="2">
                Injury Condition
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtInjuryCondition" runat="server" Width="304px" MaxLength="250" Visible="False" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="entry" colspan="2">
                <asp:CheckBox runat="server" ID="chkIsInjuryBlister" Text="Blister" />
                <asp:CheckBox runat="server" ID="chkIsInjuryOpen" Text="Open" />
                <asp:CheckBox runat="server" ID="chkIsInjuryToFat" Text="To Fat" />
                <asp:CheckBox runat="server" ID="chkIsInjuryNekrosis" Text="Nekrotik" />
                <asp:CheckBox runat="server" ID="chkIsInjuryToBone" Text="To Bone and Muscle" />
            </td>
        </tr>

        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsCulture" Text="Culture" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsDxDekubitus" Text="Dx Dekubitus" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                PPA
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMonitoringByName" runat="server" Width="304px" ReadOnly="True" />
            </td>
            <td width="20px">
            </td>
        </tr>
        <tr>
            <td class="label">
                Note
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNote" runat="server" Width="304px" MaxLength="200" />
            </td>
            <td width="20px"></td>
        </tr>

        <tr>
            <td class="labelcaption" colspan="3">Stop Monitoring
            </td>
        </tr>
    </table>
    <telerik:RadAjaxPanel ID="ajaxPanel" runat="server">
        <table width="100%">
            <tr>
                <td class="label">
                    <asp:Label ID="Label4" runat="server" Text="Stop"></asp:Label>
                </td>
                <td class="entry">
                    <asp:RadioButtonList ID="optIsRelease" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="optIsRelease_OnSelectedIndexChanged">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0" Selected="true"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="Label7" runat="server" Text="Stop Date"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadDateTimePicker ID="txtReleaseDateTime" runat="server" Width="170px">
                        <Calendar ID="Calendar1"  runat="server">
                            <SpecialDays>
                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-BackColor="Green">
                                </telerik:RadCalendarDay>
                            </SpecialDays>
                        </Calendar>
                    </telerik:RadDateTimePicker>
                </td>
                <td width="20px"></td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>

</asp:Content>
