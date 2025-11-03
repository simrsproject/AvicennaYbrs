<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="NosocomialCatheterDetailEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.NosocomialCatheterDetailEntry" %>

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
            <td class="labelcaption" colspan="3">Replacement
            </td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsUrineBagChange" Text="Urine Bag" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Addition of catheter fixation fluid"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtFixationFluid" runat="server" Width="304px" MaxLength="200" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">General Chateter
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboSRGeneralChateterNo" Width="304px" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">Silicon Catheter
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboSRSiliconChateterNo" Width="304px" />
            </td>
            <td width="20px"></td>
        </tr>

        <tr>
            <td class="labelcaption" colspan="3">HAIs Risk
            </td>
        </tr>

        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsTempAbove38" Text="Temperatur >38/<=35 C" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsApneu" Text="Apneu / Bradikardi" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsDisuria" Text="Disuria Frekuensi / Urgensi" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsPain" Text="Suprapubic Pain" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsPyuria" Text="Pyuria" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsHematuria" Text="Hematuria" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsUrineCulture" Text="Urine Culture" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsIskDiagnose" Text="ISK Diagnose" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsUrineRutin" Text="Urine Rutin" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="PPA"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMonitoringByName" runat="server" Width="304px" ReadOnly="True" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Note"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNote" runat="server" Width="304px" MaxLength="200" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="labelcaption" colspan="3">Release
            </td>
        </tr>
    </table>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <table width="100%">
            <tr>
                <td class="label">
                    <asp:Label ID="Label4" runat="server" Text="Release"></asp:Label>
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
                    <asp:Label ID="Label7" runat="server" Text="Release Date"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadDateTimePicker ID="txtReleaseDateTime" runat="server" Width="170px">
                        <Calendar runat="server">
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
