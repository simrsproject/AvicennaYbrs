<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="CompletenessAnalysisHistoryDetailList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.CompletenessAnalysisHistoryDetailList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:100%">
                <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource" AutoGenerateColumns="False" GridLines="None">
                    <MasterTableView DataKeyNames="DocumentFilesID" CommandItemDisplay="None" ShowHeader="True">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="DocumentFilesID" HeaderText="ID"
                                UniqueName="DocumentFilesID" SortExpression="DocumentFilesID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DocumentNumber" HeaderText="Document Number"
                                UniqueName="DocumentNumber" SortExpression="DocumentNumber" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="DocumentName" HeaderText="Document Name"
                                UniqueName="DocumentName" SortExpression="DocumentName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="Notes" HeaderText="Notes To Unit"
                                UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridTemplateColumn />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="False" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
