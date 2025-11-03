<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="CloseClinicDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.CloseClinic.CloseClinicDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtStartTime1" />
                    <telerik:AjaxUpdatedControl ControlID="txtEndTime1" />
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime1" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime1" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime1" />
                    <telerik:AjaxUpdatedControl ControlID="txtStartTime2" />
                    <telerik:AjaxUpdatedControl ControlID="txtEndTime2" />
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime2" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime2" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime2" />
                    <telerik:AjaxUpdatedControl ControlID="txtStartTime3" />
                    <telerik:AjaxUpdatedControl ControlID="txtEndTime3" />
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime3" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime3" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime3" />
                    <telerik:AjaxUpdatedControl ControlID="txtStartTime4" />
                    <telerik:AjaxUpdatedControl ControlID="txtEndTime4" />
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime4" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime4" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime4" />
                    <telerik:AjaxUpdatedControl ControlID="txtStartTime5" />
                    <telerik:AjaxUpdatedControl ControlID="txtEndTime5" />
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime5" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime5" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime5" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnClosedTime1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime1" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime1" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnClosedTime2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime2" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime2" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnClosedTime3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime3" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime3" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnClosedTime4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime4" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime4" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime4" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnClosedTime5">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime5" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime5" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime5" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnOpenTime1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime1" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime1" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnOpenTime2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime2" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime2" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnOpenTime3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime3" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime3" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnOpenTime4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime4" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime4" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime4" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnOpenTime5">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblStatusTime5" />
                    <telerik:AjaxUpdatedControl ControlID="btnClosedTime5" />
                    <telerik:AjaxUpdatedControl ControlID="btnOpenTime5" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <legend>SCHEDULES</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblScheduleDate" Text="Date" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtScheduleDate" runat="server" Width="110px" Enabled="false">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblServiceUnitID" Text="Service Unit" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table width="100%">
                                    <tr runat="server" visible="false">
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblParamedicID" Text="Physician" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="300px" ReadOnly="true">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr runat="server" visible="false">
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPeriodYear" Text="Year" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPeriodYear" runat="server" Width="300px" ReadOnly="true">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset>
                    <legend>PHYSICIAN OPERATIONAL TIME</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblStartTime1" runat="server" Text="Start Time 1"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="60px">
                                                        <telerik:RadTextBox ID="txtStartTime1" runat="server" Width="50" ReadOnly="true">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td width="20px">
                                                        <asp:Label ID="lblEndTime1" runat="server" Text="To"></asp:Label>
                                                    </td>
                                                    <td width="60px">
                                                        <telerik:RadTextBox ID="txtEndTime1" runat="server" Width="50" ReadOnly="true">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td width="10px"></td>
                                                    <td width="70px">
                                                        <asp:Label ID="lblStatusTime1" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td width="10px">
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnClosedTime1" runat="server" ImageUrl="~/Images/Toolbar/close16.png"
                                                            OnClick="btnClosedTime1_Click" ToolTip="Closed" />
                                                        <asp:ImageButton ID="btnOpenTime1" runat="server" ImageUrl="~/Images/Toolbar/post_green_16.png"
                                                            OnClick="btnOpenTime1_Click" ToolTip="Open" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblStartTime2" runat="server" Text="Start Time 2"></asp:Label></td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="60px">
                                                        <telerik:RadTextBox ID="txtStartTime2" runat="server" Width="50" ReadOnly="true">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td width="20px">
                                                        <asp:Label ID="lblEndTime2" runat="server" Text="To"></asp:Label>
                                                    </td>
                                                    <td width="60px">
                                                        <telerik:RadTextBox ID="txtEndTime2" runat="server" Width="50" ReadOnly="true">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td width="10px"></td>
                                                    <td width="70px">
                                                        <asp:Label ID="lblStatusTime2" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td width="10px">
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnClosedTime2" runat="server" ImageUrl="~/Images/Toolbar/close16.png"
                                                            OnClick="btnClosedTime2_Click" ToolTip="Closed" />
                                                        <asp:ImageButton ID="btnOpenTime2" runat="server" ImageUrl="~/Images/Toolbar/post_green_16.png"
                                                            OnClick="btnOpenTime2_Click" ToolTip="Open" />
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblStartTime3" runat="server" Text="Start Time 3"></asp:Label></td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="60px">
                                                        <telerik:RadTextBox ID="txtStartTime3" runat="server" Width="50" ReadOnly="true">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td width="20px">
                                                        <asp:Label ID="lblEndTime3" runat="server" Text="To"></asp:Label>
                                                    </td>
                                                    <td width="60px">
                                                        <telerik:RadTextBox ID="txtEndTime3" runat="server" Width="50" ReadOnly="true">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td width="10px"></td>
                                                    <td width="70px">
                                                        <asp:Label ID="lblStatusTime3" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td width="10px">
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnClosedTime3" runat="server" ImageUrl="~/Images/Toolbar/close16.png"
                                                            OnClick="btnClosedTime3_Click" ToolTip="Closed" />
                                                        <asp:ImageButton ID="btnOpenTime3" runat="server" ImageUrl="~/Images/Toolbar/post_green_16.png"
                                                            OnClick="btnOpenTime3_Click" ToolTip="Open" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblStartTime4" runat="server" Text="Start Time 4"></asp:Label></td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="60px">
                                                        <telerik:RadTextBox ID="txtStartTime4" runat="server" Width="50" ReadOnly="true">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td width="20px">
                                                        <asp:Label ID="lblEndTime4" runat="server" Text="To"></asp:Label>
                                                    </td>
                                                    <td width="60px">
                                                        <telerik:RadTextBox ID="txtEndTime4" runat="server" Width="50" ReadOnly="true">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td width="10px"></td>
                                                    <td width="70px">
                                                        <asp:Label ID="lblStatusTime4" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td width="10px">
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnClosedTime4" runat="server" ImageUrl="~/Images/Toolbar/close16.png"
                                                            OnClick="btnClosedTime4_Click" ToolTip="Closed" />
                                                        <asp:ImageButton ID="btnOpenTime4" runat="server" ImageUrl="~/Images/Toolbar/post_green_16.png"
                                                            OnClick="btnOpenTime4_Click" ToolTip="Open" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblStartTime5" runat="server" Text="Start Time 5"></asp:Label></td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="60px">
                                                        <telerik:RadTextBox ID="txtStartTime5" runat="server" Width="50" ReadOnly="true">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td width="20px">
                                                        <asp:Label ID="lblEndTime5" runat="server" Text="To"></asp:Label>
                                                    </td>
                                                    <td width="60px">
                                                        <telerik:RadTextBox ID="txtEndTime5" runat="server" Width="50" ReadOnly="true">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td width="10px"></td>
                                                    <td width="70px">
                                                        <asp:Label ID="lblStatusTime5" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td width="10px">
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnClosedTime5" runat="server" ImageUrl="~/Images/Toolbar/close16.png"
                                                            OnClick="btnClosedTime5_Click" ToolTip="Closed" />
                                                        <asp:ImageButton ID="btnOpenTime5" runat="server" ImageUrl="~/Images/Toolbar/post_green_16.png"
                                                            OnClick="btnOpenTime5_Click" ToolTip="Open" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                </table>
                            </td>
                        </tr>

                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
