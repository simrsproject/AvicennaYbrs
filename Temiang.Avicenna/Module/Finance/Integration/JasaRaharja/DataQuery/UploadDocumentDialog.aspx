<%@ Page Title="Upload Document" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="UploadDocumentDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Integration.JasaRaharja.UploadDocumentDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" witdh="100%">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Sifat Cedera
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSifatCedera" Width="304px">
                                <Items>
                                    <telerik:RadComboBoxItem Value="01" Text="Meninggal Dunia" />
                                    <telerik:RadComboBoxItem Value="02" Text="Luka-luka" />
                                    <telerik:RadComboBoxItem Value="04" Text="Cacat Tetap" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Jenis Tindakan
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtJenisTindakan" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Dokter Berwenang
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboDokterBerwenang" Width="304px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Biaya Perawatan
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox runat="server" ID="txtBiayaPerawatan" Width="100px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdList_UpdateCommand"
        OnDeleteCommand="grdList_DeleteCommand" OnInsertCommand="grdList_InsertCommand">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID">
            <Columns>
                <telerik:GridNumericColumn DataField="NAMA_FILE" HeaderText="NAMA FILE" UniqueName="NAMA_FILE"
                    SortExpression="NAMA_FILE" />
                <telerik:GridNumericColumn DataField="DESKRIPSI_NAMA" HeaderText="DESKRIPSI" UniqueName="DESKRIPSI_NAMA"
                    SortExpression="DESKRIPSI_NAMA" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="JasaRaharjaUploadItem.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="JasaRaharjaUploadItemCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
