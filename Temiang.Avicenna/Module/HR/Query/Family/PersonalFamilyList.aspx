<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="PersonalFamilyList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Query.PersonalFamilyList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="PersonalFamilyID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PersonalFamilyID"
                    HeaderText="ID" UniqueName="PersonalFamilyID" SortExpression="PersonalFamilyID"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="EmployeeName" HeaderText="Employee Name"
                    UniqueName="EmployeeName" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="MedicalNo" HeaderText="Medical No"
                    UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="FamilyRelationName"
                    HeaderText="Family Relation" UniqueName="FamilyRelationName" SortExpression="FamilyRelationName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FamilyName" HeaderText="Family Name"
                    UniqueName="FamilyName" SortExpression="FamilyName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DateBirth" HeaderText="Date Of Birth"
                    UniqueName="DateBirth" SortExpression="DateBirth" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="EducationLevelName"
                    HeaderText="Education Level" UniqueName="EducationLevelName" SortExpression="EducationLevelName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Address" HeaderText="Address"
                    UniqueName="Address" SortExpression="Address" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="Phone" HeaderText="Phone"
                    UniqueName="Phone" SortExpression="Phone" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MaritalStatusName" HeaderText="Marital Status"
                    UniqueName="MaritalStatusName" SortExpression="MaritalStatusName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="GenderTypeName" HeaderText="Gender Type"
                    UniqueName="GenderTypeName" SortExpression="GenderTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="False" />
                <telerik:GridCheckBoxColumn DataField="IsGuaranteed" HeaderText="Guaranteed" UniqueName="IsGuaranteed"
                    SortExpression="IsGuaranteed">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
