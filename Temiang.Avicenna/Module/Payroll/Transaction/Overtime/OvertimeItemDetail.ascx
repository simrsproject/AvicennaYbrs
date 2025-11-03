<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OvertimeItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Payroll.Transaction.OvertimeItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeOvertimeItem" runat="server" ValidationGroup="EmployeeOvertimeItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeOvertimeItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblPersonID" runat="server" Text="Employee Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboPersonID" runat="server" Width="304px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                OnItemsRequested="cboPersonID_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboPersonID_SelectedIndexChanged">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvPersonID" runat="server" ErrorMessage="Employee Name required."
                ValidationGroup="entry" ControlToValidate="cboPersonID" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image25" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr runat="server" id="trWorkingHour" visible="false">
        <td class="label">Working Hour
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboWorkingHour" runat="server" Width="304px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvWorkingHour" runat="server" ErrorMessage="Working Hour required."
                ValidationGroup="entry" ControlToValidate="cboPersonID" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblSalaryComponetID" runat="server" Text="Salary Componet Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSalaryComponetID" runat="server" Width="304px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSalaryComponetID_ItemDataBound"
                OnItemsRequested="cboSalaryComponetID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "SalaryComponentCode")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "SalaryComponentName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSalaryComponetID" runat="server" ErrorMessage="Salary Componet Name required."
                ValidationGroup="entry" ControlToValidate="cboSalaryComponetID" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr height="30">
        <td class="label">
            <asp:Label ID="lblAmount" runat="server" Text="Quantity (in Hour)"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="100px" MaxLength="10"
                MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Quantity required."
                ControlToValidate="txtAmount" SetFocusOnError="True" ValidationGroup="EmployeeOvertimeItem"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
        </td>
        <td class="entry">
             <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="1000" TextMode="MultiLine" />
        </td>
        <td width="20px">
        </td>
        <td></td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeOvertimeItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="EmployeeOvertimeItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
