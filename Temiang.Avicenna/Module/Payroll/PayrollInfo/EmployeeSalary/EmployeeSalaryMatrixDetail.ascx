<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeSalaryMatrixDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Payroll.PayrollInfo.EmployeeSalaryMatrixDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeSalaryMatrix" runat="server" ValidationGroup="EmployeeSalaryMatrix" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeSalaryMatrix"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr style="display: none">
        <td class="label">
            <asp:Label ID="lblEmployeeSalaryMatrixID" runat="server" Text="Employee Salary Matrix ID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtEmployeeSalaryMatrixID" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvEmployeeSalaryMatrixID" runat="server" ErrorMessage="Employee Salary Matrix ID required."
                ControlToValidate="txtEmployeeSalaryMatrixID" SetFocusOnError="True" ValidationGroup="EmployeeSalaryMatrix"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblSalaryComponentID" runat="server" Text="Salary Component Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSalaryComponentID" runat="server" Width="304px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSalaryComponentID_ItemDataBound"
                OnItemsRequested="cboSalaryComponentID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "SalaryComponentCode")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "SalaryComponentName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSalaryComponentID" runat="server" ErrorMessage="Salary Component ID required."
                ControlToValidate="cboSalaryComponentID" SetFocusOnError="True" ValidationGroup="EmployeeSalaryMatrix"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblQty" runat="server" Text="Qty"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvQty" runat="server" ErrorMessage="Qty required."
                ControlToValidate="txtQty" SetFocusOnError="True" ValidationGroup="EmployeeSalaryMatrix"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblNominalAmount" runat="server" Text="Nominal Amount"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtNominalAmount" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvNominalAmount" runat="server" ErrorMessage="Nominal Amount required."
                ControlToValidate="txtNominalAmount" SetFocusOnError="True" ValidationGroup="EmployeeSalaryMatrix"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblSRCurrencyCode" runat="server" Text="Currency Code"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSRCurrencyCode" runat="server" Width="304px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSRCurrencyCode" runat="server" ErrorMessage="Currency Code required."
                ControlToValidate="cboSRCurrencyCode" SetFocusOnError="True" ValidationGroup="EmployeeSalaryMatrix"
                Width="100%">
                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeSalaryMatrix"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="EmployeeSalaryMatrix" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
