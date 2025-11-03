<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="BkuDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.BkuDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="50%" valign="top">
                <table>
                    <tr>
                        <td class="label">Jenis</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboJenis" Width="304px" AutoPostBack="true" OnSelectedIndexChanged="cboJenis_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Value="1" Text="Transaksi Penerimaan" />
                                    <telerik:RadComboBoxItem Value="2" Text="Transaksi Pengeluaran" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvJenis" runat="server" ErrorMessage="Jenis required."
                                ValidationGroup="entry" ControlToValidate="cboJenis" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Tanggal</td>
                        <td class="entry">
                            <telerik:RadDatePicker runat="server" ID="txtTanggal" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTanggal" runat="server" ErrorMessage="Tanggal required."
                                ValidationGroup="entry" ControlToValidate="txtTanggal" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Kode</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboKode" Width="304px" AutoPostBack="true" OnSelectedIndexChanged="cboKode_SelectedIndexChanged" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboKode_ItemDataBound" OnItemsRequested="cboKode_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 20 items                                  
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvKode" runat="server" ErrorMessage="Kode required."
                                ValidationGroup="entry" ControlToValidate="cboKode" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Nomor</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtNomor" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvNomor" runat="server" ErrorMessage="Nomor required."
                                ValidationGroup="entry" ControlToValidate="txtNomor" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Pelanggan</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboPelanggan" Width="304px" Filter="Contains" AllowCustomText="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPelanggan" runat="server" ErrorMessage="Pelanggan required."
                                ValidationGroup="entry" ControlToValidate="cboPelanggan" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Unit</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboUnit" Width="304px" Filter="Contains" AllowCustomText="true" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Kas/Bank&nbsp;<asp:Label runat="server" ID="lblKasBank" /></td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboKasBank" Width="304px" Filter="Contains" AllowCustomText="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Kas/Bank required."
                                ValidationGroup="entry" ControlToValidate="cboKasBank" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Uraian</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtUraian" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top"></td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdDetail" runat="server" OnNeedDataSource="grdDetail_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdDetail_UpdateCommand"
        OnDeleteCommand="grdDetail_DeleteCommand" OnInsertCommand="grdDetail_InsertCommand"
        ShowFooter="true">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="Id, Nomor">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="35px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="ChartOfAccountCode" HeaderText="Kode Rekening"
                    UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Nama Item" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Memo" HeaderText="Memo" UniqueName="Memo"
                    SortExpression="Memo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Posisi" HeaderText="Posisi" UniqueName="Posisi"
                    SortExpression="Posisi" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Nominal" HeaderText="Nominal" UniqueName="Nominal"
                    SortExpression="Nominal" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="BkuDetailItem.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="BkuDetailItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
