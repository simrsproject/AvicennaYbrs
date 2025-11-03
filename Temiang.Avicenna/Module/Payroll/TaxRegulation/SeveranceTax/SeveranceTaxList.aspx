<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="SeveranceTaxList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.TaxRegulation.SeveranceTaxList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="SeveranceTaxID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SeveranceTaxID" HeaderText="Severance Tax ID"
                    UniqueName="SeveranceTaxID" SortExpression="SeveranceTaxID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                    UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsNPWP" HeaderText="NPWP"
                    UniqueName="IsNPWP" SortExpression="IsNPWP" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LowerLimit" HeaderText="Lower Limit"
                    UniqueName="LowerLimit" SortExpression="LowerLimit" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="UpperLimit" HeaderText="Upper Limit"
                    UniqueName="UpperLimit" SortExpression="UpperLimit" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TaxRate" HeaderText="Tax Rate"
                    UniqueName="TaxRate" SortExpression="TaxRate" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TaxAmount" HeaderText="Tax Amount"
                    UniqueName="TaxAmount" SortExpression="TaxAmount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="AmountOfDeduction"
                    HeaderText="Amount Of Deduction" UniqueName="AmountOfDeduction" SortExpression="AmountOfDeduction"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
