<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="LaundryItemsDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Master.LaundryItemsDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboFromServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboFromLocation" />
                    <telerik:AjaxUpdatedControl ControlID="cboAssetID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboFromLocation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboAssetID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboAssetID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtSerialNumber" />
                    <telerik:AjaxUpdatedControl ControlID="txtAssetGroup" />
                    <telerik:AjaxUpdatedControl ControlID="txtPurchaseDate2" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRAssetsStatusFrom" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsFixedAssetFrom" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsFixedAssetTo" />
                    <telerik:AjaxUpdatedControl ControlID="txtCurrentValue" />
                    <telerik:AjaxUpdatedControl ControlID="txtDepreciationAccValue" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRAssetsStatusTo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCurrentValue" />
                    <telerik:AjaxUpdatedControl ControlID="txtDepreciationAccValue" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsFixedAssetTo" />
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
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemName" runat="server" Text="Item Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRItemUnit" runat="server" Text="Item Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSRItemUnit" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNote" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" TextMode="MultiLine" runat="server" Width="300px"
                                MaxLength="500" Height="60px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry" style="height: 15px;">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Enabled="False" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%" cellpadding="0" cellspacing="1">
        <tr>
            <td>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label3" runat="server" Text="WEIGHT" Font-Bold="True"
                            Font-Size="9"></asp:Label></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">Weight
                                        </td>
                                        <td class="entry" style="height: 15px;">
                                            <telerik:RadNumericTextBox ID="txtWeight" runat="server" Value="0" Width="100px">
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                            <telerik:RadTextBox ID="txtGram" Text="Gram" runat="server" Width="50px" ReadOnly="true" />

                                        </td>
                                        <td style="height: 15px; width: 20px;"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
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
