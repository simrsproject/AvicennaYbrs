<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RlReport4_3Detail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.RlReport.v2025.RlReport4_3Detail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumRlReport4_3" runat="server" ValidationGroup="RlReport4_3" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="RlReport4_3"
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
                ValidationGroup="RlReport4_3" ControlToValidate="cboDiagnoseID" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblHidupMatiL" runat="server" Text="Pasien Hidup Laki-laki"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtHidupMatiL" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvHidupMatiL" runat="server" ErrorMessage="Pasien Hidup dan Mati Laki-laki required."
                ControlToValidate="txtHidupMatiL" SetFocusOnError="True" ValidationGroup="RlReport4_3"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblHidupMatiP" runat="server" Text="Pasien Hidup Perempuan"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtHidupMatiP" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvHidupMatiP" runat="server" ErrorMessage="Pasien Hidup dan Mati Perempuan required."
                ControlToValidate="txtHidupMatiP" SetFocusOnError="True" ValidationGroup="RlReport4_3"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lbKeluarMatiL" runat="server" Text="Pasien Keluar Mati Laki-laki"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtKeluarMatiL" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvKeluarMatiL" runat="server" ErrorMessage="Pasien Keluar Mati Laki-laki required."
                ControlToValidate="txtKeluarMatiL" SetFocusOnError="True" ValidationGroup="RlReport4_3"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lbKeluarMatiP" runat="server" Text="Pasien Keluar Mati Perempuan"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtKeluarMatiP" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvKeluarMatiP" runat="server" ErrorMessage="Pasien Keluar Mati Perempuan required."
                ControlToValidate="txtKeluarMatiP" SetFocusOnError="True" ValidationGroup="RlReport4_3"
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
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="RlReport4_3"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="RlReport4_3" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
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
