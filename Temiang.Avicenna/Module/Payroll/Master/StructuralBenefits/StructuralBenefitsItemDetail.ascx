<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StructuralBenefitsItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Payroll.Master.StructuralBenefitsItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumStructuralBenefits" runat="server" ValidationGroup="StructuralBenefits" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="StructuralBenefits"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblPositionID" runat="server" Text="Position"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboPositionID" runat="server" Width="304px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPositionID_ItemDataBound"
                OnItemsRequested="cboPositionID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "PositionCode")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "PositionName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 30 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvPositionID" runat="server" ErrorMessage="Position required."
                ControlToValidate="cboPositionID" SetFocusOnError="True" ValidationGroup="StructuralBenefits"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900"
                MaxDate="12/31/2999" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                ControlToValidate="txtValidFrom" SetFocusOnError="True" ValidationGroup="StructuralBenefits"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="100px" MaxLength="10"
                MaxValue="999999999999" MinValue="0" NumberFormat-DecimalDigits="0" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Amount required."
                ControlToValidate="txtAmount" SetFocusOnError="True" ValidationGroup="StructuralBenefits"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="StructuralBenefits"
                CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'>
            </asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="StructuralBenefits" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
