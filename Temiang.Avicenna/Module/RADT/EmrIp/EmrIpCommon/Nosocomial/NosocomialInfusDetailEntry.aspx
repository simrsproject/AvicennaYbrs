<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="NosocomialInfusDetailEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.NosocomialInfusDetailEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="100%">
        <tr>
            <td class="label">Date
            </td>
            <td class="entry">
                <telerik:RadDateTimePicker ID="txtMonitoringDateTime" runat="server" Width="170px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rf1" runat="server" ErrorMessage="Monitoring time required."
                    ValidationGroup="entry" ControlToValidate="txtMonitoringDateTime" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="labelcaption" colspan="3">Replacement
            </td>
        </tr>
        <tr>
            <td class="label">Chateter Type
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboSRIVChateter" Width="304px" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">Set Infus
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboSRInfusSet" Width="304px" />
            </td>
            <td width="20px"></td>
        </tr>

        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsSetBlood" Text="Set Blood" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Medicine / Liquid"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMedicineAndLiquid" runat="server" Width="304px" MaxLength="200" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label6" runat="server" Text="Liquid Type"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtLiquidType" runat="server" Width="304px" MaxLength="200" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Medication Method"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMedicationMethod" runat="server" Width="304px" MaxLength="200" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td colspan="3">
                <br />
            </td>
        </tr>
        <tr>
            <td class="labelcaption" colspan="3">Location
            </td>
        </tr>
        <tr>
            <td class="entry" colspan="2" style="padding-left: 20px;">
                <telerik:RadAutoCompleteBox ID="acbInfusLocation" runat="server" Width="100%" DropDownHeight="150"
                    EmptyMessage="Infus Location">
                </telerik:RadAutoCompleteBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rf2" runat="server" ErrorMessage="Location required."
                    ValidationGroup="entry" ControlToValidate="acbInfusLocation" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <br />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td class="labelcaption" colspan="2">HAIs Risk
            </td>
        </tr>
        <tr>
            <td style="width: 50%; padding-left: 20px;">
                <table width="100%">
                    <tr>
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsTempAbove38" Text="Temperatur >38/<=35 C" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsApneu" Text="Apneu / Asw / Suffocated" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsRedness" Text="Redness" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsPain" Text="Pain" />
                        </td>
                        <td width="20px"></td>
                    </tr>

                    <tr style="display: none">
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsFeelingHot" Text="Feeling Hot" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsSwollen" Text="Swollen" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkVeinHarden" Text="Vein Harden" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsPus" Text="Pus" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr style="display: none">
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsDirty" Text="Dirty" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsKanulaCulture" Text="Kanula Culture" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsShivers" Text="Shivers" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsInfected" Text="Infected" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>




    <table width="100%">
        <tr>
            <td colspan="3">
                <br />
            </td>
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
                <asp:Label ID="Label5" runat="server" Text="Notes"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNotes" runat="server" Width="304px" MaxLength="1000" TextMode="MultiLine" />
            </td>
            <td width="20px"></td>
        </tr>

        <tr>
            <td class="labelcaption" colspan="3">Infus Release
            </td>
        </tr>
    </table>
    <telerik:RadAjaxPanel runat="server">
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
