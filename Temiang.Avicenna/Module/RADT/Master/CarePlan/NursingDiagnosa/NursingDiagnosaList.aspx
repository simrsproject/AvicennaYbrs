<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="NursingDiagnosaList.aspx.cs" Inherits="Temiang.Avicenna.Module.NursingCare.Master.NursingDiagnosaList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="NursingDiagnosaID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="NursingDiagnosaID" HeaderText="ID"
                    UniqueName="NursingDiagnosaID" SortExpression="NursingDiagnosaID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="NursingDiagnosaCode" HeaderText="Code"
                    UniqueName="NursingDiagnosaCode" SortExpression="NursingDiagnosaCode" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="NursingDiagnosaName" HeaderText="Name"
                    UniqueName="NursingDiagnosaName" SortExpression="NursingDiagnosaName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ItemName" HeaderText="Level"
                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="NursingDiagnosaParentID" HeaderText="Parent ID"
                    UniqueName="NursingDiagnosaParentID" SortExpression="NursingDiagnosaParentID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="NursingDiagnosaParentCode" HeaderText="Parent Code"
                    UniqueName="NursingDiagnosaParentCode" SortExpression="NursingDiagnosaParentCode" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="NursingDiagnosaParentName" HeaderText="Parent Name"
                    UniqueName="NursingDiagnosaParentName" SortExpression="NursingDiagnosaParentName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="NsDiagnosaTypeName" HeaderText="Diagnosis Type"
                    UniqueName="NsDiagnosaTypeName" SortExpression="NsDiagnosaTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn DataField="NsEtiologyTypeName" HeaderText="Etiology Type"
                    UniqueName="NsEtiologyTypeName" SortExpression="NsEtiologyTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
