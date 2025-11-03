<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserHostPrinterOtherDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.ControlPanel.PrinterManagement.UserHostPrinterOtherDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumUserHostPrinterOther" runat="server" ValidationGroup="UserHostPrinterOther" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="UserHostPrinterOther"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblProgramID" runat="server" Text="Program ID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboProgramID" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboProgramID_ItemDataBound"
                OnItemsRequested="cboProgramID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ProgramID")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "ProgramName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvProgramID" runat="server" ErrorMessage="Program ID required."
                ValidationGroup="entry" ControlToValidate="cboProgramID" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblPrinterID" runat="server" Text="Printer ID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboPrinterID" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPrinterID_ItemDataBound"
                OnItemsRequested="cboPrinterID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "PrinterID")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "PrinterName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvPrinterID" runat="server" ErrorMessage="Printer ID required."
                ValidationGroup="entry" ControlToValidate="cboPrinterID" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="UserHostPrinterOther"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="UserHostPrinterOther" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
