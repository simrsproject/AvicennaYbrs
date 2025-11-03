<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PatientIncidentComponentTypePickList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PatientIncidentComponentTypePickList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            var sumInput = null;
            var tempValue = 0.0;

            function Load(sender, args) {
                sumInput = sender;
            }

            function Blur(sender, args) {
                sumInput.set_value(tempValue + sender.get_value());
            }

            function Focus(sender, args) {
                tempValue = sumInput.get_value() - sender.get_value();
            }

            function RowSelected(sender, args) {
                __doPostBack("<%= grdListDetail.UniqueID %>", args.getDataKeyValue("SRIncidentType"));
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdListDetail" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListDetail" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="15%" valign="top">
                <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnNeedDataSource="grdList_NeedDataSource" OnItemDataBound="grdList_ItemDataBound"
                    OnDataBound="grdList_DataBound">
                    <MasterTableView DataKeyNames="ItemID" ClientDataKeyNames="ItemID">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="grdList_HeaderChkBoxCheckedChanged"
                                        AutoPostBack="True" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="itemChkbox" runat="server" AutoPostBack="True" OnCheckedChanged="grdList_ItemChkBoxCheckedChanged" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="ItemID" UniqueName="ItemID" SortExpression="ItemID"
                                HeaderText="ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                            <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" SortExpression="ItemName"
                                HeaderText="Incident Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td>
                <asp:Panel ID="Panel1" runat="server" Width="3px" />
            </td>
            <td width="85%" valign="top">
                <telerik:RadGrid ID="grdListDetail" runat="server" AutoGenerateColumns="False"
                    GridLines="Both" OnItemDataBound="grdListDetail_ItemDataBound"
                    OnPageSizeChanged="grdListDetail_PageSizeChanged"
                    OnPageIndexChanged="grdListDetail_PageIndexChanged"
                    OnSortCommand="grdListDetail_SortCommand"
                    ShowFooter="True" AllowPaging="true" PageSize="50" PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowSorting="true"
                    OnItemCreated="grdListDetail_ItemCreated">
                    <MasterTableView DataKeyNames="SRIncidentType, ComponentID, SubComponentID" ClientDataKeyNames="SRIncidentType, ComponentID, SubComponentID">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="IncidentType" HeaderText="Incident Type "></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="SRIncidentType" SortOrder="Ascending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="ComponentName" HeaderText="Component "></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="ComponentID" SortOrder="Ascending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState"
                                        AutoPostBack="True" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="detailChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsChecked") %>'
                                        AutoPostBack="true" OnCheckedChanged="ToggleSelectedState" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="SRIncidentType" UniqueName="SRIncidentType" Visible="false" />
                            <telerik:GridBoundColumn DataField="IncidentType" UniqueName="IncidentType" Visible="false" />
                            <telerik:GridBoundColumn DataField="ComponentID" UniqueName="ComponentID" Visible="false" />
                            <telerik:GridBoundColumn DataField="ComponentName" UniqueName="ComponentName" Visible="false" />
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SubComponentID" HeaderText="ID"
                                UniqueName="SubComponentID" SortExpression="SubComponentID" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="SubComponent" HeaderText="Sub Component"
                                UniqueName="SubComponent" SortExpression="SubComponent" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="500px" HeaderText="Description" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" UniqueName="SubComponentName">
                                <ItemTemplate>
                                    <telerik:RadTextBox ID="txtSubComponentName" runat="server" Width="450px" Text='<%#Eval("SubComponentName")%>' MaxLength="250"
                                    Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsAllowEdit")) %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="500px" HeaderText="Modus" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" UniqueName="Modus">
                                <ItemTemplate>
                                    <telerik:RadTextBox ID="txtModus" runat="server" Width="450px" Text='<%#Eval("Modus")%>' MaxLength="500"  />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="50px" HeaderText="IsAllowEdit" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIsAllowEdit" runat="server" Width="45px" Checked='<%#Eval("IsAllowEdit")%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
