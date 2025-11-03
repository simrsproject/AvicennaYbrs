<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BloodTransformationItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.BloodBank.Stock.BloodTransformationItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumBloodTransformationItem" runat="server" ValidationGroup="BloodTransformationItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="BloodTransformationItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBagNo" runat="server" Text="Bag No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboBagNo" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboBagNo_ItemDataBound" OnItemsRequested="cboBagNo_ItemsRequested"
                            AutoPostBack="True" OnSelectedIndexChanged="cboBagNo_SelectedIndexChanged">
                            <ItemTemplate>
                                <b><%# DataBinder.Eval(Container.DataItem, "BagNo")%></b>&nbsp;&nbsp;[
                                <i>ED:&nbsp;<%# DataBinder.Eval(Container.DataItem, "ExpiredDateTime", "{0:dd-MMM-yyyy HH:mm}")%></i>]
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvBagNo" runat="server" ErrorMessage="Bag No required."
                            ControlToValidate="cboBagNo" SetFocusOnError="True" ValidationGroup="BloodTransformationItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBloodType" runat="server" Text="Blood Type / Rhesus"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadComboBox runat="server" ID="cboSRBloodType" Width="105px" AllowCustomText="true"
                                        Filter="Contains" Enabled="False">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 5px">
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblBloodRhesus" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" Enabled="False">
                                        <asp:ListItem Value="0" Text="+" Selected="True" />
                                        <asp:ListItem Value="1" Text="-" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRBloodType" runat="server" ErrorMessage="Blood Type required."
                            ValidationGroup="entry" ControlToValidate="cboSRBloodType" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRBloodGroupFrom" runat="server" Text="Blood Group From"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSRBloodGroupFrom" Width="300px" AllowCustomText="true"
                            Filter="Contains" Enabled="False">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRBloodGroupFrom" runat="server" ErrorMessage="Blood Group From required."
                            ValidationGroup="entry" ControlToValidate="cboSRBloodGroupFrom" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRBloodGroupTo" runat="server" Text="Blood Group To"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSRBloodGroupTo" Width="300px" AllowCustomText="true"
                            Filter="Contains">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRBloodGroupTo" runat="server" ErrorMessage="Blood Group To required."
                            ValidationGroup="entry" ControlToValidate="cboSRBloodGroupTo" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblVolumeBag" runat="server" Text="Volume"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtVolumeBag" runat="server" Width="100px" NumberFormat-DecimalDigits="0" MinValue="0" />
                                </td>
                                <td style="width: 5px"></td>
                                <td>ML/CC
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvVolumeBag" runat="server" ErrorMessage="Volume required."
                            ValidationGroup="entry" ControlToValidate="txtVolumeBag" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblExpiredDateTime" runat="server" Text="Expired Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDateTimePicker ID="txtExpiredDateTime" runat="server" AutoPostBackControl="None">
                            <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                            </DateInput>
                            <TimeView ID="TimeView1" runat="server" TimeFormat="HH:mm">
                            </TimeView>
                        </telerik:RadDateTimePicker>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvExpiredDateTime" runat="server" ErrorMessage="Blood ED required."
                            ControlToValidate="txtExpiredDateTime" SetFocusOnError="True" ValidationGroup="BloodTransformationItem"
                            Width="100%">
                            <asp:Image ID="Image21" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="BloodTransformationItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="BloodTransformationItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>