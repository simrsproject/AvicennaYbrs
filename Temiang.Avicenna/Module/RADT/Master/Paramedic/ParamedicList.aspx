<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="ParamedicList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ParamedicID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ParamedicID" HeaderText="Physician ID"
                    UniqueName="ParamedicID" SortExpression="ParamedicID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician Name" UniqueName="ParamedicName"
                    SortExpression="ParamedicName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ParamedicInitial" HeaderText="Initial"
                    UniqueName="ParamedicInitial" SortExpression="ParamedicInitial">
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="DateOfBirth" HeaderText="Date Of Birth"
                    HeaderStyle-HorizontalAlign="center" UniqueName="DateOfBirth" SortExpression="DateOfBirth"
                    ItemStyle-HorizontalAlign="Center">
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="StreetName" HeaderText="Street Name" UniqueName="StreetName"
                    SortExpression="StreetName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PhoneNo" HeaderText="Phone No"
                    UniqueName="PhoneNo" SortExpression="PhoneNo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LicenseNo" HeaderText="License No"
                    UniqueName="LicenseNo" SortExpression="LicenseNo">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
