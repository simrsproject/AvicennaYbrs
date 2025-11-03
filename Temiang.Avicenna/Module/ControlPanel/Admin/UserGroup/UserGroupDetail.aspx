<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="UserGroupDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.Admin.UserGroupDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock runat="server" ID="scriptBlock">

        <script type="text/javascript">
            <!--
    function grdSelection_OnRowDropping(sender, args) {
        if (<%=(DataModeCurrent==Temiang.Avicenna.Common.AppEnum.DataMode.Read).ToString().ToLower() %>)
            {
                args.set_cancel(true);
                return;
            }
                if (sender.get_id() == "<%=grdSelection.ClientID %>") {
                var node = args.get_destinationHtmlElement();
                if (!isChildOf('<%=grdSelected.ClientID %>', node)) {
                            args.set_cancel(true);
                        }
                    }
            }

            function grdSelected_OnRowDropping(sender, args) {
                if (<%=(DataModeCurrent==Temiang.Avicenna.Common.AppEnum.DataMode.Read).ToString().ToLower() %>)
                    {
                        args.set_cancel(true);
                        return;
                    }
                if (sender.get_id() == "<%=grdSelected.ClientID %>") {
                        var node = args.get_destinationHtmlElement();
                        if (!isChildOf('<%=grdSelection.ClientID %>', node)) {
                            args.set_cancel(true);
                        }
                    }
            }

            function isChildOf(parentId, element) {
                while (element) {
                    if (element.id && element.id.indexOf(parentId) > -1) {
                        return true;
                    }
                    element = element.parentNode;
                }
                return false;
            }
            -->
        </script>

    </telerik:RadScriptBlock>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblUserGroupID" runat="server" Text="User Group ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtUserGroupID" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvUserGroupID" runat="server" ErrorMessage="User Group ID required."
                    ValidationGroup="entry" ControlToValidate="txtUserGroupID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblUserGroupName" runat="server" Text="User Group Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtUserGroupName" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvUserGroupName" runat="server" ErrorMessage="User Group Name required."
                    ValidationGroup="entry" ControlToValidate="txtUserGroupName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr style="display: none">
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox ID="chkIsEditAble" runat="server" Text="Editable" />
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
    </table>
    <br />
    <telerik:RadSplitter runat="server" Orientation="Horizontal" Width="100%" Height="249px">
        <telerik:RadPane ID="paneLeft" runat="server" Width="100%">
            <div style="width: 100%">
                <asp:Image ID="fw_imgLeft" ImageUrl="~/Images/boundleft.gif" runat="server" />
                <asp:Label ID="fw_lblProgramCation" runat="server" Text="Note: Drag row program from Unselected Box to selected Program Box or from Selected Box to
                        Unselected Program Box"></asp:Label>
            </div>
            <fieldset>
                <legend>
                    <asp:Label ID="lblFilter" runat="server" Text="Search Filter" ForeColor="Blue" Font-Italic="true"></asp:Label>
                </legend>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 50%" valign="top">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblModule" runat="server" Text="Module"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboModule" Width="300px" AllowCustomText="true"
                                            MarkFirstMatch="true">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="BtnFilterModule" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%" valign="top">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblProgramType" runat="server" Text="Program Type"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboProgramType" Width="300px" AllowCustomText="true"
                                            MarkFirstMatch="true">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="BtnFilterProgramType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <telerik:RadGrid ID="grdSelection" Height="200px" runat="server" AutoGenerateColumns="false"
                AllowMultiRowSelection="true" AllowSorting="true" OnNeedDataSource="grdSelection_NeedDataSource"
                OnRowDrop="grdSelection_RowDrop">
                <MasterTableView DataKeyNames="ProgramID">
                    <Columns>
                        <telerik:GridClientSelectColumn HeaderStyle-Width="40px" UniqueName="ClientSelectColumn1" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ProgramID" HeaderText="ID"
                            UniqueName="ProgramID" SortExpression="ProgramID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ProgramName" HeaderText="Unselected Program"
                            UniqueName="ProgramName" SortExpression="ProgramName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ModuleName" HeaderText="Module"
                            UniqueName="ModuleName" SortExpression="ModuleName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ProgramType" HeaderText="Type"
                            UniqueName="ProgramType" SortExpression="ProgramType" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </MasterTableView>
                <ClientSettings AllowRowsDragDrop="True">
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="true" />
                    <ClientEvents OnRowDropping="grdSelection_OnRowDropping" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPane>
    </telerik:RadSplitter>
    <div style="width: 100%">
        <asp:Image ID="Image3" ImageUrl="~/Images/boundleft.gif" runat="server" />
        <asp:Label ID="Label1" runat="server" Text="Selected Program"></asp:Label>
    </div>
    <telerik:RadGrid ID="grdSelected" Height="300px" runat="server" AutoGenerateColumns="false"
        AllowMultiRowSelection="true" AllowSorting="true" OnNeedDataSource="grdSelected_NeedDataSource"
        OnRowDrop="grdSelected_RowDrop" OnItemCreated="grdSelected_ItemCreated">
        <MasterTableView DataKeyNames="ProgramID">
            <Columns>
                <telerik:GridClientSelectColumn HeaderStyle-Width="40px" UniqueName="ClientSelectColumn2" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ProgramID" HeaderText="ID"
                    UniqueName="ProgramID" SortExpression="ProgramID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ProgramName" HeaderText="Selected Program" UniqueName="ProgramName"
                    SortExpression="ProgramName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ModuleName" HeaderText="Module"
                    UniqueName="ModuleName" SortExpression="ModuleName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ProgramType" HeaderText="Type"
                    UniqueName="ProgramType" SortExpression="ProgramType" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn DataField="IsAddAble" UniqueName="IsAddAble" HeaderText="Add"
                    HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsAddAble" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                            Checked='<%#DataBinder.Eval(Container.DataItem, "IsAddAble") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="IsEditAble" UniqueName="IsEditAble" HeaderText="Edit"
                    HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsEditAble" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                            Checked='<%# DataBinder.Eval(Container.DataItem, "IsEditAble") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="IsDeleteAble" UniqueName="IsDeleteAble" HeaderText="Delete"
                    HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsDeleteAble" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                            Checked='<%# DataBinder.Eval(Container.DataItem, "IsDeleteAble") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="IsApprovalAble" UniqueName="IsApprovalAble"
                    HeaderText="Approval" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsApprovalAble" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                            Checked='<%# DataBinder.Eval(Container.DataItem, "IsApprovalAble") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="IsUnApprovalAble" UniqueName="IsUnApprovalAble"
                    HeaderText="Un-Approval" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsUnApprovalAble" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                            Checked='<%# DataBinder.Eval(Container.DataItem, "IsUnApprovalAble") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="IsVoidAble" UniqueName="IsVoidAble" HeaderText="Void"
                    HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsVoidAble" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                            Checked='<%# DataBinder.Eval(Container.DataItem, "IsVoidAble") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="IsUnVoidAble" UniqueName="IsUnVoidAble" HeaderText="Un-Void"
                    HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsUnVoidAble" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                            Checked='<%# DataBinder.Eval(Container.DataItem, "IsUnVoidAble") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="IsExportAble" UniqueName="IsExportAble" HeaderText="Export"
                    HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsExportAble" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                            Checked='<%# DataBinder.Eval(Container.DataItem, "IsExportAble") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="IsCrossUnitAble" UniqueName="IsCrossUnitAble"
                    HeaderText="Cross Unit" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsCrossUnitAble" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                            Checked='<%# DataBinder.Eval(Container.DataItem, "IsCrossUnitAble") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="IsPowerUserAble" UniqueName="IsPowerUserAble"
                    HeaderText="Power User" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsPowerUserAble" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                            Checked='<%# DataBinder.Eval(Container.DataItem, "IsPowerUserAble") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="IsProgramAddAble" HeaderText="" UniqueName="IsProgramAddAble"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="IsProgramEditAble" HeaderText="" UniqueName="IsProgramEditAble"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="IsProgramDeleteAble" HeaderText="" UniqueName="IsProgramDeleteAble"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="IsProgramApprovalAble" HeaderText="" UniqueName="IsProgramApprovalAble"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="IsProgramUnApprovalAble" HeaderText="" UniqueName="IsProgramUnApprovalAble"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="IsProgramVoidAble" HeaderText="" UniqueName="IsProgramVoidAble"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="IsProgramUnVoidAble" HeaderText="" UniqueName="IsProgramUnVoidAble"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="IsProgramExportAble" HeaderText="" UniqueName="IsProgramExportAble"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="IsProgramCrossUnitAble" HeaderText="" UniqueName="IsProgramCrossUnitAble"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="IsProgramPowerUserAble" HeaderText="" UniqueName="IsProgramPowerUserAble"
                    Visible="false" />
            </Columns>
        </MasterTableView>
        <ClientSettings AllowRowsDragDrop="True">
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="true" />
            <ClientEvents OnRowDropping="grdSelected_OnRowDropping" />
            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
