<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="AssessmentAspectList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.TrainingHR.Master.AssessmentAspect.AssessmentAspectList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="AssessmentAspectID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AssessmentAspectID" HeaderText="ID"
                    UniqueName="AssessmentAspectID" SortExpression="AssessmentAspectID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="AssessmentAspectName" HeaderText="Assessment Aspect" UniqueName="AssessmentAspectName"
                    SortExpression="AssessmentAspectName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MinValue"
                    HeaderText="Min Value" UniqueName="MinValue" SortExpression="MinValue"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MaxValue"
                    HeaderText="Max Value" UniqueName="MaxValue" SortExpression="MaxValue"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false">
                </telerik:GridNumericColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>