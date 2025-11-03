<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PackagingItemDetailItem.ascx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.PackagingItemDetailItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumCssdPackagingItem" runat="server" ValidationGroup="CssdPackagingItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="CssdPackagingItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr style="display:none">
                    <td class="label">
                        <asp:Label ID="lblReceivedSeqNo" runat="server" Text="Sequence No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtReceivedSeqNo" runat="server" Width="100px" MaxLength="5"
                            Enabled="false" Text="" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvReceivedSeqNo" runat="server" ErrorMessage="Sequence No required."
                            ControlToValidate="txtReceivedSeqNo" SetFocusOnError="True" ValidationGroup="CssdPackagingItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboItemID" Height="190px" Width="350px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboItemID_ItemDataBound" OnItemsRequested="cboItemID_ItemsRequested"
                            AutoPostBack="True" OnSelectedIndexChanged="cboItemID_SelectedIndexChanged">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr height="30">
                    <td class="label">
                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="100px" MaxLength="10"
                                        MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="0" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <telerik:RadComboBox runat="server" ID="cboSRCssdItemUnit" Width="100px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsItemProduction" runat="server" Text="Item Production" Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity required."
                            ControlToValidate="txtQty" SetFocusOnError="True" ValidationGroup="CssdPackagingItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvSRCssdItemUnit" runat="server" ErrorMessage="Unit required."
                            ControlToValidate="cboSRCssdItemUnit" SetFocusOnError="True" ValidationGroup="CssdPackagingItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="CssdPackagingItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="CssdPackagingItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblExpiredDate" runat="server" Text="Expired Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtExpiredDate" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvExpiredDate" runat="server" ErrorMessage="Expired Date required."
                            ControlToValidate="txtExpiredDate" SetFocusOnError="True" ValidationGroup="CssdPackagingItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReuseTo" runat="server" Text="Reuse To"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtReuseTo" runat="server" Width="100px" MaxLength="10"
                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="0" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsNeedUltrasound" runat="server" Text="Need Ultrasound" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr runat="server" id="trOldIsDtt">
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsDtt" runat="server" Text="High Level Disinfection (DTT) Process" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr runat="server" id="trNewIsDtt">
                        <td class="label">
                            <asp:Label ID="lbl" runat="server" Text="Temperature"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblIsDtt" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="0" Text="Low" />
                                <asp:ListItem Value="1" Text="High" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTemperature" runat="server" ErrorMessage="Temperature Type required."
                                ControlToValidate="rblIsDtt" SetFocusOnError="True" ValidationGroup="CssdSterileItemsReceivedItem"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
            </table>
        </td>
    </tr>
</table>
