<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="ItemIdiList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ItemIdiList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="IdiCode">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="IdiCode" HeaderText="IDI Code"
                    UniqueName="IdiCode" SortExpression="IdiCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="IdiName" HeaderText="Description" UniqueName="IdiName"
                    SortExpression="IdiName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Icd9Cm" HeaderText="ICD 9 CM" UniqueName="Icd9Cm"
                    SortExpression="Icd9Cm">
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="F_1" HeaderText="F_1"
                    UniqueName="F_1" SortExpression="F_1" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="F_2_1" HeaderText="F_2_1"
                    UniqueName="F_2_1" SortExpression="F_2_1" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="F_2_2" HeaderText="F_2_2"
                    UniqueName="F_2_2" SortExpression="StartingValue" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="F_2_3" HeaderText="F_2_3"
                    UniqueName="F_2_3" SortExpression="F_2_3" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="F_3" HeaderText="F_3"
                    UniqueName="F_3" SortExpression="F_3" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="F_4" HeaderText="F_4"
                    UniqueName="F_4" SortExpression="F_4" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Rvu" HeaderText="RVU"
                    UniqueName="Rvu" SortExpression="Rvu" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Specialist" HeaderText="Specialist" UniqueName="Specialist"
                    SortExpression="Specialist">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
