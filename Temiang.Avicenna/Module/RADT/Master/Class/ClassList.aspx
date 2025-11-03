<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ClassList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ClassList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ClassID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ClassID" HeaderText="Class ID"
                    UniqueName="ClassID" SortExpression="ClassID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class Name" UniqueName="ClassName"
                    SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ShortName" HeaderText="Short Name"
                    UniqueName="ShortName" SortExpression="ShortName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemName" HeaderText="Class RL"
                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MarginPercentage"
                    HeaderText="Margin Percentage (Item Medic)" UniqueName="MarginPercentage" SortExpression="MarginPercentage"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Margin2Percentage"
                    HeaderText="Margin Percentage (Item Non Medic)" UniqueName="Margin2Percentage"
                    SortExpression="Margin2Percentage" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DepositAmount" HeaderText="Deposit Amount"
                    UniqueName="DepositAmount" SortExpression="DepositAmount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="ClassSeq" HeaderText="Seq. No."
                    UniqueName="ClassSeq" SortExpression="ClassSeq" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsInPatientClass"
                    HeaderText="Inpatient Class" UniqueName="IsInPatientClass" SortExpression="IsInPatientClass"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsTariffClass"
                    HeaderText="Tariff Class" UniqueName="IsTariffClass" SortExpression="IsTariffClass"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
