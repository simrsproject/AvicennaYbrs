<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ExamOrderRadiologyResult.aspx.cs" Inherits="Temiang.Avicenna.Module.Emr.ExamOrderRadiologyResult" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdImagingResult" runat="server" OnNeedDataSource="grdImagingResult_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" Height="524px" Width="980px">
        <MasterTableView DataKeyNames="TransactionNo" HierarchyDefaultExpanded="true">
            <NestedViewTemplate>
                <div style="padding-left: 20px;">
                    <telerik:RadEditor ID="txtResult" runat="server" Width="900px" Height="100px" NewLineMode="Br"
                        EditModes="Preview" Content='<%#Eval("TestResult")%>'>
                        <Tools>
                            <telerik:EditorToolGroup name="invisibleToolbar" dockingZone="Bottom">
                            </telerik:EditorToolGroup>
                        </Tools>
                    </telerik:RadEditor>
                </div>
            </NestedViewTemplate>
            <Columns>
                <telerik:GridBoundColumn DataField="TransactionNo" UniqueName="TransactionNo" HeaderText="Transaction No" HeaderStyle-Width="150px" />
                <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" HeaderText="Test Name" />
                <telerik:GridDateTimeColumn DataField="TestResultDateTime" UniqueName="TestResultDateTime"
                    HeaderText="Result Date" />
                <telerik:GridBoundColumn DataField="ParamedicName" UniqueName="ParamedicName" HeaderText="Physician" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="false" />
            <Scrolling AllowScroll="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
