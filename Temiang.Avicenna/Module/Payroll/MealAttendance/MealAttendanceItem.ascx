<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MealAttendanceItem.ascx.cs" Inherits="Temiang.Avicenna.Module.Payroll.MealAttendanceItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPersonalContact" runat="server" ValidationGroup="PersonalContact" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PersonalContact"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table>
    <tr>
        <td class="label">Meal Datetime</td>
        <td class="entry">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadDatePicker runat="server" ID="txtOpenDate" Width="100px">
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                        <telerik:RadMaskedTextBox ID="txtOpenTime" runat="server" Mask="<00..23>:<00..59>"
                            PromptChar="_" RoundNumericRanges="false" Width="100px">
                        </telerik:RadMaskedTextBox>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Meal Date required."
                ValidationGroup="PersonalContact" ControlToValidate="txtOpenDate" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Meal Time required."
                ValidationGroup="PersonalContact" ControlToValidate="txtOpenTime" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">Employee Name</td>
        <td class="entry">
            <telerik:RadComboBox ID="cboPersonID" runat="server" Width="304px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                OnItemsRequested="cboPersonID_ItemsRequested">
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
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Employee Name required."
                ValidationGroup="PersonalContact" ControlToValidate="cboPersonID" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PersonalContact"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="PersonalContact" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
