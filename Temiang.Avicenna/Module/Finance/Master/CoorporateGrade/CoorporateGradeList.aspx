<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="CoorporateGradeList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.CoorporateGradeList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="CoorporateGradeID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CoorporateGradeID" HeaderText="ID"
                    UniqueName="CoorporateGradeID" SortExpression="CoorporateGradeID" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="CoorporateGradeLevel" HeaderText="Level"
                    UniqueName="CoorporateGradeLevel" SortExpression="CoorporateGradeLevel" DataFormatString="{0:n0}" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="CoorporateGradeMin" HeaderText="Minimum"
                    UniqueName="CoorporateGradeMin" SortExpression="CoorporateGradeMin" DataFormatString="{0:n0}" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="CoorporateGradeInterval" HeaderText="Interval"
                    UniqueName="CoorporateGradeInterval" SortExpression="CoorporateGradeInterval" DataFormatString="{0:n0}" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="CoorporateGradeMax" HeaderText="Maximum"
                    UniqueName="CoorporateGradeMax" SortExpression="CoorporateGradeMax" DataFormatString="{0:n0}" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="CoorporateGradeCoefficient" HeaderText="Coefficient"
                    UniqueName="CoorporateGradeCoefficient" SortExpression="CoorporateGradeCoefficient" DataFormatString="{0:n2}" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right">
                </telerik:GridNumericColumn>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
