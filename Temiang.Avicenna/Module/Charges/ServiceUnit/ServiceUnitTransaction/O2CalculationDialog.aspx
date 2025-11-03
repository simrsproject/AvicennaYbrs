<%@ Page Title="O2 Calculation" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="O2CalculationDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.O2CalculationDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnCalculate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalUsage" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <legend></legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 100%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblStartDateTime" runat="server" Text="From"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadDateTimePicker ID="txtStartDateTime" runat="server" AutoPostBackControl="None">
                                                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                                </DateInput>
                                                <TimeView ID="TimeView1" runat="server" TimeFormat="HH:mm">
                                                </TimeView>
                                            </telerik:RadDateTimePicker>
                                        </td>
                                        <td style="width=20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEndDateTime" runat="server" Text="To"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadDateTimePicker ID="txtEndDateTime" runat="server" AutoPostBackControl="None">
                                                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                                </DateInput>
                                                <TimeView ID="TimeView2" runat="server" TimeFormat="HH:mm">
                                                </TimeView>
                                            </telerik:RadDateTimePicker>
                                        </td>
                                        <td style="width=20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblUsagePerMinute" runat="server" Text="O2 Usage"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadNumericTextBox ID="txtUsagePerMinute" runat="server" Width="100px">
                                                <NumberFormat DecimalDigits="2" />
                                            </telerik:RadNumericTextBox>&nbsp;/&nbsp;Minute
                                        </td>
                                        <td style="width=20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="Label1" runat="server" Text="Total O2 Usage"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtTotalUsage" runat="server" Width="100px" ReadOnly="true">
                                                            <NumberFormat DecimalDigits="2" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                    <td>&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:ImageButton ID="btnCalculate" runat="server" ImageUrl="../../../../Images/Toolbar/refresh16.png"
                                                            CausesValidation="False" OnClick="btnCalculate_Click" ToolTip="Calculate" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width=20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
