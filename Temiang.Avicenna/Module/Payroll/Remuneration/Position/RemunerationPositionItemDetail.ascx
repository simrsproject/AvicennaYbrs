<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RemunerationPositionItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Payroll.RemunerationPosition.RemunerationPositionItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeWageStructureAndScalePositionItem" runat="server" ValidationGroup="EmployeeWageStructureAndScalePositionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeWageStructureAndScalePositionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblWageStructureAndScalePositionItemID" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtWageStructureAndScalePositionItemID" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRWageStructureAndScaleType" runat="server" Text="Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRWageStructureAndScaleType" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSRWageStructureAndScaleType_SelectedIndexChanged"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRWageStructureAndScaleType" runat="server" ErrorMessage="Type required."
                            ControlToValidate="cboSRWageStructureAndScaleType" SetFocusOnError="True" ValidationGroup="EmployeeWageStructureAndScalePositionItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblWageStructureAndScaleID" runat="server" Text="Wage Structure And Scale"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboWageStructureAndScaleID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboWageStructureAndScaleID_ItemDataBound"
                            OnItemsRequested="cboWageStructureAndScaleID_ItemsRequested" OnSelectedIndexChanged="cboWageStructureAndScaleID_SelectedIndexChanged">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "WageStructureAndScaleCode")%>
                            &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "WageStructureAndScaleName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvWageStructureAndScaleID" runat="server" ErrorMessage="Wage Structure And Scale required."
                            ControlToValidate="cboWageStructureAndScaleID" SetFocusOnError="True" ValidationGroup="EmployeeWageStructureAndScalePositionItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblWageStructureAndScaleItemID" runat="server" Text="Wage Structure And Scale Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboWageStructureAndScaleItemID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboWageStructureAndScaleItemID_ItemDataBound"
                            OnItemsRequested="cboWageStructureAndScaleItemID_ItemsRequested" OnSelectedIndexChanged="cboWageStructureAndScaleItemID_SelectedIndexChanged">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "SRWageStructureAndScaleItem")%>
                            &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "WageStructureAndScaleItemName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSREmployeeWageStructureAndScalePositionItem" runat="server" ErrorMessage="Wage Structure And Scale Item required."
                            ControlToValidate="cboWageStructureAndScaleItemID" SetFocusOnError="True" ValidationGroup="EmployeeWageStructureAndScalePositionItem"
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
                            ControlToValidate="txtPoints" SetFocusOnError="True" ValidationGroup="EmployeeWageStructureAndScalePositionItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeWageStructureAndScalePositionItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeeWageStructureAndScalePositionItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
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
