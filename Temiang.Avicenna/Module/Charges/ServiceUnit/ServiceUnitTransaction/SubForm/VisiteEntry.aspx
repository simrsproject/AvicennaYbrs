<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master"
    AutoEventWireup="true" CodeBehind="VisiteEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitTransaction.VisiteEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEntry" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                Item
            </td>
            <td class="entry">
                <asp:Label ID="lblItemName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                Notes
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNotes" runat="server" TextMode="MultiLine" MaxLength="500"
                    Width="100%" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                Visit Order Qty
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtVisitQty" runat="server" MinValue="0" NumberFormat-DecimalDigits="0"
                    Width="30px" />
            </td>
            <td>
            </td>
        </tr>
    </table>
    <fieldset>
        <legend>Charge Info</legend>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="label">
                    Amount
                </td>
                <td class="entry">
                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                </td>
                <td class="label">
                    Charge Qty
                </td>
                <td class="entry">
                    <asp:Label ID="lblChargeQuantity" runat="server"></asp:Label>
                </td>
                <td class="label">
                    Discount (%)
                </td>
                <td class="entry">
                    <asp:Label ID="lblDiscountAmount" runat="server"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset>
        <legend>Visite Realization History</legend>
        <table width="100%">
            <tr>
                <td class="label">
                    Visit Realization
                </td>
                <td style="width: 30px">
                    <asp:Label ID="lblRealizationQty" runat="server"></asp:Label>
                </td>
                <td style="width: 20px">
                    of
                </td>
                <td style="width: 30px">
                    <asp:Label ID="lblVisitQtyInfo" runat="server"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
            GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
            AllowSorting="true">
            <MasterTableView Name="master">
                <Columns>
                    <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                        SortExpression="RegistrationDate">
                        <HeaderStyle HorizontalAlign="Center" Width="75px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                        UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                        <HeaderStyle HorizontalAlign="Center" Width="130px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                        SortExpression="ServiceUnitName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                        SortExpression="ParamedicName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </fieldset>
</asp:Content>
