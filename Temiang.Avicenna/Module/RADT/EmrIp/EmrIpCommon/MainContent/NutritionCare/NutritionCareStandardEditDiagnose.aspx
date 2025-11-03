<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NutritionCareStandardEditDiagnose.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NutritionCare.NutritionCareStandardEditDiagnose" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="gridListDiagnosa" runat="server" AutoGenerateColumns="false"
        GridLines="None" OnNeedDataSource="gridListDiagnosa_NeedDataSource">
        <MasterTableView ClientDataKeyNames="TerminologyID,ID" DataKeyNames="TerminologyID,ID"
            GroupLoadMode="Client">
            <Columns>
                <telerik:GridBoundColumn DataField="TerminologyName" HeaderText="Diagnose" SortExpression="TerminologyName"
                    UniqueName="TerminologyName" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TerminologyParentID" HeaderText="Parent"
                    SortExpression="TerminologyParentID" UniqueName="TerminologyParentID"
                    Visible="false">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TerminologyNameDisplay" HeaderText="Diagnose"
                    SortExpression="TerminologyNameDisplay" UniqueName="TerminologyNameDisplay">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="S" UniqueName="S">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtS" Width="200px" runat="server" Text='<%#Eval("S").ToString()%>'></telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="O" UniqueName="Biochemistry">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtO" Width="200px" runat="server" Text='<%#Eval("O").ToString()%>'></telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
