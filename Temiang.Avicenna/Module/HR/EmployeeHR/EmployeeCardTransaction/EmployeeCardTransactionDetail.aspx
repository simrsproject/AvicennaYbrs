<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="EmployeeCardTransactionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeeCardTransactionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label">Employee Name</td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPersonID" runat="server" Width="304px" EnableLoadOnDemand="true"
                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                    OnItemsRequested="cboPersonID_ItemsRequested" AutoPostBack="True" OnSelectedIndexChanged="cboPersonID_OnSelectedIndexChanged">
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Working Hour Name required."
                    ValidationGroup="entry" ControlToValidate="txtDate" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator></td>
            <td />
        </tr>
        <tr>
            <td class="label">Date</td>
            <td class="entry">
                <telerik:RadDatePicker runat="server" ID="txtDate" Width="100px">
                </telerik:RadDatePicker>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Working Hour Name required."
                    ValidationGroup="entry" ControlToValidate="txtDate" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">Old Card ID</td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtOldCardID" Width="300px"></telerik:RadTextBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Old Card ID required."
                    ValidationGroup="entry" ControlToValidate="txtOldCardID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator></td>
            <td />
        </tr>
        <tr>
            <td class="label">New Card ID</td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtNewCardID" Width="300px"></telerik:RadTextBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="New Card ID required."
                    ValidationGroup="entry" ControlToValidate="txtNewCardID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator></td>
            <td />
        </tr>
        <tr>
            <td class="label">Notes</td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtNotes" Width="300px" TextMode="MultiLine"></telerik:RadTextBox>
            </td>
            <td width="20px"></td>
            <td />
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server">
        <MasterTableView DataKeyNames="EmployeeCardTransactionID" AutoGenerateColumns="False">
            <Columns>
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="Datetime" HeaderText="Datetime"
                    UniqueName="Datetime" SortExpression="Datetime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="OldCardID" HeaderText="Old Card ID"
                    UniqueName="OldCardID" SortExpression="OldCardID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="NewCardID" HeaderText="New Card ID"
                    UniqueName="NewCardID" SortExpression="NewCardID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes"
                    UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
