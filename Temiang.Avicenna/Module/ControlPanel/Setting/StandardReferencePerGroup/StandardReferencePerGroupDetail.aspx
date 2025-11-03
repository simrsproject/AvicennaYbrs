<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="StandardReferencePerGroupDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.Setting.StandardReferencePerGroupDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            function openWorkTradeItem(refid) {
                var oWnd = $find("<%= winOpen.ClientID %>");
                oWnd.SetUrl("WorkTradeItemList.aspx?refid=" + refid);
                oWnd.Show();
                oWnd.Maximize();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winOpen" Animation="None" Width="800px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblStandardReferenceID" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtStandardReferenceID" runat="server" Width="280px"
                                ReadOnly="True">
                            </telerik:RadTextBox>
                            <asp:CheckBox ID="chkIsHasCOA" runat="server" Text="" Enabled="false" />

                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvStandardReferenceID" runat="server" ErrorMessage="ID required."
                                ControlToValidate="txtStandardReferenceID" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblStandardReferenceName" runat="server" Text="Description"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtStandardReferenceName" runat="server" Width="280px" ReadOnly="True">
                            </telerik:RadTextBox>
                            <asp:CheckBox ID="chkIsNumericValue" runat="server" Text="" Enabled="false" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvStandardReferenceName" runat="server" ErrorMessage="Description required."
                                ControlToValidate="txtStandardReferenceName" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItem_UpdateCommand"
        OnDeleteCommand="grdItem_DeleteCommand" OnInsertCommand="grdItem_InsertCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="StandardReferenceID, ItemID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                    SortExpression="ItemID">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="NumericValue" HeaderText="Numeric Value" UniqueName="NumericValue" HeaderStyle-Width="100px"
                    SortExpression="NumericValue" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right">
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn DataField="Note" HeaderText="Notes" UniqueName="Note" SortExpression="Note">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ReferenceID" HeaderText="Reference ID" UniqueName="ReferenceID"
                    SortExpression="ReferenceID">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="120px" DataField="IsNeedCrossMatchingProcess" HeaderText="Need Cross Matching Process"
                    UniqueName="IsNeedCrossMatchingProcess" SortExpression="IsNeedCrossMatchingProcess" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsReturnable" HeaderText="Returnable"
                    UniqueName="IsReturnable" SortExpression="IsReturnable" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn DataField="ReferenceID" HeaderText="Back Color" UniqueName="BackColorTemplateColumn">
                    <ItemTemplate>
                        <div style="width: 100%; background-color: <%#DataBinder.Eval(Container.DataItem,"ReferenceID")%>">
                            <%#DataBinder.Eval(Container.DataItem,"ReferenceID")%>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="CoaCode" HeaderText="Coa Code" SortExpression="CoaCode"
                    UniqueName="CoaCode" HeaderStyle-Width="100px">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CoaName" HeaderText="Coa Name" SortExpression="CoaName"
                    UniqueName="CoaName">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
                <telerik:GridTemplateColumn UniqueName="openWorkTradeItem">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openWorkTradeItem('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"Work Trade Item\" /></a>",
                                                                                                                                        DataBinder.Eval(Container.DataItem, "ItemID"))%>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings UserControlName="StandardReferencePerGroupItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="grdStandardReferencePerGroupEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
