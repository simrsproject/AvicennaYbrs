<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="AutoNumberList.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.Setting.AppAutoNumberList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="SRAutoNumber, EffectiveDate">
            <Columns>
                <telerik:GridBoundColumn DataField="SRAutoNumber" HeaderText="ID" UniqueName="SRAutoNumber"
                    SortExpression="SRAutoNumber" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EffectiveDate" HeaderText="Effective Date"
                    UniqueName="EffectiveDate" SortExpression="EffectiveDate" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="Prefik" HeaderText="Prefik"
                    UniqueName="Prefik" SortExpression="Prefik" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SeparatorAfterPrefik"
                    HeaderText="Sep." UniqueName="SeparatorAfterPrefik" SortExpression="SeparatorAfterPrefik"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsUsedDepartment"
                    HeaderText="Dep." UniqueName="IsUsedDepartment" SortExpression="IsUsedDepartment"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SeparatorAfterDept"
                    HeaderText="Sep." UniqueName="SeparatorAfterDept" SortExpression="SeparatorAfterDept"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsUsedYear" HeaderText="Year"
                    UniqueName="IsUsedYear" SortExpression="IsUsedYear" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="YearDigit" HeaderText="Y.Digit"
                    UniqueName="YearDigit" SortExpression="YearDigit" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SeparatorAfterYear"
                    HeaderText="Sep." UniqueName="SeparatorAfterYear" SortExpression="SeparatorAfterYear"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsUsedMonth" HeaderText="Month"
                    UniqueName="IsUsedMonth" SortExpression="IsUsedMonth" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsMonthInRomawi"
                    HeaderText="Romawi" UniqueName="IsMonthInRomawi" SortExpression="IsMonthInRomawi"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SeparatorAfterMonth"
                    HeaderText="Sep." UniqueName="SeparatorAfterMonth" SortExpression="SeparatorAfterMonth"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsUsedDay" HeaderText="Day"
                    UniqueName="IsUsedDay" SortExpression="IsUsedDay" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SeparatorAfterDay" HeaderText="Sep."
                    UniqueName="SeparatorAfterDay" SortExpression="SeparatorAfterDay" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="NumberLength" HeaderText="Num Length"
                    UniqueName="NumberLength" SortExpression="NumberLength" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SeparatorAfterNumber" HeaderText="Sep."
                    UniqueName="SeparatorAfterNumber" SortExpression="SeparatorAfterNumber" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />    
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="NumberGroupLength"
                    HeaderText="Grp Length" UniqueName="NumberGroupLength" SortExpression="NumberGroupLength"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="NumberGroupSeparator"
                    HeaderText="Grp Sep" UniqueName="NumberGroupSeparator" SortExpression="NumberGroupSeparator"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                 <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsUsedYearToDateOrder" HeaderText="Year To Date Order"
                    UniqueName="IsUsedYearToDateOrder" SortExpression="IsUsedYearToDateOrder" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />   
                <telerik:GridTemplateColumn>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
