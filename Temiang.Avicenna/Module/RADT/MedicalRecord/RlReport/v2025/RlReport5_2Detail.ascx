<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RlReport5_2Detail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.RlReport.RlReport5_2Detail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumRlReport5_2" runat="server" ValidationGroup="RlReport5_2" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="RlReport5_2"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblDiagnoseID" runat="server" Text="Kode ICD X"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboDiagnoseID" runat="server" Width="304px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboDiagnoseID_ItemDataBound"
                OnItemsRequested="cboDiagnoseID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "DiagnoseName") %>
                    </b>
                    <br />
                    Diagnosis ID :
                    <%# DataBinder.Eval(Container.DataItem, "DiagnoseID")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20">
            <asp:RequiredFieldValidator ID="rfvDiagnoseID" runat="server" ErrorMessage="Kode ICD X required."
                ValidationGroup="RlReport5_2" ControlToValidate="cboDiagnoseID" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblKasusBaruL" runat="server" Text="Kasus Baru Lk"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtKasusBaruL" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvKasusBaruL" runat="server" ErrorMessage="Kasus Baru Lk required."
                ControlToValidate="txtKasusBaruL" SetFocusOnError="True" ValidationGroup="RlReport5_2"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblKasusBaruP" runat="server" Text="Kasus Baru Pr"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtKasusBaruP" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvKasusBaruP" runat="server" ErrorMessage="Kasus Baru Pr required."
                ControlToValidate="txtKasusBaruP" SetFocusOnError="True" ValidationGroup="RlReport5_2"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblJumlahKasusBaru" runat="server" Text="Jumlah Kasus Baru"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtJumlahKasusBaru" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvJumlahKasusBaru" runat="server" ErrorMessage="Jumlah Kasus Baru required."
                ControlToValidate="txtJumlahKasusBaru" SetFocusOnError="True" ValidationGroup="RlReport5_2"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblKunjunganL" runat="server" Text="Kunjungan Lk"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtKunjunganL" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvKunjunganL" runat="server" ErrorMessage="Kunjungan Lk required."
                ControlToValidate="txtKunjunganL" SetFocusOnError="True" ValidationGroup="RlReport5_2"
                Width="100%">
                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblKunjunganP" runat="server" Text="Kunjungan Pr"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtKunjunganP" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvKunjunganP" runat="server" ErrorMessage="Kunjungan Pr required."
                ControlToValidate="txtKunjunganL" SetFocusOnError="True" ValidationGroup="RlReport5_2"
                Width="100%">
                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblJumlahKunjungan" runat="server" Text="JumlahKunjungan"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtJumlahKunjungan" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvJumlahKunjungan" runat="server" ErrorMessage="Jumlah Kunjungan required."
                ControlToValidate="txtJumlahKunjungan" SetFocusOnError="True" ValidationGroup="RlReport5_2"
                Width="100%">
                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td class="entry" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="RlReport5_2"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="RlReport5_2" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
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