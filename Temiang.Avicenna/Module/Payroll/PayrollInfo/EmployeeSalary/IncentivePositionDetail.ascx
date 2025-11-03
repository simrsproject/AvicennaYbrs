<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncentivePositionDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Payroll.PayrollInfo.IncentivePositionDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeSalaryMatrix" runat="server" ValidationGroup="IncentivePosition" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="IncentivePosition"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr style="display: none">
        <td class="label">
            <asp:Label ID="lblIncentivePositionID" runat="server" Text="ID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtIncentivePositionID" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvIncentivePositionID" runat="server" ErrorMessage="ID required."
                ControlToValidate="txtIncentivePositionID" SetFocusOnError="True" ValidationGroup="IncentivePosition"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
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
                ControlToValidate="txtValidFrom" SetFocusOnError="True" ValidationGroup="IncentivePosition"
                Width="100%">
                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblValidTo" runat="server" Text="Valid To"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" MinDate="01/01/1900"
                MaxDate="12/31/2999" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                ControlToValidate="txtValidTo" SetFocusOnError="True" ValidationGroup="IncentivePosition"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblSRIncentiveServiceUnitGroup" runat="server" Text="Incentive Service Unit Group"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSRIncentiveServiceUnitGroup" runat="server" Width="300px" AllowCustomText="true"
                Filter="Contains" />
        </td>
        <td width="20px">
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblSRIncentivePositionGroup" runat="server" Text="Incentive Position Group"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSRIncentivePositionGroup" runat="server" Width="300px" AllowCustomText="true"
                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSRIncentivePositionGroup_SelectedIndexChanged" />
        </td>
        <td width="20px">
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblSRIncentivePosition" runat="server" Text="Incentive Position"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSRIncentivePosition" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboSRIncentivePosition_ItemDataBound"
                OnItemsRequested="cboSRIncentivePosition_ItemsRequested" OnSelectedIndexChanged="cboSRIncentivePosition_SelectedIndexChanged">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%><br />
                    <%# string.Format("Point: {0}", DataBinder.Eval(Container.DataItem, "NumericValue"))%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblIncentivePositionPoints" runat="server" Text="Incentive Position Points"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtIncentivePositionPoints" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvIncentivePositionPoints" runat="server" ErrorMessage="Incentive Position Points required."
                ControlToValidate="txtIncentivePositionPoints" SetFocusOnError="True" ValidationGroup="IncentivePosition"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="IncentivePosition"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="IncentivePosition" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
