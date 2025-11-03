<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NursingCareStandardPickImplementation.aspx.cs" 
    Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare.NursingCareStandardPickImplementation" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="width: 75%; vertical-align: top;">
                <telerik:RadGrid ID="gridListImplementasi" runat="server" AutoGenerateColumns="false"
                    GridLines="None" OnNeedDataSource="gridListImplementasi_OnNeedDataSource"
                    AllowPaging="True" PageSize="10" AllowSorting="False" AllowCustomPaging="true" >
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    <MasterTableView ClientDataKeyNames="NursingDiagnosaID" DataKeyNames="NursingDiagnosaID,ID" 
                    InsertItemPageIndexAction="ShowItemOnCurrentPage">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="DS" UniqueName="chkSelectedDS">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
	                                <asp:CheckBox runat="server" ID="chkDS" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="DO" UniqueName="chkSelectedDO">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
	                                <asp:CheckBox runat="server" ID="chkDO" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Id" HeaderText="Id" SortExpression="Id"
                                Visible="false" UniqueName="Id">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ExecuteDateTime" HeaderText="Execution Date"
                                DataFormatString="{0:MM/dd/yyyy HH:mm}" DataType="System.DateTime" 
                                SortExpression="ExecuteDateTime" UniqueName="ExecuteDateTime" >
                                <HeaderStyle HorizontalAlign="Left" Width="130" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Implemetation" UniqueName="GetFullImplementationName">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
	                                <%# GetFullImplementationNameFormatted(((string)Eval("NursingDiagnosaName")), (string)Eval("S"),(string)Eval("O"),(string)Eval("A"),((string)Eval("P"))).Replace("\n", "<br/>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Respond" UniqueName="Respond">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
	                                <%# (Eval("Respond")).ToString().Replace("\n", "<br/>") + " " + (Eval("Respond2")).ToString().Replace("\n", "<br/>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="NursingDiagnosaParentID" HeaderText="Parent"
                                SortExpression="NursingDiagnosaParentID" UniqueName="NursingDiagnosaParentID"
                                Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TmpNursingDiagnosaID" HeaderText="Id" SortExpression="TmpNursingDiagnosaID"
                                Visible="false" UniqueName="TmpNursingDiagnosaID">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RefToUserName" HeaderText="User" SortExpression="RefToUserName"
                                UniqueName="RefToUserName">
                                <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
