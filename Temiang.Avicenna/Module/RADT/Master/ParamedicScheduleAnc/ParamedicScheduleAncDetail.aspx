<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ParamedicScheduleAncDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicScheduleAnc.ParamedicScheduleAncDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="50">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                AutoPostBack="false" HighlightTemplatedItems="true" OnItemDataBound="cboParamedicID_ItemDataBound"
                                OnItemsRequested="cboParamedicID_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ParamedicName") %>
                                    </b>
                                    <br />
                                    Physician ID :
                                    <%# DataBinder.Eval(Container.DataItem, "ParamedicID")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="50">
                            <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Physician required."
                                ValidationGroup="entry" ControlToValidate="cboParamedicID" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblScheduleDate" runat="server" Text="Schedule Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtScheduleDate" runat="server" Width="100px" />
                        </td>
                        <td width="50">
                            <asp:RequiredFieldValidator ID="rfvScheduleDate" runat="server" ErrorMessage="Schedule Date"
                                ValidationGroup="entry" ControlToValidate="txtScheduleDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItem_UpdateCommand"
        OnDeleteCommand="grdItem_DeleteCommand" OnInsertCommand="grdItem_InsertCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="OperationalTimeID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn DataField="OperationalTimeID" HeaderText="ID" UniqueName="TemplateColumn">
                    <ItemTemplate>
                        <div style="width: 100%; background-color: <%#DataBinder.Eval(Container.DataItem,"OperationalTimeBackcolor")%>">
                            <%#DataBinder.Eval(Container.DataItem,"OperationalTimeID")%>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="OperationalTimeName" HeaderText="Operational Time"
                    UniqueName="OperationalTimeName" SortExpression="OperationalTimeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsIpr" HeaderText="IPR"
                    UniqueName="IsIpr" SortExpression="IsIpr" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsOpr" HeaderText="OPR"
                    UniqueName="IsOpr" SortExpression="IsOpr" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsEmr" HeaderText="EMR"
                    UniqueName="IsEmr" SortExpression="IsEmr" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="ParamedicScheduleAncItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="EditParamedicScheduleAncItemDetail">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings>
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
