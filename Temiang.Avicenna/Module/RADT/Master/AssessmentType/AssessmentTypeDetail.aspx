<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="AssessmentTypeDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.AssessmentTypeDetail" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblItemID" runat="server" Text="Item Group ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtItemID" runat="server" Width="300px" ReadOnly="true" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblItemName" runat="server" Text="Item Group Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px"  ReadOnly="true"/>
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" ReadOnly="true"/>
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tab" runat="server" MultiPageID="mpgEhr" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Body Diagram" PageViewID="pgvBodyDiagram" Selected="true" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgEhr" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView runat="server" ID="pgvBodyDiagram">
            <telerik:RadGrid ID="grdBodyDiagram" runat="server" AutoGenerateColumns="False" GridLines="None"
                OnNeedDataSource="grdBodyDiagram_NeedDataSource">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="BodyID">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkIsSelect" Enabled="<%#DataModeCurrent != AppEnum.DataMode.Read %>"
                                    Checked='<%#DataBinder.Eval(Container.DataItem, "IsSelect") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="BodyID" HeaderText="ID"
                            UniqueName="BodyID" SortExpression="BodyID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="BodyName" HeaderText="Name"
                            UniqueName="BodyName" SortExpression="BodyName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Description" HeaderText="Description"
                            UniqueName="Description" SortExpression="Description" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBinaryImageColumn DataField="BodyImage" HeaderText="Body Diagram" UniqueName="BodyImage"
                            ImageHeight="100px" ImageWidth="100px" ResizeMode="Fit" />
                        <telerik:GridTemplateColumn>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
