<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmfDiagnoseDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.SmfDiagnoseDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsum" runat="server" ValidationGroup="SmfDiagnose" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="SmfDiagnose"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblItem" runat="server" Text="Item Service"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboDiagnoseID" Width="300px" AutoPostBack="True"
                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                OnItemDataBound="cboDiagnoseID_ItemDataBound" OnItemsRequested="cboDiagnoseID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "DiagnoseName") %>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 30 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Diagnose ID required."
                ControlToValidate="cboDiagnoseID" SetFocusOnError="True" ValidationGroup="SmfDiagnose"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label"></td>
        <td class="entry">
            <asp:CheckBox ID="chkIsVisible" runat="server" Text="Visible To User Entry" />
        </td>
        <td width="20px"></td>
        <td></td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="SmfDiagnose"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="SmfDiagnose" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;&nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
