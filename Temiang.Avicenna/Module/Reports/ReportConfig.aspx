<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="ReportConfig.aspx.cs" Inherits="Temiang.Avicenna.Module.Reports.ReportConfig" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboDatasource">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboModule" />
                    <telerik:AjaxUpdatedControl ControlID="cboStoredProcedure" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboModule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtUrl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboStoredProcedure">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdParameters" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Datasource
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboDatasource" runat="server" Width="304px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboDatasource_SelectedIndexChanged" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Module
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboModule" runat="server" Width="304px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboModule_SelectedIndexChanged" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Report Name
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReportName" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReportName" runat="server" ErrorMessage="Report Name required."
                                ValidationGroup="entry" ControlToValidate="txtReportName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Description
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Url
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtUrl" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Stored Procedure
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboStoredProcedure" runat="server" Width="304px" AutoPostBack="true"
                                EnableLoadOnDemand="true" OnItemDataBound="cboStoredProcedure_ItemDataBound"
                                OnItemsRequested="cboStoredProcedure_ItemsRequested" OnSelectedIndexChanged="cboStoredProcedure_SelectedIndexChanged">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvStoredProcedure" runat="server" ErrorMessage="Stored Procedure required."
                                ValidationGroup="entry" ControlToValidate="cboStoredProcedure" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdParameters" runat="server" GridLines="None" AutoGenerateColumns="false"
        OnItemCreated="grdParameters_ItemCreated">
        <MasterTableView DataKeyNames="ParameterName">
            <Columns>
                <telerik:GridBoundColumn DataField="ParameterName" HeaderText="Parameter Name" UniqueName="ParameterName"
                    SortExpression="ParameterName" HeaderStyle-Width="300px" />
                <telerik:GridBoundColumn DataField="ParameterDataType" HeaderText="Data Type" UniqueName="ParameterDataType"
                    SortExpression="ParameterDataType" HeaderStyle-Width="150px" />
                <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderText="Option Control">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cboOptionControl" runat="server" Width="100%" AllowCustomText="true"
                            Filter="Contains" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn2" HeaderText="Merged" HeaderStyle-Width="60px"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsMerged" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
