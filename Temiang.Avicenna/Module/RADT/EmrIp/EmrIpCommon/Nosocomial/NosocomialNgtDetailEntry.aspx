<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="NosocomialNgtDetailEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.NosocomialNgtDetailEntry" %>

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
            <td class="label">Replacement
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtReplacement" runat="server" Width="304px" MaxLength="100" />
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
                <asp:CheckBox runat="server" ID="chkIsTempAbove38" Text="Temperatur > 38 C" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsPus" Text="Pus (Nose / Throat)" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsHeadache" Text="Headache" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsNoseClogged" Text="Nose Clogged" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsPainswallow" Text="Pain Swallow" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsPharingRedness" Text="Pharing Redness" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsCough" Text="Cough" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsTransillumination" Text="Transillumination" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsCulture" Text="Culture" />
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
