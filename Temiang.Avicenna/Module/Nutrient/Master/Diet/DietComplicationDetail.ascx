<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DietComplicationDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Nutrient.Master.DietComplicationDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumDietComplication" runat="server" ValidationGroup="DietComplication" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="DietComplication"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblDietID" runat="server" Text="Diet"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboDietID" Width="300px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboDietID_ItemDataBound" OnItemsRequested="cboDietID_ItemsRequested"
                ValidationGroup="other">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "DietName")%>
                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "DietID")%>) </b>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvDietID" runat="server" ErrorMessage="Diet required."
                ControlToValidate="cboDietID" SetFocusOnError="True" ValidationGroup="DietComplication"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td class="entry">
            <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="DietComplication"
                CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'>
            </asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="DietComplication" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
