<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemTariffRequest2ItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Tariff.ItemTariffRequest2ItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemTariffRequest2Item" runat="server" ValidationGroup="ItemTariffRequest2Item" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemTariffRequest2Item"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry" valign="middle">
                        <telerik:RadComboBox ID="cboItemID" runat="server" Width="300px" EnableLoadOnDemand="True"
                            HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboItemID_ItemDataBound"
                            OnItemsRequested="cboItemID_ItemsRequested" AutoPostBack="True" OnSelectedIndexChanged="cboItemID_SelectedIndexChanged">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ItemName") %>
                                &nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "ItemID")%>) </b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 result
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                            ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="ItemTariffRequest2Item"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="tr1" runat="server">
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsItemAllowDiscount" runat="server" Text="Allow Discount" Enabled="False" />
                        <asp:CheckBox ID="chkIsItemAllowVariable" runat="server" Text="Allow Variable" Enabled="False" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="trCitoFromStdRef" runat="server">
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsCitoFromStandardReference" runat="server" Text="Cito From Standard Reference" Enabled="False" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="trCito" runat="server">
                    <td class="label">
                        <asp:Label ID="lblCitoValue" runat="server" Text="Cito Value"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtCitoValue" runat="server" Width="100px" />
                        &nbsp;
                        <asp:CheckBox ID="chkIsCitoInPercent" runat="server" Text="In Percent" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvCitoValue" runat="server" ErrorMessage="Cito Value required."
                            ControlToValidate="txtCitoValue" SetFocusOnError="True" ValidationGroup="ItemTariffRequest2Item"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
                    </td>
                    <td class="entry" valign="middle">
                        <telerik:RadComboBox ID="cboClassID" runat="server" Width="300px" EnableLoadOnDemand="True"
                            HighlightTemplatedItems="True" MarkFirstMatch="True" AutoPostBack="True" OnSelectedIndexChanged="cboClassID_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvClassID" runat="server" ErrorMessage="Class required."
                            ControlToValidate="cboClassID" SetFocusOnError="True" ValidationGroup="ItemTariffRequest2Item"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemTariffRequest2Item"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ItemTariffRequest2Item" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%">
            <telerik:RadGrid ID="grdItemTariffRequest2ItemComp" runat="server" AutoGenerateColumns="False"
                SkinID="none" GridLines="None" OnItemCreated="grdItemTariffRequest2ItemComp_ItemCreated">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TariffComponentID" SkinID="none">
                    <Columns>
                        <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Component" UniqueName="TariffComponentName"
                            SortExpression="TariffComponentName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderText="Price" UniqueName="Price">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <telerik:RadNumericTextBox runat="server" Visible="true" ID="txtComponentPrice" Width="100px">
                                </telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn>
                            <HeaderStyle Width="20px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:RequiredFieldValidator ID="rfvComponentPrice" runat="server" ErrorMessage="Price required."
                                    ControlToValidate="txtComponentPrice" SetFocusOnError="True" ValidationGroup="ItemTariffRequest2Item"
                                    Width="100%">
                                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Discount" UniqueName="IsAllowDiscount">
                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkIsAllowDiscount" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Variable" UniqueName="IsAllowVariable">
                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkIsAllowVariable" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="false">
                    <Resizing AllowColumnResize="false" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
