<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WageStructureAndScalePointsItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.WageStructureAndScalePointsItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumWageStructureAndScaleItem" runat="server" ValidationGroup="WageStructureAndScaleItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="WageStructureAndScaleItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblWageStructureAndScaleItemID" runat="server" Text="WageStructureAndScaleItemID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtWageStructureAndScaleItemID" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRWageStructureAndScaleItem" runat="server" Text="Item Name"></asp:Label>
                    </td>
                    <td class="entry">

                        <telerik:RadComboBox ID="cboSRWageStructureAndScaleItem" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSRWageStructureAndScaleItem_ItemDataBound"
                            OnItemsRequested="cboSRWageStructureAndScaleItem_ItemsRequested">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRWageStructureAndScaleItem" runat="server" ErrorMessage="Item Name required."
                            ControlToValidate="cboSRWageStructureAndScaleItem" SetFocusOnError="True" ValidationGroup="WageStructureAndScaleItem"
                            Width="100%">
                            <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPoints" runat="server" Text="Points"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPoints" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPoints" runat="server" ErrorMessage="Points required."
                            ControlToValidate="txtPoints" SetFocusOnError="True" ValidationGroup="WageStructureAndScaleItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="WageStructureAndScaleItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="WageStructureAndScaleItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
