<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BkuDetailItem.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.BkuDetailItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitAutoBillItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitAutoBillItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">Kode Rekening</td>
        <td class="entry">
            <telerik:RadComboBox ID="cboKodeRekening" runat="server" Width="304px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboKodeRekening_ItemDataBound"
                OnItemsRequested="cboKodeRekening_ItemsRequested">
                <FooterTemplate>
                    Note : Show max 20 items                                  
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvKodeRekening" runat="server" ErrorMessage="Kode Rekening required."
                ControlToValidate="cboKodeRekening" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">Posisi</td>
        <td class="entry">
            <asp:RadioButtonList ID="rblJenis" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Debit" Value="D" />
                <asp:ListItem Text="Kredit" Value="K" />
            </asp:RadioButtonList>
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td class="label">Nominal</td>
        <td class="entry">
            <telerik:RadNumericTextBox runat="server" ID="txtNominal" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvNominal" runat="server" ErrorMessage="Nominal required."
                ControlToValidate="txtNominal" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">Kode Item</td>
        <td class="entry">
            <telerik:RadComboBox ID="cboItem" runat="server" Width="304px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboItem_ItemDataBound"
                OnItemsRequested="cboItem_ItemsRequested">
                <FooterTemplate>
                    Note : Show max 20 items                                  
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td class="label">Memo</td>
        <td class="entry">
            <telerik:RadTextBox ID="txtMemo" runat="server" Width="300px" TextMode="MultiLine" />
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitAutoBillItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ServiceUnitAutoBillItem" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;         
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" />
        </td>
    </tr>
</table>
