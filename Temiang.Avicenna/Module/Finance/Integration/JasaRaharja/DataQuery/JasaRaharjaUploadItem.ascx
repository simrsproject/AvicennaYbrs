<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JasaRaharjaUploadItem.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Integration.JasaRaharja.JasaRaharjaUploadItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemProductMarginValue" runat="server" ValidationGroup="ItemProductMarginValue" />
<table width="100%">
    <tr>
        <td class="label">
            NAMA FILE
        </td>
        <td class="entry">
            <asp:FileUpload ID="fileUpload1" runat="server" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvStartingValue" runat="server" ErrorMessage="NAMA FILE required."
                ControlToValidate="fileUpload1" SetFocusOnError="True" ValidationGroup="ItemProductMarginValue"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            DESKRIPSI
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboDeskripsi" Width="300px">
                <Items>
                    <telerik:RadComboBoxItem Value="06" Text="KTP" />
                    <telerik:RadComboBoxItem Value="09" Text="Kartu Keluarga" />
                    <telerik:RadComboBoxItem Value="12" Text="Billing / Kwitansi" />
                    <telerik:RadComboBoxItem Value="15" Text="Surat Kuasa" />
                    <telerik:RadComboBoxItem Value="22" Text="Rekam Medis" />
                    <telerik:RadComboBoxItem Value="14" Text="Surat Rujukan" />
                    <telerik:RadComboBoxItem Value="16" Text="Foto Rontgen" />
                    <telerik:RadComboBoxItem Value="19" Text="Surat Keterangan Cacat" />
                </Items>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td class="entry" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemProductMarginValue"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ItemProductMarginValue" Visible='<%# DataItem is GridInsertionObject %>'
                OnClick="btnInsert_Click"></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
        <td width="20">
        </td>
        <td>
        </td>
    </tr>
</table>
