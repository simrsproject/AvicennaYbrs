<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="ParamedicFeeRemunByIdiSettingDetail.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Master.ParamedicFeeRemunByIdiSettingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Paramedic" Width="100px"></asp:Label>
                            <asp:HiddenField ID="hfId" runat="server" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedic" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label11" runat="server" Text="Smf" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSmf" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="Item Group" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItemGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>

                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label9" runat="server" Text="Item"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItem" runat="server" Width="300px" 
                                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                OnItemDataBound="cboItem_ItemDataBound"
                                OnItemsRequested="cboItem_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label7" runat="server" Text="Multiplier Value"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtMultiplierValue" runat="server" Type="Number" NumberFormat-DecimalDigits="2"
                                Width="100px" MinValue="0" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Multiplier value required."
                                ControlToValidate="txtMultiplierValue" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
